using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Threading.Tasks;

namespace Teste_Prover2.Services.EmailService
{
    public interface IEmailSenders
    {
        Task SendEmailAsync(string email, string assunto, string mensagemTexto, string mensagemHtml);
    }
    public class EmailSender : IEmailSenders
    {
        private readonly EmailSetup _emailSetup;

        public EmailSender(IOptions<EmailSetup> emailSetup)
        {
            _emailSetup = emailSetup.Value;
        }

        public async Task SendEmailAsync(string email, string assunto, string mensagemTexto, string mensagemHtml)
        {
            var mensagem = new MimeMessage();
            mensagem.From.Add(new MailboxAddress(_emailSetup.Remetente, _emailSetup.EmailRemetente));
            mensagem.To.Add(MailboxAddress.Parse(email));
            mensagem.Subject = assunto;
            var builder = new BodyBuilder { TextBody = mensagemTexto, HtmlBody = mensagemHtml };
            mensagem.Body = builder.ToMessageBody();
            try
            {
                var smtp = new SmtpClient();
               smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                
                await smtp.ConnectAsync(_emailSetup.EnderecoServidorEmail,_emailSetup.PortaServidorEmail).ConfigureAwait(false);
                await smtp.AuthenticateAsync(_emailSetup.EmailRemetente, _emailSetup.Senha).ConfigureAwait(false);
                await smtp.SendAsync(mensagem).ConfigureAwait(false);
                await smtp.DisconnectAsync(true).ConfigureAwait(false);
            }
            catch(Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
