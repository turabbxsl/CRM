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
using UserrrrrSon.Models.Get;
using UserrrrrSon.Models.models_;

namespace UserrrrrSon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {

        AppDbContext _context;
        IMapper _mapper;

        public BankController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetAllBank()
        {
            var banks = _context.Banks.Select(s => new GetBank
            {
                Id = s.Id,
                BankName = s.BankName
            });

            if (banks != null)
            {
                return Ok(banks);
            }
            return BadRequest("Invoice is empty");
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetBranchesOfBank(int id)
        {

            var bank = await _context.Banks.FirstOrDefaultAsync(x => x.Id == id);
            if (bank == null)
            {
                return BadRequest("Bank not found");
            }

            var branches = await _context.Branches.Where(x => x.BankId == id)
                                                   .Select(s => new BankBranchGet
                                                   {
                                                       AccountName = s.AccountName,
                                                       AccountNo = s.AccountNo,
                                                       BranchName = s.BranchName
                                                   }).ToListAsync();

            if (id == 0)
            {
                return BadRequest("ID required");
            }

            return Ok(branches);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateBank([FromBody] BankDTO bankDTO)
        {

            Bank bank = new Bank();
            bank.BankName = bankDTO.BankName;

            _context.Banks.Add(bank);
            await _context.SaveChangesAsync();

            return Ok(bank.BankName);
        }

        [HttpPut]
        [Route("[action]/id")]
        public async Task<ActionResult> UpdateBank(int id, [FromBody] BankDTO bankDTO)
        {

            var bank = await _context.Banks.FirstOrDefaultAsync(x => x.Id == id);
            if (bank == null)
            {
                return BadRequest("Bank Not Found");
            }

            bank.BankName = bankDTO.BankName;

            await _context.SaveChangesAsync();

            return Ok("Bank is Success");
        }






        [HttpDelete]
        [Route("[action]/id")]
        public async Task<ActionResult> DeleteBank(int id)
        {

            var bank = await _context.Banks.FirstOrDefaultAsync(x => x.Id == id);
            if (bank == null)
            {
                return BadRequest("Bank Not Found");
            }

            _context.Banks.Remove(bank);
            await _context.SaveChangesAsync();

            return Ok("Bank is Delete");
        }


    }
}
