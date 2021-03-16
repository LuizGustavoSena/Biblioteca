using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cliente
    {
        public long IdCliente { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public String Telefone { get; set; }
        public Endereco endereco;

        // IMPRESSÃO
        public override string ToString()
        {
            return "\t\t>>> Cliente " + IdCliente + "<<<" +
                   "\n\nNome: " + Nome + "\tCPF: " + Cpf +
                   "\n\nData Nascimento: " + DataNascimento.ToString("dd/MM/yyyy") + "\tTelefone: " + Telefone +
                   "\n\nEndereço: " + endereco.ToString() +
                   "\n\n------------------------------------------------------------------";
        }
    }
}
