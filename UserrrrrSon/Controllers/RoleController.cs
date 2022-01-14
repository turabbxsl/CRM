using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.Authentication;
using UserrrrrSon.Models.Context;
using UserrrrrSon.Models.DTO;

namespace UserrrrrSon.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RoleController : ControllerBase
    {
        readonly RoleManager<AppRole> _roleManager;
        readonly UserManager<AppUser> _userManager;
        AppDbContext _context;
        IMapper _mapper;

        public RoleController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IMapper mapper, AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }


        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetRoles()
        {
            var roles = await _context.Users.ToListAsync();

            if (roles.Count > 0)
            {
                return Ok(roles);
            }
            else
            {
                return BadRequest("Roles is Empty");
            }

        }



        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="roledto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Create([FromBody] RoleDTO roledto)
        {
            var role = _mapper.Map<AppRole>(roledto);

            var res = await _roleManager.RoleExistsAsync(role.Name);

            if (res)
            {
                return BadRequest("Role is Exist");
            }

            IdentityResult result = await _roleManager.CreateAsync(new AppRole { Name = role.Name });
            if (result.Succeeded)
            {
                return Ok(role);
            }
            return BadRequest(roledto);
        }


        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="roledto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> UpdateRole(RoleDTO roledto, string id)
        {
            IdentityResult result = null;
            if (id != null)
            {
                if (roledto != null)
                {
                    if (roledto.Name != "string")
                    {
                        var res = await _roleManager.RoleExistsAsync(roledto.Name);
                        if (res)
                        {
                            return BadRequest("Role is Exist");
                        }
                        var roledt = _mapper.Map<AppRole>(roledto);
                        AppRole role = await _roleManager.FindByIdAsync(id);

                        if (role == null)
                        {
                            return BadRequest("Don't found Role this ID");
                        }

                        role.Name = roledt.Name;
                        result = await _roleManager.UpdateAsync(role);
                    }
                }
            }

            if (result.Succeeded)
            {
                return Ok(roledto);
            }
            return BadRequest();
        }



        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteRole(string name)
        {
            AppRole role = await _roleManager.FindByNameAsync(name);
            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }



        /// <summary>
        /// Role Assign To User
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="rolename"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]/{userid}/{rolename}")]
        public async Task<ActionResult> RoleAssign(string userid, string rolename)
        {

            AppUser user = await _userManager.FindByIdAsync(userid);
            AppRole role = await _roleManager.FindByNameAsync(rolename);

            if (user == null)
            {
                return BadRequest("User not Found");
            }
            if (role == null)
            {
                return BadRequest("Role not Found");
            }

            var us = _context.UserRoles.Where(x=>x.UserId == user.Id).FirstOrDefault();
            var rol = _roleManager.Roles.Where(x=>x.Id == us.RoleId).FirstOrDefault();

            var linq = from r in _roleManager.Roles
                       where r.Id == us.RoleId
                       select r.Name;


            IdentityResult res = await _userManager.RemoveFromRoleAsync(user,rol.Name);

            IdentityResult result = await _userManager.AddToRoleAsync(user, rolename);

            return Ok();
        }


    }
}
