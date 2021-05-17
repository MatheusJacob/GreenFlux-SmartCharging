using AutoMapper;
using GreenFlux.SmartCharging.Matheus.API.Resources;
using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.API.Controllers
{
    [Route(Routes.ConnectorsRoute)]
    [ApiController]
    public class ConnectorController : ControllerBase
    {        
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ConnectorController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<ConnectorController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConnectorResource>>> GetAll(Guid groupId, Guid chargeStationId)
        {
            Group group = await _context.Group.FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
                return NotFound("Group not found");

            ChargeStation chargeStation = await _context.ChargeStation.FirstOrDefaultAsync(c => c.Id == chargeStationId);
            if (chargeStation == null)
                return NotFound("Charge Station not found");

            ICollection<Connector> connectors = await _context.Connector.Where(c => c.ChargeStationId == chargeStationId).ToListAsync();

            return Ok(_mapper.Map<List<ConnectorResource>>(connectors));
        }

        // GET api/<ConnectorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConnectorResource>> GetConnector(Guid groupId, Guid chargeStationId, int id)
        {
            Group group = await _context.Group.FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
                return NotFound("Group not found");

            ChargeStation chargeStation = await _context.ChargeStation.FirstOrDefaultAsync(c => c.Id == chargeStationId);
            if (chargeStation == null)
                return NotFound("Charge Station not found");

            Connector connector = await _context.Connector.FirstOrDefaultAsync(c => c.Id == id && c.ChargeStationId == chargeStationId);

            if(connector == null)
                return NotFound("Connector not found");

            return _mapper.Map<ConnectorResource>(connector);
        }

        // POST api/<ConnectorController>
        [HttpPost]
        public async Task<ActionResult<ConnectorResource>> Post(Guid groupId, Guid chargeStationId, [FromBody] SaveConnectorResource saveConnector)
        {
            Group group = await _context.Group.Include(g => g.ChargeStations).ThenInclude(c => c.Connectors).FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
                return NotFound("Group not found");
            
            Connector connector = _mapper.Map<Connector>(saveConnector);
            ChargeStation chargeStation;

            if (!group.ChargeStations.TryGetValue(new ChargeStation(chargeStationId), out chargeStation))
                return NotFound("Charge station not found");

            chargeStation.SyncConnectorIds();
            chargeStation.AppendConnector(connector);

            await _context.SaveChangesAsync();

            ConnectorResource connectorResponse = _mapper.Map<ConnectorResource>(connector);
            return CreatedAtAction(nameof(GetConnector), new { chargeStationId = chargeStation.Id, groupId = chargeStation.GroupId, id = connector.Id.Value }, connectorResponse);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConnectorResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ConnectorResource>> Patch(Guid groupId, Guid chargeStationId,int connectorId, [FromBody] PatchConnectorResource patchConnectorResource)
        {
            Group group = await _context.Group.Include(g => g.ChargeStations).ThenInclude(c => c.Connectors).FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
                return NotFound("Group not found");

            ChargeStation chargeStation;

            if (!group.ChargeStations.TryGetValue(new ChargeStation(chargeStationId), out chargeStation))
                return NotFound("Charge station not found");

            Connector connector;

            if(!chargeStation.Connectors.TryGetValue(new Connector(connectorId),out connector))
                return NotFound("Connector not found");

            if (patchConnectorResource.MaxCurrentAmp.HasValue)
                connector.ChangeMaxCurrentAmp(patchConnectorResource.MaxCurrentAmp.Value);

            await _context.SaveChangesAsync();
            ConnectorResource connectorUpdated = _mapper.Map<ConnectorResource>(connector);
            return Ok(connectorUpdated);
        }

        // DELETE api/<ConnectorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid chargeStationId, int id)
        {
            Connector connector = await _context.Connector.FirstOrDefaultAsync(c => c.Id.Value == id && c.ChargeStationId == chargeStationId);
            if (connector == null)
                return StatusCode(404);

            connector.ChargeStation.UpdateTotalMaxCurrentAmp(-connector.MaxCurrentAmp);
            _context.Connector.Remove(connector);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
