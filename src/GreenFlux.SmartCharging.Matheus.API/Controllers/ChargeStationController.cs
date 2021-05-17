using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using AutoMapper;
using GreenFlux.SmartCharging.Matheus.API.Resources;

namespace GreenFlux.SmartCharging.Matheus.API.Controllers
{
    [Route(Routes.ChargeStationsRoute)]
    [ApiController]
    public class ChargeStationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ChargeStationController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }   

        // GET: api/groups/GroupId/ChargeStations
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ChargeStationResource>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ICollection<ChargeStationResource>>> GetAll(Guid groupId)
        {
            Group group = await _context.Group.FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
                return NotFound("Group not found");

            ICollection<ChargeStation> chargeStations = await _context.ChargeStation.Where(c => c.GroupId == groupId).ToListAsync();

            return Ok(_mapper.Map<List<ChargeStationResource>>(chargeStations));
        }

        // GET: api/groups/GroupId/ChargeStations/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChargeStationResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ChargeStationResource>> GetChargeStation(Guid groupId, Guid id)
        {
            Group group = await _context.Group.FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
                return NotFound("Group not found");

            var chargeStation = await _context.ChargeStation.FindAsync(id);

            if (chargeStation == null)
            {
                return NotFound();
            }

            return _mapper.Map<ChargeStationResource>(chargeStation);            
        }

        // POST: api/groups/GroupId/ChargeStations
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChargeStationResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ChargeStationResource>> PostChargeStation(Guid groupId, SaveChargeStationResource saveChargeStation)
        {
            Group group = await _context.Group.Include(g => g.ChargeStations).ThenInclude(c => c.Connectors).FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
                return NotFound("Group not found");
            
            ChargeStation chargeStation = _mapper.Map<ChargeStation>(saveChargeStation);
            group.AppendChargeStation(chargeStation);

            await _context.ChargeStation.AddAsync(chargeStation);

            await _context.SaveChangesAsync();

            ChargeStationResource chargeStationResponse = _mapper.Map<ChargeStationResource>(chargeStation);
            return CreatedAtAction(nameof(GetChargeStation), new { id = chargeStation.Id, groupId = chargeStation.GroupId }, chargeStationResponse);
        }

        // PATCH api/groups/GroupId/ChargeStations/5
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChargeStationResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Patch(Guid id, [FromBody] PatchChargeStationResource value)
        {
            ChargeStation chargeStation = await _context.ChargeStation.FirstOrDefaultAsync(c => c.Id == id);
            if (chargeStation == null)
                return StatusCode(404);

            if (!String.IsNullOrEmpty(value.Name))
                chargeStation.Name = value.Name;

            await _context.SaveChangesAsync();
            ChargeStationResource chargeStationUpdated = _mapper.Map<ChargeStationResource>(chargeStation);
            return Ok(chargeStationUpdated);
        }

        // DELETE: api/groups/GroupId/ChargeStations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChargeStation(Guid id)
        {
            ChargeStation chargeStation = await _context.ChargeStation.Include(c => c.Connectors).FirstOrDefaultAsync( c=> c.Id == id);
            if (chargeStation == null)
                return StatusCode(404);

            _context.ChargeStation.Remove(chargeStation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
