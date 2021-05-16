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
        public async Task<ActionResult<IEnumerable<ChargeStation>>> GetAll(Guid groupId)
        {
            return await _context.ChargeStation.ToListAsync();
        }

        // GET: api/groups/GroupId/ChargeStations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChargeStation>> GetChargeStation(Guid groupId, Guid id)
        {
            var chargeStation = await _context.ChargeStation.FindAsync(id);

            if (chargeStation == null)
            {
                return NotFound();
            }

            return chargeStation;
        }

        // POST: api/groups/GroupId/ChargeStations
        [HttpPost]
        public async Task<ActionResult<ChargeStationResource>> PostChargeStation(Guid groupId, SaveChargeStationResource saveChargeStation)
        {
            Group group = await _context.Group.FirstAsync(g => g.Id == groupId);
            if (group == null)
                return NotFound("Group not found");

            ChargeStation chargeStation = _mapper.Map<ChargeStation>(saveChargeStation);

            _context.ChargeStation.Add(chargeStation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChargeStation), new { id = chargeStation.Id }, chargeStation);
        }

        // DELETE: api/groups/GroupId/ChargeStations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChargeStation(Guid id)
        {
            var chargeStation = await _context.ChargeStation.FindAsync(id);
            if (chargeStation == null)
            {
                return NotFound();
            }

            _context.ChargeStation.Remove(chargeStation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChargeStationExists(Guid id)
        {
            return _context.ChargeStation.Any(e => e.Id == id);
        }
    }
}
