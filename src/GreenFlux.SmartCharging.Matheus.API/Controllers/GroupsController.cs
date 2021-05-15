﻿using AutoMapper;
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
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GroupsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<GroupsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<Group>))]
        public async Task<ActionResult<ICollection<GroupResource>>> GetAsync()
        {
            ICollection<Group> groups = await _context.Group.AsNoTracking().ToListAsync();
            return Ok(_mapper.Map<ICollection<GroupResource>>(groups));
        }

        // GET api/<GroupsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Group))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            Group group = await _context.Group.FindAsync(id);
            if (group == null)
                return StatusCode(404);

            return Ok(_mapper.Map<GroupResource>(group));
        }

        // POST api/<GroupsController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Group))]
        public async Task<IActionResult> Post([FromBody] SaveGroupResource value)
        {    
            Group group = _mapper.Map<Group>(value);

            await _context.Group.AddAsync(group);
            await _context.SaveChangesAsync();
            var created = _mapper.Map<GroupResource>((Group)group);
            return Created("", created);
        }

        // DELETE api/<GroupsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
