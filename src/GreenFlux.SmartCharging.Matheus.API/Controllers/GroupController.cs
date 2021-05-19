using AutoMapper;
using GreenFlux.SmartCharging.Matheus.API.Resources;
using GreenFlux.SmartCharging.Matheus.Data;
using GreenFlux.SmartCharging.Matheus.Domain.Exceptions;
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
    [Route(Routes.GroupsRoute)]
    [ApiController]
    public class GroupController : ControllerBase
    {
        //TODO: remove application db dependency
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GroupController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET api/<GroupsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupResource))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
        {
            Group group = await _context.Group.FindAsync(id);
            if (group == null)
                return StatusCode(404);

            return Ok(_mapper.Map<GroupResource>(group));
        }

        // POST api/<GroupsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GroupResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] SaveGroupResource value)
        {
            Group group = _mapper.Map<Group>(value);

            await _context.Group.AddAsync(group);
            await _context.SaveChangesAsync();
            var created = _mapper.Map<GroupResource>(group);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        // PATCH api/<GroupsController>/5
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupResource))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Patch(Guid id, [FromBody] PatchGroupResource value)
        {
            Group group = await _context.Group.Include(g => g.ChargeStations).ThenInclude(c => c.Connectors).FirstOrDefaultAsync(g => g.Id == id);
            if (group == null)
                return StatusCode(404);

            if (value.Capacity.HasValue)
            {
                if (value.Capacity.Value < group.Capacity && group.HasExceededCapacity(group.Capacity - value.Capacity.Value))
                    throw new CapacityExceededException(group.Capacity - value.Capacity.Value,new List<RemoveSuggestions>());

                group.Capacity = value.Capacity.Value;
            }
                
            if (!string.IsNullOrEmpty(value.Name))
                group.Name = value.Name;

            await _context.SaveChangesAsync();
            GroupResource updatedGroup = _mapper.Map<GroupResource>(group);
            return Ok(updatedGroup);
        }

        // DELETE api/<GroupsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            Group group = await _context.Group.FindAsync(id);
            if (group == null)
                return StatusCode(404);

            _context.Group.Remove(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
