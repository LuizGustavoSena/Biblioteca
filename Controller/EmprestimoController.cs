using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class EmprestimoController
    {
        public static Emprestimo NumeroTomboEmprestado(List<Emprestimo> lista, long valor)
        {
            // VERIFICA SE NUMERO TOMBO LIVRO EXISTE E RETORNA EMPRESTIMO
            foreach (Emprestimo i in lista)
                if (i.NumeroTombo == valor && i.StatusEmprestimo == 1)
                    return i;
            return null;
        }
    }
}
