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
using UserrrrrSon.Models.Edit;
using UserrrrrSon.Models.Get;
using UserrrrrSon.Models.models_;

namespace UserrrrrSon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        AppDbContext _context;
        IMapper _mapper;

        public PersonController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        /// <summary>
        /// Get All Person
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetAllPerson()
        {
            var persons = _context.Persons.Select(s => new PersonGet
            {
                ID = s.PersonId,
                Name = s.Name,
                Organization = s.Organization,
                Title = s.Title,
                Value = s.Value,
                ExpectedDate = s.ExpectedDate,
                Visible = s.Visible,
                Pipeline = s.Pipeline,
                ReferenceNumber = s.ReferenceNumber,
                Phones = _context.Phones.Where(p => p.PersonId == s.PersonId).Select(c => new ph
                {
                    Number = c.PhoneNumber,
                    Work = c.Work.Name
                }).ToList(),
                Emails = _context.Emails.Where(p => p.PersonId == s.PersonId).Select(c => new em
                {
                    Email = c.Emaill,
                    Work = c.Work.Name
                }).ToList(),
            });

            return Ok(persons);


        }




        /// <summary>
        /// Get Person By ID
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult> GetPersonByID(int id)
        {

            var person = _context.Persons.Where(x => x.PersonId == id)
                                                   .Select(s => new PersonGet
                                                   {
                                                       ID = s.PersonId,
                                                       Name = s.Name,
                                                       Organization = s.Organization,
                                                       Title = s.Title,
                                                       Value = s.Value,
                                                       ExpectedDate = s.ExpectedDate,
                                                       Visible = s.Visible,
                                                       Pipeline = s.Pipeline,
                                                       ReferenceNumber = s.ReferenceNumber,
                                                       Phones = _context.Phones.Where(p => p.PersonId == id).Select(c => new ph
                                                       {
                                                           Number = c.PhoneNumber,
                                                           Work = c.Work.Name
                                                       }).ToList(),
                                                       Emails = _context.Emails.Where(p => p.PersonId == id).Select(c => new em
                                                       {
                                                           Email = c.Emaill,
                                                           Work = c.Work.Name
                                                       }).ToList(),

                                                   });


            if (person != null)
            {
                return Ok(person);
            }
            return BadRequest("Person Not Found");
        }




        /// <summary>
        /// Create Person
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Create([FromBody] PersonDTO dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (dto != null)
                    {
                        Person person = new Person();

                        if (dto.Name != "string")
                        {
                            person.Name = dto.Name;
                        }
                        if (person.Name == null)
                        {
                            return BadRequest("Name is required");
                        }


                        if (dto.Organization != "string")
                        {
                            person.Organization = dto.Organization;
                        }
                        if (person.Organization == null)
                        {
                            return BadRequest("Organization is required");
                        }


                        if (dto.Title != "string")
                        {
                            person.Title = dto.Title;
                            if (person.Title == null)
                            {
                                return BadRequest("Title is required");
                            }
                        }

                        person.Value = dto.Value;

                        if (dto.Pipeline != "string")
                        {
                            person.Pipeline = dto.Pipeline;
                        }
                        if (person.Pipeline == null)
                        {
                            return BadRequest("Pipeline is required");
                        }


                        if (dto.ExpectedDate != "string")
                        {
                            person.ExpectedDate = dto.ExpectedDate;
                        }
                        if (person.ExpectedDate == null)
                        {
                            return BadRequest("ExpectedDate is required");
                        }


                        if (dto.Visible != "string")
                        {
                            person.Visible = dto.Visible;
                        }
                        if (person.Visible == null)
                        {
                            return BadRequest("Visible is required");
                        }

                        person.ExistingClient = dto.ExistingClient;

                        if (dto.Source != "string")
                        {
                            person.Source = dto.Source;
                        }
                        if (person.Source == null)
                        {
                            return BadRequest("Source is required");
                        }

                        var serviceDto = await _context.Services.FirstOrDefaultAsync(x => x.Name.ToUpper() == dto.Service.ToUpper());
                        if (serviceDto == null)
                        {
                            return BadRequest("Service not found");
                        }
                        person.ServiceId = serviceDto.ServiceId;
                        person.Priority = dto.Priority;


                        Random rnd = new Random();
                        int value = rnd.Next(10000, 100000);
                        var list = _context.Persons.Select(x => x.ReferenceNumber).ToList();
                        if (list.Contains(value))
                        {
                            value = rnd.Next(10000, 100000);
                        }
                        person.ReferenceNumber = value;

                        _context.Persons.Add(person);
                        await _context.SaveChangesAsync();


                        foreach (var item in dto.PhonesWorks)
                        {
                            var work = _context.Works.FirstOrDefault(x => x.Name.ToUpper() == item.Work.ToUpper());
                            if (work == null)
                            {
                                return BadRequest("Phone,Work not found");
                            }
                            var id = work.WorkId;

                            Phone phone = new Phone();
                            phone.PersonId = person.PersonId;
                            phone.PhoneNumber = item.PhoneNumber;
                            phone.WorkId = id;

                            _context.Phones.Add(phone);
                            await _context.SaveChangesAsync();
                        }


                        foreach (var item in dto.EmailsWorks)
                        {
                            var work = _context.Works.FirstOrDefault(x => x.Name.ToUpper() == item.Work.ToUpper());
                            if (work == null)
                            {
                                return BadRequest("Email,Work not found");
                            }
                            var id = work.WorkId;

                            Email email = new Email();
                            email.PersonId = person.PersonId;
                            email.Emaill = item.Email;
                            email.WorkId = id;

                            _context.Emails.Add(email);
                            await _context.SaveChangesAsync();
                        }

                        return Ok(person);
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
        /// update Person
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]/{personid}")]
        public async Task<ActionResult<PersonDTO>> Update(int personid, [FromBody] PersonEditDTO dto)
        {

            Person person = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == personid);

            if (person == null)
            {
                return BadRequest("Person not found");
            }

            if (person.Name != null)
            {
                if (person.Name != "string")
                {
                    person.Name = dto.Name;
                }
            }

            if (person.Organization != null)
            {
                if (person.Organization != "string")
                {
                    person.Organization = dto.Organization;
                }
            }

            if (person.Title != null)
            {
                if (person.Title != "string")
                {
                    person.Title = dto.Title;
                }
            }

            if (person.ExpectedDate != null)
            {
                if (person.ExpectedDate != "string")
                {
                    person.ExpectedDate = dto.ExpectedDate;
                }
            }

            if (person.Visible != null)
            {
                if (person.Visible != "string")
                {
                    person.Visible = dto.Visible;
                }
            }

            if (person.Pipeline != null)
            {
                if (person.Pipeline != "string")
                {
                    person.Pipeline = dto.Pipeline;
                }
            }

            foreach (var item in dto.PhoneNumbers)
            {
                var phone = await _context.Phones.FirstOrDefaultAsync(x => x.PhoneID == item.PhoneId && x.PersonId == personid);

                if (phone == null)
                {
                    return BadRequest("Phone not found");
                }

                phone.PhoneNumber = item.PhoneNumber;
                var work = _context.Works.FirstOrDefault(x => x.Name.ToUpper() == item.Work.ToUpper());
                if (work == null)
                {
                    return BadRequest("Phone,Work not found");
                }
                var id = work.WorkId;

                phone.WorkId = id;

                await _context.SaveChangesAsync();
            }

            foreach (var item in dto.Emails)
            {
                var email = await _context.Emails.FirstOrDefaultAsync(x => x.EmailID == item.EmailId && x.PersonId == personid);

                if (email == null)
                {
                    return BadRequest("Email not found");
                }

                email.Emaill = item.Email;
                var work = _context.Works.FirstOrDefault(x => x.Name.ToUpper() == item.Work.ToUpper());
                if (work == null)
                {
                    return BadRequest("Email,Work not found");
                }
                var id = work.WorkId;

                email.WorkId = id;

                await _context.SaveChangesAsync();
            }



            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
            return Ok(person);

        }




        /// <summary>
        /// Delete Person
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{personid}")]
        public async Task<ActionResult> DeletePerson(int personid)
        {
            Person person = await _context.Persons.FirstOrDefaultAsync(x => x.PersonId == personid);

            if (person == null)
            {
                return BadRequest("Person not found");
            }

            var phoneList = await _context.Phones.Where(x => x.PersonId == personid).ToListAsync();
            var emailList = await _context.Emails.Where(x => x.PersonId == personid).ToListAsync();

            foreach (var phone in phoneList)
            {
                _context.Phones.Remove(phone);
            }

            foreach (var email in emailList)
            {
                _context.Emails.Remove(email);
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();

            return Ok();
        }






    }
}
