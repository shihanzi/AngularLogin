using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularLogin.Context;
using AngularLogin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AngularLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private AppDbContext _authDbContext;

        public LocationController (AppDbContext appDbContext)
        {
            _authDbContext = appDbContext;
        }

        [HttpPost("RegisterLocation")] 
        public async Task<IActionResult> RegisterLocation([FromBody]Location LocObj)
        {
            if (LocObj == null)
                return BadRequest();
            
            if (await CheckLocationNameExistAsync(LocObj.Name))
                return BadRequest(new { Message = "Location Name Already Exist" });

            if (await CheckLocationNicknameExistAsync(LocObj.Nickname))
                return BadRequest(new { Message = "Location Nickname Already Exist" });

            await _authDbContext.Locations.AddAsync(LocObj);
            await _authDbContext.SaveChangesAsync();
            return Ok(new { Message = "Location Registered Successfully" });
        }
        private Task<bool> CheckLocationNameExistAsync(string locationname)
            => _authDbContext.Locations.AnyAsync(x => x.Name == locationname);

        private Task<bool> CheckLocationNicknameExistAsync(string nickname)
            => _authDbContext.Locations.AnyAsync(x => x.Nickname == nickname);

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Location>> GetAllLocations()
        {
            return Ok(await _authDbContext.Locations.ToListAsync());
        }

        [HttpPut("updatelocation")]
        public async Task<IActionResult> UpdateLocation([FromBody] Location LocationObj)
        {
            if (LocationObj == null)
                return BadRequest();

            var location = await _authDbContext.Locations.AsNoTracking().FirstOrDefaultAsync(x => x.LocationId == LocationObj.LocationId);
            if (location == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Location Not Found"
                });
            }
            else
            {
                _authDbContext.Locations.Update(LocationObj);
                await _authDbContext.SaveChangesAsync();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Location Updated Successfully"
                });
            }
        }

        [HttpDelete("deletelocation/{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            var location = await _authDbContext.Locations.FirstOrDefaultAsync(x => x.LocationId == id);
            if (location == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Location Not Found"
                });
            }
            else
            {
                _authDbContext.Remove(location);
                await _authDbContext.SaveChangesAsync();
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Location Deleted Succesfully"
            });
        }
    }
}
