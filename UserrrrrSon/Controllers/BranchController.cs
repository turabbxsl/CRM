﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.Context;
using UserrrrrSon.Models.DTO;
using UserrrrrSon.Models.models_;

namespace UserrrrrSon.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BranchController : ControllerBase
    {

        AppDbContext _context;
        IMapper _mapper;

        public BranchController(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        /// <summary>
        /// Get All Branch
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Branch>>> GetBranches()
        {
            if (_context.Branches.Count() > 0)
            {
                return await _context.Branches.ToListAsync();
            }
            return BadRequest();
        }


        /// <summary>
        /// Get Branch By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult> GetBranchByID(int id)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(x => x.BranchId == id);

            if (branch != null)
            {
                return Ok(branch);
            }
            return BadRequest();
        }




        /// <summary>
        /// Get Branch By Name
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{name}")]
        public async Task<ActionResult> GetBranchByName(string name)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(x => x.BranchName == name);

            if (branch != null)
            {
                return Ok(branch);
            }
            return BadRequest();
        }




        /// <summary>
        /// Create Branch
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Create([FromBody] BranchDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dto != null)
                    {
                        var branch = _mapper.Map<Branch>(dto);
                        _context.Branches.Add(branch);
                        await _context.SaveChangesAsync();
                        return Ok(branch);
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
        public async Task<ActionResult<BranchDTO>> update(int id, [FromBody] BranchDTO dto)
        {
            try
            {
                Branch branch = await _context.Branches.FirstOrDefaultAsync(x => x.BranchId == id);

                if (branch == null)
                {
                    return BadRequest("Not found branch");
                }

                if (dto.BranchName != null)
                {
                    if (dto.BranchName != "string")
                    {
                        branch.BranchName = dto.BranchName;
                    }
                }
                if (dto.BranchCode != null)
                {
                    if (dto.BranchCode != "string")
                    {
                        branch.BranchCode = dto.BranchCode;
                    }
                }
                if (dto.BranchHead != null)
                {
                    if (dto.BranchCode != "string")
                    {
                        branch.BranchCode = dto.BranchCode;
                    }
                }
                if (dto.Address != null)
                {
                    if (dto.Address != "string")
                    {
                        branch.Address = dto.Address;
                    }
                }
                if (dto.State != null)
                {
                    if (dto.State != "string")
                    {
                        branch.State = dto.State;
                    }
                }
                if (dto.City != null)
                {
                    if (dto.City != "string")
                    {
                        branch.City = dto.City;
                    }
                }
                if (dto.PostalCode != null)
                {
                    if (dto.PostalCode != "string")
                    {
                        branch.PostalCode = dto.PostalCode;
                    }
                }
                if (dto.PhoneNumber != null)
                {
                    if (dto.PhoneNumber != "string")
                    {
                        branch.PhoneNumber = dto.PhoneNumber;
                    }
                }
                if (dto.Telephone != null)
                {
                    if (dto.Telephone != "string")
                    {
                        branch.Telephone = dto.Telephone;
                    }
                }
                if (dto.BankName != null)
                {
                    if (dto.BankName != "string")
                    {
                        branch.BankName = dto.BankName;
                    }
                }
                if (dto.Country != null)
                {
                    if (dto.Country != "string")
                    {
                        branch.Country = dto.Country;
                    }
                }

                _context.Branches.Update(branch);
                await _context.SaveChangesAsync();
                return Ok(branch);
            }
            catch (Exception ex)
            {
                throw new Exception(
                            $"Error State {ex}.");
            }

        }



        /// <summary>
        /// Delete Branch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult> DeleteBranch(int id)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(x => x.BranchId == id);

            if (branch != null)
            {
                _context.Branches.Remove(branch);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }


    }
}
