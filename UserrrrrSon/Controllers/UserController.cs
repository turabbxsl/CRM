using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UserrrrrSon.Models;
using UserrrrrSon.Models.Authentication;
using UserrrrrSon.Models.Context;
using UserrrrrSon.Models.DTO;
using UserrrrrSon.Models.DTO2;
using UserrrrrSon.Models.List;
using UserrrrrSon.Models.models_;
using UserrrrrSon.Models.Test;

namespace UserrrrrSon.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {

        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;
        private IPasswordHasher<AppUser> _passwordHasher;
        IConfiguration _configuration;
        IMapper _mapper;
        AppDbContext _context;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IConfiguration configuration, AppDbContext context, IPasswordHasher<AppUser> passwordHasher, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
            _passwordHasher = passwordHasher;
            _roleManager = roleManager;
        }


        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<ActionResult> GetUserByID(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }


        /// <summary>
        /// Registration User
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> Registration([FromBody] AppUserDTO dto)
        {
            try
            {
                if (dto != null)
                {
                    var user = _mapper.Map<AppUser>(dto);

                    var branchId = _context.Branches.Where(x => x.BranchName.ToLower() == dto.BranchName.ToLower()).Select(x => x.BranchId).FirstOrDefault();


                    if (branchId < 0)
                        return BadRequest(error: "Branch is not found");

                    user.BranchId = branchId;

                    var email = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

                    if (email != null)
                    {
                        return BadRequest(error: "This Email is Available");
                    }

                    var username = await _userManager.FindByNameAsync(dto.UserName);
                    if (username != null)
                    {
                        return BadRequest("This Username is Available");
                    }

                    IdentityResult result = await _userManager.CreateAsync(user, dto.Sifre);
                    if (result.Succeeded)
                    {

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var confirmationLink = Url.Action("ConfirmEmail", "User", new { token, email = user.Email }, Request.Scheme);
                        EmailHelper emailHelper = new EmailHelper();
                        bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);

                        await _userManager.AddToRoleAsync(user, "User");

                        UserProfile userProfile = new UserProfile();

                        string[] array = user.FullName.Split(' ');

                        userProfile.FirstName = array[0];
                        userProfile.LastName = array[1];

                        _context.UserProfiles.Add(userProfile);
                        _context.SaveChanges();

                        user.UserProfileId = userProfile.UserProfileId;
                        userProfile.Image = "Yoxdur";
                        _context.SaveChanges();

                        return Ok(user);
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

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Error");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return Ok("ConfirmEmail");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult> GetUserProfile(int userid)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userid);

            var id = user.UserProfileId;

            var userProfile = (from s in _context.UserProfiles
                               where s.UserProfileId == id
                               select new UserProfileList()
                               {
                                   FirstName = s.FirstName,
                                   LastName = s.LastName,
                                   Image = s.Image
                               }).FirstOrDefault<UserProfileList>();




            if (userProfile != null)
            {
                return Ok(userProfile);
            }
            return BadRequest();

        }


        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("[action]")]
        public async Task<ActionResult> Login([FromBody] LoginDTO model)
        {
            try
            {
                if (model != null)
                {
                    AppUser user = await _userManager.FindByEmailAsync(model.Email);

                    var role = await _userManager.GetRolesAsync(user);

                    if (user != null)
                    {
                        await _signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                        if (result.Succeeded)
                        {
                            await _userManager.ResetAccessFailedCountAsync(user); //Önceki hataları girişler neticesinde +1 arttırılmış tüm değerleri 0(sıfır)a çekiyoruz.
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);

                            var tokenDescription = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                                new Claim(ClaimTypes.Name, model.Email.ToString())
                                }),
                                Expires = DateTime.UtcNow.AddDays(7),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                            };

                            var token = tokenHandler.CreateToken(tokenDescription);
                            var tokenString = tokenHandler.WriteToken(token);

                            return Ok(tokenHandler.WriteToken(token));
                        }
                        else
                        {
                            await _userManager.AccessFailedAsync(user);

                            int failcount = await _userManager.GetAccessFailedCountAsync(user);
                            if (failcount == 3)
                            {
                                await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(1)));
                                ModelState.AddModelError("Locked", "Art arda 3 başarısız giriş denemesi etdiyiniz ucun hesabınız 1 dk kilitlenmişdir.");
                            }
                            else
                            {
                                if (result.IsLockedOut)
                                    ModelState.AddModelError("Locked", "Art arda 3 başarısız giriş denemesi etdiyiniz ucun hesabınız 1 dk kilitlenmiştir.");
                                else
                                    ModelState.AddModelError("NotUser2", "E-posta veya şifre yanlış.");
                            }

                        }
                    }
                    else
                    {
                        return BadRequest(error: "Email or password wrong");
                    }
                }
                return BadRequest(model);
            }
            catch (Exception ex)
            {
                throw new Exception(
                            $"Error State {ex}.");
            }

        }


        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetUserRoles([FromBody] GetUserRoles email)
        {
            var user = await _userManager.FindByEmailAsync(email.Email);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> RemoveUserFromRole([FromBody] RemoveUserFromRole removeUserFromRole)
        {
            var user = await _userManager.FindByEmailAsync(removeUserFromRole.Email);

            var rol = await _roleManager.RoleExistsAsync(removeUserFromRole.RoleName);

            if (rol)
            {
                return BadRequest("Role is not found");
            }

            if (user != null && removeUserFromRole.RoleName != "string" || removeUserFromRole.RoleName != null)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, removeUserFromRole.RoleName);

                if (result.Succeeded)
                {
                    return Ok(new { result = $"User {user.Email} removed from the {removeUserFromRole.RoleName} role" });
                }
                else
                {
                    return BadRequest(new { error = $"Error: Unable to removed user {user.Email} from the {removeUserFromRole.RoleName} role" });
                }
            }

            return BadRequest(new { error = "Unable to find user" });
        }


        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetUsers()
        {

            var list = await _userManager.Users.Select(x =>
             new UserList()
             {
                 FullName = x.FullName,
                 BranchName = x.Branch.BranchName,
                 Country = x.Country,
                 PhoneNumber = x.PhoneNumber == "string" || x.PhoneNumber == null ? "Empty" : x.PhoneNumber,
                 UserName = x.UserName,
                 Email = x.Email,
                 Sifre = x.PasswordHash
             }).ToListAsync();

            if (list.Count > 0)
            {
                return Ok(list);
            }
            else
            {
                return BadRequest("Users is Empty");
            }

        }


        /// <summary>
        /// Update User By ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<ActionResult> EditUser(string id, UserDetailDTO model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByIdAsync(id);

                var branchId = _context.Branches.Where(x => x.BranchName.ToLower() == model.BranchName.ToLower()).Select(x => x.BranchId).FirstOrDefault();

                if (model.BranchName != "string")
                {
                    if (branchId > 0)
                    {
                        user.BranchId = branchId;
                    }
                }

                if (model.Country != null)
                {
                    if (model.Country != "string")
                    {
                        user.Country = model.Country;
                    }
                }
                if (model.Email != null)
                {
                    if (model.Email != "string")
                    {
                        user.Email = model.Email;
                    }
                }
                if (model.UserName != null)
                {
                    if (model.UserName != "string")
                    {
                        user.UserName = model.UserName;
                    }
                }
                if (model.Email != null)
                {
                    if (model.Email != "string")
                    {
                        user.Email = model.Email;
                    }
                }
                if (model.Sifre != null)
                {
                    if (model.Sifre != "string")
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.Sifre);
                    }
                }
                IdentityResult result2 = await _userManager.UpdateAsync(user);
                if (!result2.Succeeded)
                {
                    return BadRequest(model);
                }
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, true);
            }
            return Ok(model);
        }

        [HttpPost]
        [Route("EditUserProfile/{userid}")]
        [AllowAnonymous]
        public async Task<ActionResult> EditUserProfile(int userid, IFormFile file, [FromForm] UserEditProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userid);

                var id = user.UserProfileId;

                var userprofile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserProfileId == id);

                UserProfile userP = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserProfileId == id);

                if (userP == null)
                {
                    return BadRequest("Not found User Profile");
                }

                if (model.FirstName != null)
                {
                    if (model.FirstName != "string")
                    {
                        userP.FirstName = model.FirstName;
                    }
                }

                if (model.LastName != null)
                {
                    if (model.LastName != "string")
                    {
                        userP.LastName = model.LastName;
                    }
                }

                if (file != null)
                {

                    if (userP.Image != null)
                    {
                        if (userP.Image != "Yoxdur")
                        {
                            FileInfo fileInfo = new FileInfo(userP.Image);
                            fileInfo.Delete();
                        }
                    }

                    var extension = Path.GetExtension(file.FileName);
                    var newImagename = Guid.NewGuid() + extension;

                    var location = Path.Combine(Directory.GetCurrentDirectory(), "Image/", newImagename);
                    var stream = new FileStream(location, FileMode.Create);

                    await file.CopyToAsync(stream);
                    userP.Image = location;
                }

                try
                {
                    _context.UserProfiles.Update(userP);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return BadRequest("Error");
                    throw;
                }




            }
            return Ok();
        }

        /*  [HttpDelete]
          public async Task<ActionResult> DeleteUserProfileImage(int userid)
          {

              var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userid);
              var id = user.UserProfileId;

              var userprofile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserProfileId == id);

              userprofile.Image == null;


          }*/



        /// <summary>
        /// Delete User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("[action]/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var idd = user.UserProfileId;

                var userprofile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserProfileId == idd);
                _context.UserProfiles.Remove(userprofile);

                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok();
                }

            }
            return BadRequest();
        }


    }
}
