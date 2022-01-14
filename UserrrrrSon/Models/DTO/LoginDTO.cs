using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserrrrrSon.Models.DTO
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "Lütfen e-posta adresini boş geçmeyiniz.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen uygun formatta e-posta adresi giriniz.")]
        [Display(Name = "E-Posta ")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Xahis edirik şifreyi boş buraxmayin.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

    }
}
