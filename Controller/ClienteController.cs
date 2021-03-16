using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Controller
{
    public class ClienteController
    {
        public static bool CpfExistente(List<Cliente> lista, string valor) 
        {
            // VERIFICA SE CPF JÁ EXISTE NA LISTA RETORNO BOLEANO
            foreach (Cliente i in lista)
                if (i.Cpf.Equals(valor))
                    return true;
            return false;
        }

        public static long RetornaCLienteCpf(List<Cliente> lista, string valor)
        {
            // VERIFICA SE CPF JÁ EXISTE NA LISTA RETORNO CLIENTE
            foreach (Cliente i in lista)
                if (i.Cpf.Equals(valor))
                    return i.IdCliente;
            return -1;
        }
    }
}
