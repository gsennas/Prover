using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Teste_Prover2.Services.EmailService;


namespace Teste_Prover2.Areas.Identity.Pages.Account
{
    public class RecuperarSenha : PageModel
    {
        private UserManager<IdentityUser> _userManager;
        private readonly IEmailSenders _emailSender;

        public RecuperarSenha(UserManager<IdentityUser> userManager, IEmailSenders emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public class DadosEmail
        {
            [Required(ErrorMessage = "Informe o Email")]
            [EmailAddress]
            [Display(Name = "Email")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }


        }
        [BindProperty]
        public DadosEmail Dados { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByNameAsync(Dados.Email);
            if (user != null)
            {


                string token = await _userManager.GeneratePasswordResetTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var urlReset = Url.Page("./RedefinirSenha", null, new { token }, Request.Scheme);
                StringBuilder msg = new StringBuilder();
                msg.Append("<h1> Profer: Recuperação de senha</h1>");
                msg.Append($"<p>Por favor, redefina sua senha <a href = '{HtmlEncoder.Default.Encode(urlReset)}'>clicando aqui</a></p>");
                msg.Append("<p>Atenciosamente,<br>Suporte Prover</p>");
                await _emailSender.SendEmailAsync(user.Email, "Recuperação de Senha", "", msg.ToString());
                return RedirectToPage("/EmailRecuperacaoEnviado");
            }
            else
            {
                return RedirectToPage("/Identity/Account/ForgotPasswordConfirmation");
            }
            return Page();
        }
        public void OnGet()
        {
        }
    }
}
