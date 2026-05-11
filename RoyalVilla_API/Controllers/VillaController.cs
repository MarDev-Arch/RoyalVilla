using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Data;
using RoyalVilla_API.Models;
using RoyalVilla_API.Models.DTO;
using System.Collections;

namespace RoyalVilla_API.Controllers
{
    [Route("api/villa")]
    [ApiController]
    [Authorize]
    public class VillaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        //end


        public VillaController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<Villa>>), StatusCodes.Status200OK),]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<IEnumerable<Villa>>> GetVillas()
        {
            return  Ok(await _db.Villa.ToListAsync());
            
        }


        [HttpGet("{id:int}")]
       // [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<Villa>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Villa>> GetVillaById(int id)
        {
            try
            {

                if (id <= 0)
                {
                    return BadRequest("Villa ID must be greater than zero.");
                }
                var villa = await _db.Villa.FirstOrDefaultAsync(u => u.Id == id);
                if (villa == null)
                {
                    return NotFound($"Villa with ID {id} was not found.");
                }
                return Ok(villa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while retrieving Villa with ID{id}: {ex.Message}");
            }
        }


        // GET: api/productsby/colors
        [HttpGet("color/{color}")]
        [ProducesResponseType(typeof(ActionResult<IEnumerable<Villa>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Villa>> GetColor(string color)
        {
            try
            {

                if (!color.Contains(color))
                {
                    return BadRequest(" Villa Color must be provided.");
                }
                var villa = await _db.Villa.Where(u => u.Color.ToLower() == color.ToLower()).ToListAsync();


                //var villa = await _db.Villa.FirstOrDefaultAsync(u => u.Color == color);
                if (villa == null || !villa.Any())
                {
                    return NotFound($"No Product villas found with color {color}.");
                }
                
                return Ok(villa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while retrieving Villa with color {color}: {ex.Message}");
            }
        }


        //Create Villa
        [HttpPost]
        [ProducesResponseType(typeof(ActionResult<VillaDTO>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<VillaDTO>>> CreateVilla(VillaCreateDTO villaDTO)
        {
            try
            {

                if (villaDTO == null)
                {
                    return BadRequest("Villa data must be provided.");
                }
                // declaring a new Villa object and mapping the properties from the DTO to the entity
                //mapper
                Villa villa = _mapper.Map<Villa>(villaDTO);
                //end
                // initializing the Villa object with the properties from the DTO manually
                //{
                //    Name = villaDTO.Name,
                //    Color = villaDTO.Color,
                //    Price = villaDTO.Price,
                //    Details = villaDTO.Details,
                //    Occupancy = villaDTO.Occupancy,
                //    CreatedDate = DateTime.Now
                //};
                //end
                await _db.Villa.AddAsync(villa);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(CreateVilla), new { id = villa.Id }, villa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while creating the Villa: {ex.Message}");
            }
        }




        //Updating Villa
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(ActionResult<VillaDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<VillaDTO>>> UpdateVilla(int id, VillaUpdateDTO villaDTO)
        {
            try
            {

                if (villaDTO == null)
                {
                    return BadRequest("Villa data must be provided.");
                }
                if(id != villaDTO.Id)
                {
                    return BadRequest("Villa ID mismatch.");
                }
                var existingVilla = await _db.Villa.FirstOrDefaultAsync(u => u.Id == id);
                if (existingVilla == null)
                {
                    return NotFound($"Villa with ID {id} was not found.");
                }

                // declaring a new Villa object and mapping the properties from the DTO to the entity
                //mapper
                _mapper.Map(villaDTO,existingVilla);
                existingVilla.UpdatedDate = DateTime.Now;
                //end
                // initializing the Villa object with the properties from the DTO manually
                //{
                //    Name = villaDTO.Name,
                //    Color = villaDTO.Color,
                //    Price = villaDTO.Price,
                //    Details = villaDTO.Details,
                //    Occupancy = villaDTO.Occupancy,
                //    CreatedDate = DateTime.Now
                //};
                //end
                await _db.SaveChangesAsync();
                return Ok(villaDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while Updating the Villa: {ex.Message}");
            }
            }



        //Delete Villa
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ActionResult<object>), StatusCodes.Status404NotFound)]
       
        public async Task<ActionResult<ApiResponse<VillaDTO>>> DeleteVilla(int id)
        {
            try
            {



                var existingVilla = await _db.Villa.FirstOrDefaultAsync(u => u.Id == id);
                if (existingVilla == null)
                {
                    return NotFound($"Villa with ID {id} was not found.");
                }
                _db.Villa.Remove(existingVilla);
                await _db.SaveChangesAsync();
                return Ok(new { success = true, message = "Villa removved from records" });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"An error occured while Deleting the Villa: {ex.Message}");
            }

        }






    }
}
