using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    class Program
    {
        static void Main()
        {
            // VARIAVEIS
            byte op;

            // LSITAS
            List<Cliente> listaCliente = new List<Cliente>();
            List<Livro> listaLivro = new List<Livro>();
            List<Emprestimo> listaEmprestimo = new List<Emprestimo>();

            // LEITURA DE ARQUIVOS
            ArquivoController.Leitura(listaCliente);
            ArquivoController.Leitura(listaLivro);
            ArquivoController.Leitura(listaEmprestimo);

            // LAÇO PROGRAMA
            do
            {
                op = MenuPrincipal(); // CHAMA MENU RETORNA OPÇÃO
                
                Console.Clear(); // LIMPA TELA CONSOLE

                switch (op)
                {
                    case 1: // CADASTRO DE CLIENTE

                        LerCliente(listaCliente); // CADASTRA CLIENTE ADICIONA A LISTA E ARQUIVO

                        break;
                    case 2: // CADASTRO DE LIVRO

                        LerLivro(listaLivro); // CADASTRA LIVRO ADICIONA A LISTA E ARQUIVO

                        break;
                    case 3: // EMPRESTIMO

                        LerEmprestimo(listaCliente, listaLivro, listaEmprestimo); // CADASTRA TODOS CAMPOS DE EMRPESTIMO
                         
                        break;

                    case 4: // DEVOLUÇÃO

                        LerDevolucao(listaEmprestimo);
                         
                        break;
                    case 5: // RELATORIO DE EMPRESTIMOS E DEVOLUÇÕES

                        Relatorio(listaCliente, listaLivro, listaEmprestimo); // FUNÇÃO RELATÓRIO

                        break;
                }
            } while (op != 0);
        }

        static byte MenuPrincipal() // FUNÇÃO MENU PRINCIPAL
        {
            string opcao;

            Console.WriteLine("\n---------->>>MENU PRINCIPAL<<<----------" +
                                "\n1 - Cadastro de Cliente" +
                                "\n2 - Cadastro de Livro" +
                                "\n3 - Emprestimo de Livro" +
                                "\n4 - Devolução de Livro" +
                                "\n5 - Relatório de Empréstimo e Devoluções" +
                                "\n0 - Sair" +
                                "\n----------------------------------------");
            opcao = Console.ReadLine();

            // TRATAMENTO DE ERRO
            if (byte.TryParse(opcao, out byte op))
                return op;
            return MenuPrincipal();
        }

        static void LerCliente(List<Cliente> lista) // FUNÇÃO ESCRITA DE CLIENTE
        {
            // VARIAVEIS
            string cpf, nome, telefone, logradouro, bairro, cidade, estado, cep;
            long idCliente;
            bool err;
            DateTime dataNascimento = DateTime.Now;
            Endereco endereco;
            Cliente cliente;
            CultureInfo CultureBr = new CultureInfo(name: "pt-BR");

            // LOCALIZAÇÃO
            Console.WriteLine("\n>>> CADASTRO CLIENTE<<<\n");

            do
            { // LAÇO TRATAMENTO DE CPF (REPETIDO E VALIDO)
                Console.Write("CPF do Cliente: ");
                cpf = Console.ReadLine();
                
                if (ClienteController.CpfExistente(lista, cpf)) { // VERIFICA SE JÁ EXISTE NA LISTA
                    Console.WriteLine("CPF já cadastrado.\nPrecione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear(); // LIMPA TELA
                    return;
                }
            } while (cpf == "");

            do
            { //LAÇO TRATA NOME VAZIO
                Console.Write("Nome do Cliente: ");
                nome = Console.ReadLine();
            } while (nome == "");

            do
            {
                err = false;
                // ARMAZENA DATA DE NASCIMENTO
                try
                {
                    Console.Write("Data de Nascimento do Cliente (dd/mm/yyyy): ");
                    dataNascimento = DateTime.ParseExact(Console.ReadLine(), "d", CultureBr);
                    if ((Convert.ToDateTime(dataNascimento)) > (Convert.ToDateTime(DateTime.Now)))
                    {
                        Console.WriteLine("Data deve ser menor que a data atual");
                        err = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Informe a data (dd/mm/yyyy)");
                    err = true;
                }
            } while (err);

            do
            { // LAÇO TRATA TELEFONE VAZIO
                Console.Write("Telefone do Cliente: ");
                telefone = Console.ReadLine();
            } while (telefone == "");

            do
            { // LAÇO TRATA LOGRADOURO VAZIO
                Console.Write("Logradouro do Cliente e Número: ");
                logradouro = Console.ReadLine();
            } while (logradouro == "");

            do
            { // LAÇO TRATA BAIRRO VAZIO
                Console.Write("Bairro do Cliente: ");
                bairro = Console.ReadLine();
            } while (bairro == "");

            do
            { // LAÇO TRATA CIDADE VAZIA
                Console.Write("Cidade do Cliente: ");
                cidade = Console.ReadLine();
            } while (cidade == "");

            do
            { // LAÇO TRATA ESTADO VAZIO
                Console.Write("Estado do Cliente: ");
                estado = Console.ReadLine();
            } while (estado == "");

            do
            { // LAÇO TRATA CEP VAZIO
                Console.Write("CEP do Cliente: ");
                cep = Console.ReadLine();
            } while (cep == "");

            // ESTANCIA ENDEREÇO
            endereco = new Endereco()
            {
                Logradouro = logradouro,
                Bairro = bairro,
                Cidade = cidade,
                Estado = estado,
                Cep = cep
            };

            if (lista.Count == 0)
                idCliente = 0;
            else
                idCliente = lista[lista.Count - 1].IdCliente + 1;
            // RETORNA CLIENTE ESTANCIADO
            cliente = new Cliente()
            {
                IdCliente = idCliente,
                Cpf = cpf,
                Nome = nome,
                DataNascimento = dataNascimento,
                Telefone = telefone,
                endereco = endereco
            };

            // ADICIONA CLIENTE NA LISTA CLIENTE
            lista.Add(cliente);

            // ESCREVE A LISTA NO ARQUIVO
            ArquivoController.Escrita(lista);

            Console.Clear();// LIMPA TELA

            Console.WriteLine("\n>>>CLIENTE CADASTRADO<<<\nPrecione qualquer tecla para continuar...");
            Console.ReadLine();

            Console.Clear();
        }

        static void LerLivro(List<Livro> lista)
        {
            // VARIAVEIS
            string isbn, titulo, genero, autor;
            bool err;
            long numeroTombo;
            DateTime dataPublicacao = DateTime.Now;
            Livro livro;
            CultureInfo CultureBr = new CultureInfo(name: "pt-BR");

            // LOCALIZAÇÃO
            Console.WriteLine("\n>>>CADASTRO LIVRO<<<\n");

            do
            { // LAÇO TRATA ISBN REPETIDO
                Console.Write("ISBN do Livro: ");
                isbn = Console.ReadLine();
                if(LivroController.IsbnExiste(lista, isbn)) // CASI TENHA ISBN REPETIDO NA LISTA
                {
                    Console.WriteLine("Livro já cadastrado.\nPrecione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear(); // LIMPA TELA
                    return;
                }
            } while (isbn == "");

            do
            { // LAÇO TRATA TITULO VAZIO
                Console.Write("Titulo do Livro: ");
                titulo = Console.ReadLine();
            } while (titulo == "");

            do
            { // LAÇO TRATA GENERO VAZIO
                Console.Write("Gênero do Livro: ");
                genero = Console.ReadLine();
            } while (genero == "");

            do
            { // LAÇO TRATA DATA POSTERIOR A DATA ATUAL
                err = false;
                try
                {
                    Console.Write("Data de Publicacao do Livro (dd/mm/yyyy): ");
                    dataPublicacao = DateTime.ParseExact(Console.ReadLine(), "d", CultureBr);
                    if ((Convert.ToDateTime(dataPublicacao)) > (Convert.ToDateTime(DateTime.Now)))
                    {
                        Console.WriteLine("Informe uma data anterior a data de hoje");
                        err = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Informe a data (dd/mm/yyyy)");
                    err = true;
                }
            } while (err);

            do
            { // LAÇO TRATA AUTOR VAZIO
                Console.Write("Autor do Livro: ");
                autor = Console.ReadLine();
            } while (autor == "");


            if (lista.Count == 0)
                numeroTombo = 0;
            else
                numeroTombo = lista[lista.Count - 1].NumeroTombo + 1;

            // IMPRIMI NÚMERO DO TOMBO PARA BIBLIOTECÁRIO ADICIONAR AO LIVRO FÍSICO
            Console.WriteLine("\n>>>Número Tombo do Livro: " + numeroTombo + "<<<\n");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();

            Console.Clear(); // LIMPA TELA

            // RETORNA LIVRO ESTANCIADO
            livro = new Livro()
            {
                NumeroTombo = numeroTombo,
                Isbn = isbn,
                Titulo = titulo,
                Genero = genero,
                DataPublicacao = dataPublicacao,
                Autor = autor
            };

            // ADICIONA LIVRO NA LISTA LIVRO
            lista.Add(livro);

            // ESCREVE A LISTA NO ARQUIVO
            ArquivoController.Escrita(lista);

            Console.Clear(); // LIMPA TELA

            Console.WriteLine("\n>>>LIVRO CADASTRADO<<<\nPrecione qualquer tecla para continuar...");
            Console.ReadLine();

            Console.Clear();
        }

        static void LerEmprestimo(List<Cliente> listaCliente, List<Livro> listaLivro, List<Emprestimo> listaEmprestimo)
        {
            // VARIAIVES
            string cpf, numeroTombo, op;
            long idCliente;
            bool err;
            DateTime dataDevolucao = DateTime.Now;
            Emprestimo emprestimo;
            CultureInfo CultureBr = new CultureInfo(name: "pt-BR");

            // LOCALIZAÇÃO
            Console.WriteLine("\n>>>CADASTRO EMPRESTIMO<<<\n");

            do
            { // LAÇO TRATA NÚMERO TOMBO DE LIVRO
                err = false;
                Console.Write("Informe o Número Tombo do Livro: ");
                numeroTombo = Console.ReadLine();

                // SE EXISTIR LIVRO NA LISTA DE EMPRESTADO E ESTAR COM STATUS EMPRESTADO RETORNA O OBJETO
                emprestimo = EmprestimoController.NumeroTomboEmprestado(listaEmprestimo, long.Parse(numeroTombo));

                // CASO NÃO EXISTIR NÚMERO TOMBO DE LIVRO OU ELE ESTAR EMPRESTADO
                if (!LivroController.NumeroTomboExiste(listaLivro, long.Parse(numeroTombo)) || emprestimo != null) 
                {
                    // CAMINHOS PARA USUARIO ESCOLHER
                    Console.WriteLine("Livro indisponível para empréstimo.\n1 - Digitar Número Tombo novamente" +
                                    "\n2 - Cadastrar Livro\n3 - Voltar ao Menu Principal ");
                    op = Console.ReadLine();

                    switch (op)// SWITCH DE OPÇÕES PARA SABER QUAL CAMINHO USUARIO ESCOLHEU
                    {
                        case "2": // CADASTRO CLIENTE
                            Console.Clear(); // LIMPA TELA
                            LerLivro(listaLivro);
                            err = true; // INFORMAR O CPF NOVAMENTE POREM COM CPF JÁ CADASTRADO
                            Console.WriteLine("\n>>>CADASTRO EMPRESTIMO<<<\n"); // LOCALIZAÇÃO
                            break;
                        case "3": // VOLTAR MENU PRINCIPAL
                            Console.Clear(); // LIMPA TELA
                            return;
                        default: // DIGITAR CPF NOVAMENTE OU OUTRO NÚMERO/LETRA NÃO INFORMADO NO MENU
                            err = true;
                            break;
                    }
                }
            } while (err);

            do
            {
                err = false;

                // INFORMA CPF
                Console.Write("Informe o CPF do Cliente: ");
                cpf = Console.ReadLine();

                // RETORNA ID DE CLIENTE QUE TEM ESSE CPF
                idCliente = ClienteController.RetornaCLienteCpf(listaCliente, cpf);

                // CASO CPF NÃO EXISTA 
                if (idCliente == -1) 
                {
                    // CAMINHOS CASO CPF NÃO RECONHECIDO
                    Console.WriteLine("Cliente não cadastrado.\n1 - Digitar CPF novamente" +
                                    "\n2 - Cadastrar Cliente\n3 - Voltar ao Menu Principal ");
                    op = Console.ReadLine();

                    switch (op)// SWITCH DE OPÇÕES PARA SABER QUAL CAMINHO USUARIO ESCOLHEU
                    {
                        case "2": // CADASTRO CLIENTE
                            Console.Clear(); // LIMPA TELA
                            LerCliente(listaCliente);
                            err = true; // INFORMAR O CPF NOVAMENTE POREM COM CPF JÁ CADASTRADO
                            Console.WriteLine("\n>>>CADASTRO EMPRESTIMO<<<\n"); // LOCALIZAÇÃO
                            Console.WriteLine("Informe o Número Tombo do Livro: " + numeroTombo); // LOCALIZAÇÃO
                            break;
                        case "3": // VOLTAR MENU PRINCIPAL
                            Console.Clear(); // LIMPA TELA
                            return;
                        default: // DIGITAR CPF NOVAMENTE OU OUTRO NÚMERO/LETRA NÃO INFORMADO NO MENU
                            err = true;
                            break;
                    }
                }
            } while (err);

            // ARMAZENA DATA DE DEVOLUÇÃO
            do
            {
                err = false;
                try
                {
                    Console.Write("Data de Devolução (dd/mm/yyyy): ");
                    dataDevolucao = DateTime.ParseExact(Console.ReadLine(), "d", CultureBr);
                    if ((Convert.ToDateTime(dataDevolucao)) < (Convert.ToDateTime(DateTime.Now)))
                    {
                        Console.WriteLine("Informe uma data posterior a data atual");
                        err = true;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Informe a data (dd/mm/yyyy)");
                    err = true;
                }
            } while (err);
            

            // ESTANCIA EMPRESTIMO ESTANCIADO
            emprestimo = new Emprestimo()
            {
                IdCliente = idCliente,
                NumeroTombo = long.Parse(numeroTombo),
                DataEmprestimo = DateTime.Now,
                DataDevolucao = dataDevolucao,
                StatusEmprestimo = 1
            };

            // ADICIONA O EMPRESTIMO NA LISTA EMPRESTIMO
            listaEmprestimo.Add(emprestimo);

            // ADICIONA LISTA NO ARQUIVO
            ArquivoController.Escrita(listaEmprestimo);

            Console.Clear(); // LIMPA TELA

            Console.WriteLine("\n>>>EMPRESTIMO CADASTRADO<<<\nPrecione qualquer tecla para continuar...");
            Console.ReadLine();

            Console.Clear();
        }

        static void LerDevolucao(List<Emprestimo> listaEmprestimo)
        {
            // VARIAVEIS
            string numeroTombo;
            Emprestimo emprestimo;
            const double multa = 0.10;
            int diaAtraso;
            double valorAtraso;

            // LOCALIZAÇÃO
            Console.WriteLine("\n>>>CADASTRO DEVOLUÇÃO<<<\n");

            do
            { // LAÇO TRATA NUMERO TOMBO VAZIO
                Console.Write("Número do Tombo do Livro: ");
                numeroTombo = Console.ReadLine();
            } while (numeroTombo == "");


            // RETORNA OBJETO EMPRESTIMO COM STATUS EMPRESTADO O LIVRO INFORMADO PELO NUMERO TOMBO
            emprestimo = EmprestimoController.NumeroTomboEmprestado(listaEmprestimo, long.Parse(numeroTombo));
            

            // CASO NÃO ENCONTRAR LIVRO OU O LIVRO ESTA COMO "DEVOLVIDO"
            if (emprestimo == null) { 
                Console.WriteLine("Livro não encontrado para devolução.\nPrecione qualquer tecla para voltar ao Menu Principal...");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            // CALCULA DIAS DE ATRASO
            diaAtraso = (int)DateTime.Now.Subtract(emprestimo.DataDevolucao).TotalDays;

            if (diaAtraso <= 0)
                Console.WriteLine("Entregue no Prazo.");
            else {
                valorAtraso = diaAtraso * multa; // CALCULA MULTA 0,10 POR DIA
                Console.WriteLine("Multa de R$ " + valorAtraso);
            }

            Console.WriteLine("Precione qualquer tecla para continuar...");
            Console.ReadLine();

            // ATUALIZA SITUAÇÃO DO EMRPESTIMO
            emprestimo.StatusEmprestimo = 2;
            emprestimo.DataDevolucao = DateTime.Now;

            // ATUALIZA A LISTA
            ArquivoController.Escrita(listaEmprestimo);

            // LIMPA TELA
            Console.Clear();

            Console.WriteLine("\n>>>DEVOLUÇÃO CADASTRADA<<<\nPrecione qualquer tecla para continuar...");
            Console.ReadLine();

            Console.Clear();
        }

        public static void Relatorio(List<Cliente> listaCliente, List<Livro> listaLivro, List<Emprestimo> listaEmprestimo)
        {
            // VARIAVEIS
            Cliente cliente;
            Livro livro;
            List<String> emprestado = new List<string>();
            List<String> devolvido = new List<string>();

            // LAÇO RODA TODA LISTA DE EMPRESTIMO
            foreach(Emprestimo i in listaEmprestimo)
            {
                // RETORNA O CLIENTE DO EMPRESTIMO "i"
                cliente = listaCliente.Find(x => x.IdCliente == i.IdCliente);

                // RETORNA O LIVRO DO EMPRESTIMO "i"
                livro = listaLivro.Find(x => x.NumeroTombo == i.NumeroTombo);

                // ARMAZENA NA LISTA 1 OS EMPRESTIMOS QUE ESTÃO COM STATUS "EMPRESTADO"
                if (i.StatusEmprestimo == 1)
                    emprestado.Add("\n\t\tCPF Cliente: " + cliente.Cpf + "\t\t\tTítulo do Livro: " + livro.Titulo +
                        "\n\n\t\tEmprestado" + "\tData Empréstimo: " + i.DataEmprestimo.ToString("dd/MM/yyyy") + "\tData Devolução: " + i.DataDevolucao.ToString("dd/MM/yyyy") +
                        "\n\n\t\t----------------------------------------------------------------------------");

                // ARMAZENA NA LISTA 2 OS EMPRESTIMOS QUE ESTÃO COM STATUS "DEVOLVIDO"
                else
                    devolvido.Add("\n\t\tCPF Cliente: " + cliente.Cpf + "\t\t\tTítulo do Livro: " + livro.Titulo +
                        "\n\n\t\tDevolvido" + "\tData Empréstimo: " + i.DataEmprestimo.ToString("dd/MM/yyyy") + "\tData Devolução: " + i.DataDevolucao.ToString("dd/MM/yyyy") +
                        "\n\n\t\t----------------------------------------------------------------------------");
            }

            // IMPRIMI PRIMEIRO A LISTA COM STATUS "EMPRESTADO"
            Console.WriteLine("\n\t\t---------------------->>>RELATÓRIO LIVRO EMPRESTÁDOS<<<---------------------");
            if (emprestado.Count == 0)
                Console.WriteLine("\t\tRelatório vazia");
            else // CASO LISTA VAZIA
                emprestado.ForEach(x => Console.WriteLine(x));

            // IMPRIMI DEPOIS A LISTA COM STATUS "DEVOLVIDO"
            Console.WriteLine("\n\t\t---------------------->>>RELATÓRIO LIVRO DEVOLVIDOS<<<----------------------");
            if (devolvido.Count == 0)
                Console.WriteLine("\t\tRelatório vazia");
            else // CASO LISTA VAZIA
                devolvido.ForEach(x => Console.WriteLine(x));

            Console.WriteLine("\nPrecione qualquer tecla para continuar...");
            Console.ReadLine();

            Console.Clear();
        }
    }
}
