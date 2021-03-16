using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Livro
    {
        public long NumeroTombo { get; set; }       
        public string Isbn { get; set; }       
        public string Titulo { get; set; }       
        public string Genero { get; set; }       
        public DateTime DataPublicacao { get; set; }       
        public string Autor { get; set; }

        // IMPRESSÃO
        public override string ToString()
        {
            return "\t\t>>>Livro " + NumeroTombo + "<<<" +
                   "\n\nTítulo: " + Titulo + "\tAutor: " + Autor +
                   "\n\nGenero: " + Genero + "\tData Publicação: " + DataPublicacao.ToString("dd/MM/yyyy") +
                   "\n\nISBN: " + Isbn +
                   "\n\n------------------------------------------------------------------";
        }
    }
}
