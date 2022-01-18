using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.Context;
using UserrrrrSon.Models.DTO2;
using UserrrrrSon.Models.models_;

namespace UserrrrrSon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        AppDbContext _context;
        IMapper _mapper;

        public ServiceController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        /// <summary>
        /// Get All Service
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Service>>> GetServices()
        {
            if (_context.Branches.Count() > 0)
            {
                return await _context.Services.ToListAsync();
            }
            return BadRequest("Service is empty");
        }



        /// <summary>
        /// Get Service By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult> GetServiceByID(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == id);

            if (service != null)
            {
                return Ok(service);
            }
            return BadRequest("Service is not found");
        }




        /// <summary>
        /// Get Branch By Name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<ActionResult> GetServiceByName(string name)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper());

            if (service != null)
            {
                return Ok(service);
            }
            return BadRequest("Service is not found");
        }




        /// <summary>
        /// Create Branch
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateService([FromBody] ServiceDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dto != null)
                    {
                        var service = _mapper.Map<Service>(dto);

                        var iss = await _context.Services.Where(x => x.Name.ToUpper() == dto.Name.ToUpper()).ToListAsync();

                        if (iss.Count > 0)
                        {
                            return BadRequest("This Service is exist");
                        }

                        _context.Services.Add(service);
                        await _context.SaveChangesAsync();
                        return Ok(service);
                    }
                }
                return BadRequest(dto);

            }
            catch (Exception ex)
            {
                throw new Exception(
                            $"Error State {ex}.");
            }
        }




        /// <summary>
        /// Update Branch
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<ActionResult<ServiceDTO>> UpdateService(int id, [FromBody] ServiceDTO dto)
        {
            try
            {
                Service service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == id);

                if (service == null)
                {
                    return BadRequest("Not found service");
                }

                var iss = await _context.Services.Where(x => x.Name.ToUpper() == dto.Name.ToUpper()).ToListAsync();

                if (iss.Count > 0)
                {
                    return BadRequest("This Service is exist");
                }

                service.Name = dto.Name;

                _context.Services.Update(service);
                await _context.SaveChangesAsync();
                return Ok(service);
            }
            catch (Exception ex)
            {
                throw new Exception(
                            $"Error State {ex}.");
            }

        }



        /// <summary>
        /// Update Branch
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult> DeleteService(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(x => x.ServiceId == id);

            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest("Error");
        }






    }
}
