using AngularLogin.Context;
using AngularLogin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepController : Controller
    {
        private AppDbContext _authDbContext;

        public RepController(AppDbContext appDbContext)
        {
            _authDbContext = appDbContext;
        }
        [HttpPost("RegisterRep")]
        public async Task<IActionResult> RegisterRep([FromBody] Rep RepObj)
        {
            if (RepObj == null)
                return BadRequest();

            if (await CheckRepIdExistAsync(RepObj.RepId))
                return BadRequest(new { Message = "Rep ID Already Exist" });

            await _authDbContext.Reps.AddAsync(RepObj);
            await _authDbContext.SaveChangesAsync();
            return Ok(new { Message = "Rep Registered Successfully" });
        }
        private Task<bool> CheckRepIdExistAsync(int RepId)
            => _authDbContext.Reps.AnyAsync(x => x.RepId == RepId);

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Location>> GetAllReps()
        {
            return Ok(await _authDbContext.Reps.ToListAsync());
        }

        [HttpPut("updateReps")]
        public async Task<IActionResult> UpdateRep([FromBody] Rep RepObj)
        {
            if (RepObj == null)
                return BadRequest();

            var rep = await _authDbContext.Reps.AsNoTracking().FirstOrDefaultAsync(x => x.RepId == RepObj.RepId);
            if (rep == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Rep Not Found"
                });
            }
            else
            {
                _authDbContext.Reps.Update(RepObj);
                await _authDbContext.SaveChangesAsync();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Rep Updated Successfully"
                });
            }
        }
    }
}
