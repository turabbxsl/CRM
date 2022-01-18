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
    public class InvoiceController : ControllerBase
    {

        AppDbContext _context;
        IMapper _mapper;

        public InvoiceController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Create(InvoiceDTO dto)
        {
            Invoice invoice = new Invoice();

            Random rnd = new Random();
            long value = rnd.Next(100000, 1000000);
            var list = await _context.Invoices.Select(x => x.ReferenceNumber).ToListAsync();
            if (list.Contains(value))
            {
                value = rnd.Next(100000, 1000000);
            }

            invoice.ReferenceNumber = value;
            invoice.Date = DateTime.Now.ToShortDateString();
            invoice.Address = dto.Address;
            invoice.BillTo = dto.BillTo;


            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            invoice.BankId = dto.BankId;
            await _context.SaveChangesAsync();

            invoice.BranchId = dto.BranchId;
            await _context.SaveChangesAsync();



            float total = 0;

            foreach (var item in dto.CalcDTOs)
            {
                Calc calc = new Calc();
                calc.Quantity = item.Quantity;
                calc.Description = item.Description;
                calc.Amount = item.Amount;
                calc.InvoiceId = invoice.Id;

                total += calc.Quantity * calc.Amount;

                _context.Calcs.Add(calc);
                await _context.SaveChangesAsync();

            }
            invoice.Total = total;



            await _context.SaveChangesAsync();
            total = 0;
            return Ok(invoice);
        }



        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetAllInvoice()
        {
            var invoice = _context.Invoices.Include(x => x.Branch).Select(s => new InvoiceGet
            {
                Address = s.Address,
                BillTo = s.BillTo,
                Date = s.Date,
                ReferenceNumber = s.ReferenceNumber,
                Total = s.Total,
                Branch = _context.Branches.FirstOrDefault(b => b.BranchId == s.BranchId).BranchName,
                Bank = _context.Banks.FirstOrDefault(b => b.Id == s.BankId).BankName,
                AccountName = s.Branch.AccountName,
                AccountNo = s.Branch.AccountNo.ToString(),
                calcGets = _context.Calcs.Where(p => p.InvoiceId == s.Id).Select(c => new CalcGet
                {
                    Quantity = c.Quantity,
                    Description = c.Description,
                    Amount = c.Amount
                }).ToList(),
            });

            if (invoice != null)
            {
                return Ok(invoice);
            }
            return BadRequest("Invoice is empty");
        }



        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult> GetInvoiceByID(int id)
        {
            var invoice = _context.Invoices.Where(x => x.Id == id).Include(x => x.Branch).Select(s => new InvoiceGet
            {
                Address = s.Address,
                BillTo = s.BillTo,
                Date = s.Date,
                ReferenceNumber = s.ReferenceNumber,
                Total = s.Total,
                Branch = _context.Branches.FirstOrDefault(b => b.BranchId == s.BranchId).BranchName,
                Bank = _context.Banks.FirstOrDefault(b => b.Id == s.BankId).BankName,
                AccountName = s.Branch.AccountName,
                AccountNo = s.Branch.AccountNo.ToString(),
                calcGets = _context.Calcs.Where(p => p.InvoiceId == s.Id).Select(c => new CalcGet
                {
                    Quantity = c.Quantity,
                    Description = c.Description,
                    Amount = c.Amount
                }).ToList(),
            });

            if (invoice != null)
            {
                return Ok(invoice);
            }
            return BadRequest("Invoice not found");
        }


    }
}
