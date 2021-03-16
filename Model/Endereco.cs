namespace Model
{
    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }

        // IMPRESSÃO
        public override string ToString()
        {
            return Logradouro + " - " + Bairro + ", " + Cidade + " - " + Estado + ", " + Cep;
        }
    }
}