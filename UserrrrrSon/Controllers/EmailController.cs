using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserrrrrSon.Models.Authentication;

namespace UserrrrrSon.Controllers
{
    public class EmailController : Controller
    {


        private UserManager<AppUser> userManager;
        public EmailController(UserManager<AppUser> usrMgr)
        {
            userManager = usrMgr;
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Error");

            var result = await userManager.ConfirmEmailAsync(user, token);
            return Ok("ConfirmEmail");
        }

    }
}
