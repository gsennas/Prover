using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.WebUtilities;

using System.Windows;
using MySqlX.XDevAPI.Common;

namespace Teste_Prover2.Areas.Identity.Pages.Account
{
    public class RedefinirSenha : PageModel
    {
        private UserManager<IdentityUser> _userManager;

        public RedefinirSenha(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        
        public class DadosRedefinicaodeSenha
        {
            [Required(ErrorMessage = "O campo\"{0}\" é obrigatório")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required(ErrorMessage = "O campo\"{0}\" é obrigatório")]
            
            [DataType(DataType.Password)]
            public string Senha { get; set; }
            [Compare("Senha", ErrorMessage = "A senha de confirmação é diferente da Senha.")]
            public string ConfirmacaoSenha { get; set; }
            public string Token { get; set; }
            public IdentityResult Result { get; set; }
        }

        [BindProperty]
        public DadosRedefinicaodeSenha Dados { get; set; }
        public IActionResult OnGet(string token = null)
        {
            if (token == null)
            {
                return BadRequest("um Token deve ser informado para redefinir a senha");
            }
            else
            {
                Dados = new DadosRedefinicaodeSenha
                {
                    Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token))
                };
                return Page();
            }
           


        }
        public async Task<IActionResult> OnPostAsync()
    {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.FindByEmailAsync(Dados.Email);
            if(user == null)
            {
                return RedirectToPage("/Index");
            }
            var resul = await _userManager.ResetPasswordAsync(user, Dados.Token, Dados.Senha);
            if (resul.Succeeded)
            {
                return RedirectToPage("/Index");
            }
            foreach (var erro in resul.Errors)
            {
                ModelState.AddModelError(string.Empty, erro.Description);
            }

            return Page();

        }
    }
    
   
}



