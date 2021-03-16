using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Emprestimo
    {
        public long IdCliente { get; set; }
        public long NumeroTombo { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int StatusEmprestimo { get; set; }

        // IMPRESSÃO
        public override string ToString()
        {
            // AO INVES DE INFORMAR 1 OU 2 INFORMA O STATUS MAS NO SISTEMA É 1 OU 2
            string status = "Devolvido";
            if (StatusEmprestimo == 1)
                status = "Emprestado";

            return "\t\t>>>Empréstimo " + status + "<<<" +
                   "\n\nId Cliente: " + IdCliente + "\tNúmero Tombo Livro: " + NumeroTombo +
                   "\n\nData Emprestimo: " + DataEmprestimo.ToString("dd/MM/yyyy") + "\tData Devolução: " + DataDevolucao.ToString("dd/MM/yyyy") +
                   "\n\n----------------------------------------------------------------------";
        }
    }
}
