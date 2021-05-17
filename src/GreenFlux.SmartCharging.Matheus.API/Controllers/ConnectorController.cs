using AutoMapper;
using GreenFlux.SmartCharging.Matheus.API.Resources;
using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ConnectorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // DELETE api/<ConnectorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
