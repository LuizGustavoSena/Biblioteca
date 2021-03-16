using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class LivroController
    {
        public static bool IsbnExiste(List<Livro> lista, string valor) // VERIFICA SE JÁ EXIST ISBN NA LISTA
        {
            foreach (Livro i in lista)
                if (i.Isbn.Equals(valor))
                    return true;
            return false;
        }

        public static bool NumeroTomboExiste(List<Livro> lista, long valor) // VERIFICA SE EXISTE NUMERO TOMBO NA LISTA
        {
            foreach (Livro i in lista)
                if (i.NumeroTombo.Equals(valor))
                    return true;
            return false;
        }
    }
}
