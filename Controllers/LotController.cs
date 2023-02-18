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
    public class LotController : ControllerBase
    {
        private AppDbContext _authDbContext;

        public LotController(AppDbContext appDbContext)
        {
            _authDbContext = appDbContext;
        }

        [HttpPost("RegisterLocation")]
        public async Task<IActionResult> RegisterLocation([FromBody] Lot LotObj)
        {
            if (LotObj == null)
                return BadRequest();

            if (await CheckLotIdExistAsync(LotObj.LotId))
                return BadRequest(new { Message = "Lot ID Already Exist" });

            await _authDbContext.Lots.AddAsync(LotObj);
            await _authDbContext.SaveChangesAsync();
            return Ok(new { Message = "Lot Registered Successfully" });
        }
        private Task<bool> CheckLotIdExistAsync(int lotId)
            => _authDbContext.Lots.AnyAsync(x => x.LotId == lotId);

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Location>> GetAllLots()
        {
            return Ok(await _authDbContext.Lots.ToListAsync());
        }

        [HttpPut("updatelots")]
        public async Task<IActionResult> UpdateLot([FromBody] Lot LotObj)
        {
            if (LotObj == null)
                return BadRequest();

            var lot = await _authDbContext.Lots.AsNoTracking().FirstOrDefaultAsync(x => x.LotId == LotObj.LotId);
            if (lot == null)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Lot Not Found"
                });
            }
            else
            {
                _authDbContext.Lots.Update(LotObj);
                await _authDbContext.SaveChangesAsync();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Lot Updated Successfully"
                });
            }
        }

        [HttpDelete("deletelocation/{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] int id)
        {
            var lot = await _authDbContext.Lots.FirstOrDefaultAsync(x => x.LotId == id);
            if (lot == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Lot Not Found"
                });
            }
            else
            {
                _authDbContext.Remove(lot);
                await _authDbContext.SaveChangesAsync();
            }
            return Ok(new
            {
                StatusCode = 200,
                Message = "Lot Deleted Succesfully"
            });


        }
    }
}
