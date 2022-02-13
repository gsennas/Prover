

namespace Teste_Prover2.Services.EmailService
{
    public class EmailSetup
    {
        public string Remetente { get; set; }
        public string EmailRemetente { get; set; }
        public string Senha { get; set; }
        public string EnderecoServidorEmail { get; set; }
        public int PortaServidorEmail { get; set; }
        public bool UsarSsl  { get; set; }
    }
}
