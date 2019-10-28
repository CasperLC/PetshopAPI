using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.ApplicationService;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PetApp.UI.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private IOwnerService _ownerService;

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET api/owners
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Owner>> Get([FromQuery] Filter filter)
        {
            try
            {
                if (filter.CurrentPage == 0 && filter.ItemsPrPage == 0)
                {
                    return Ok(_ownerService.GetAllOwner());
                }
                else
                {
                    return Ok(_ownerService.GetFilteredOwners(filter));
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/owners
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            try
            {
                return Ok(_ownerService.AddOwner(owner));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/owners/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            try
            {
                return _ownerService.GetOwner(id);
            }
            catch (Exception e)
            {
                return BadRequest("No owner with that id");
            }
        }

        // PUT api/owners/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public Owner Put(int id, [FromBody] Owner owner)
        {
            return _ownerService.updateOwner(owner);
        }

        // DELETE api/owenrs/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _ownerService.RemoveOwner(id);
        }
    }
}