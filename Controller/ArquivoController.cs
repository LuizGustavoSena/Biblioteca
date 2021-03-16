using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ArquivoController
    {
        public static void Escrita(List<Cliente> lista) // ESCRITA DE CLIENTE
        {
            using (StreamWriter arquivo = new StreamWriter("CLIENTE.csv"))
            {
                // CABEÇALHO DO ARQUIVO
                arquivo.WriteLine("IdCliente;CPF;Nome;DataNascimento;Telefone;Logradouro;Bairro;Cidade;Estado;CEP");
                foreach (Cliente i in lista)
                {
                    // CORPO ARQUIVO COM DADOS
                    arquivo.WriteLine(i.IdCliente + ";" + i.Cpf + ";" + i.Nome + ";" + i.DataNascimento.ToString("dd/MM/yyyy") + ";" + i.Telefone + ";" +
                        i.endereco.Logradouro + ";" + i.endereco.Bairro + ";" + i.endereco.Cidade + ";" + i.endereco.Estado + ";" + i.endereco.Cep);
                }
            }
        }

        public static void Escrita(List<Livro> lista) // ESCRITA DE LIVRO
        {
            using (StreamWriter arquivo = new StreamWriter("LIVRO.csv"))
            {
                // CABEÇALHO DO ARQUIVO
                arquivo.WriteLine("NumeroTombo;ISBN;Titulo;Genero;DataPublicacao;Autor");
                foreach(Livro i in lista)
                {
                    // CORPO ARQUIVO COM DADOS
                    arquivo.WriteLine(i.NumeroTombo + ";" + i.Isbn + ";" + i.Titulo + ";" + i.Genero + ";" + i.DataPublicacao.ToString("dd/MM/yyyy") + ";" +
                        i.Autor);
                }
            }
        }

        public static void Escrita(List<Emprestimo> lista) // ESCRITA EMPRESTIMO
        {
            using (StreamWriter arquivo = new StreamWriter("EMPRESTIMO.csv"))
            {
                // CABEÇALHO DO ARQUIVO
                arquivo.WriteLine("IdCliente;NumeroTombo;DataEmprestimo;DataDevolucao;StatusEmprestimo");
                foreach(Emprestimo i in lista)
                {
                    // CORPO ARQUIVO COM DADOS
                    arquivo.WriteLine(i.IdCliente + ";" + i.NumeroTombo + ";" + i.DataEmprestimo.ToString("dd/MM/yyyy") + ";" + i.DataDevolucao.ToString("dd/MM/yyyy") + ";" + i.StatusEmprestimo);
                }
            }
        }

        public static void Leitura(List<Cliente> lista) // LEITURA DE CLIENTE
        {
            // SE O ARQUIVO EXISTIR
            if (File.Exists("CLIENTE.csv"))
            {
                using (StreamReader arquivo = new StreamReader("CLIENTE.csv", Encoding.UTF8))
                {
                    // VARIAVEIS
                    Endereco endereco;
                    Cliente cliente;
                    CultureInfo CultureBr = new CultureInfo(name: "pt-BR");

                    // LAÇO D=RNQUANTO EXISTIR ARQUIVO
                    while (!arquivo.EndOfStream)
                    {
                        // ARMAZENA A LINHA EM UM VETOR
                        string[] linha = arquivo.ReadLine().Split(';');

                        // CASO NÃO SEJA A PRIMEIRO LINHA
                        if(linha[0] != "IdCliente") { 
                            
                            // ESTANCIAMENTO DE ENDEREÇO
                            endereco = new Endereco()
                            {
                                Logradouro = linha[5],
                                Bairro = linha[6],
                                Cidade = linha[7],
                                Estado = linha[8],
                                Cep = linha[9]
                            };

                            // ESTANCIAMENTO CLIENTE
                            cliente = new Cliente()
                            {
                                IdCliente = long.Parse(linha[0]),
                                Cpf = linha[1],
                                Nome = linha[2],
                                DataNascimento = DateTime.ParseExact(linha[3], "d", CultureBr),
                                Telefone = linha[4],
                                endereco = endereco
                            };

                            //ADICIONANDO CLIENTE A LISTA CLIENTE
                            lista.Add(cliente);
                        }
                    }
                }
            }
        }

        public static void Leitura(List<Livro> lista) // LEITURA LIVRO
        {
            // SE O ARQUIVO EXISTIR
            if (File.Exists("LIVRO.csv"))
            {
                using (StreamReader arquivo = new StreamReader("LIVRO.csv", Encoding.UTF8))
                {
                    // VARIAVEIS
                    CultureInfo CultureBr = new CultureInfo(name: "pt-BR");
                    Livro livro;

                    // ENQUANTO ARQUIVO EXISTIR
                    while (!arquivo.EndOfStream)
                    {
                        // ARMAZENA LINHA EM UM VETOR
                        string[] linha = arquivo.ReadLine().Split(';');

                        // SE NÃO FOR A PRIMEIRA LINHA
                        if (linha[0] != "NumeroTombo")
                        {

                            // ESTACIAMENTO LIVRO
                            livro = new Livro()
                            {
                                NumeroTombo = long.Parse(linha[0]),
                                Isbn = linha[1],
                                Titulo = linha[2],
                                Genero = linha[3],
                                DataPublicacao = DateTime.ParseExact(linha[4], "d", CultureBr),
                                Autor = linha[5]
                            };

                            // ADICIONA LIVRO A LISTA LIVRO
                            lista.Add(livro);
                        }
                    }
                }
            }
        }

        public static void Leitura(List<Emprestimo> lista) // LEITURA EMPRESTIMO
        {
            // SE O ARQUIVO EXISTIR
            if (File.Exists("EMPRESTIMO.csv"))
            {
                using (StreamReader arquivo = new StreamReader("EMPRESTIMO.csv", Encoding.UTF8))
                {
                    // VARIAVEIS
                    Emprestimo emprestimo;
                    CultureInfo CultureBr = new CultureInfo(name: "pt-BR");

                    // ENQUANTO ARQUIVO EXISTIR
                    while (!arquivo.EndOfStream)
                    {
                        // ARMAZENA LINHA EM UM VETOR
                        string[] linha = arquivo.ReadLine().Split(';');

                        // SE NÃO FOR A PRIMEIRA LINHA
                        if (linha[0] != "IdCliente")
                        {

                            // ESTACIAMENTO DE EMPRESTIMO
                            emprestimo = new Emprestimo()
                            {
                                IdCliente = long.Parse(linha[0]),
                                NumeroTombo = long.Parse(linha[1]),
                                DataEmprestimo = DateTime.ParseExact(linha[2], "d", CultureBr),
                                DataDevolucao = DateTime.ParseExact(linha[3], "d", CultureBr),
                                StatusEmprestimo = int.Parse(linha[4])
                            };

                            // ADICIONA EMPRESTIMO A LISTA
                            lista.Add(emprestimo);
                        }
                    }
                }
            }
        }
    }
}
