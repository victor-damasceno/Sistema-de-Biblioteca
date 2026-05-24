using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
public class Biblioteca
{
    private List<Livro> livros = new List<Livro>();
    private JsonStorage storage = new JsonStorage();

    public Biblioteca()
    {
        livros = storage.CarregarLivros();
    }

    public void CadastrarLivro()
    {

        string titulo = VerificarTexto("Titulo: ");

        string autor = VerificarTexto("Autor: ");

        Livro livro = new Livro(titulo, autor);
        
        bool repetido = livros.Any(
            livro => livro.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase)
        );
            if(repetido)
            {
                System.Console.WriteLine("Este livro já está cadastrado no sistema!");
                return;
            }

        livros.Add(livro);
        storage.SalvarLivro(livros);

    }

    public void ListarLivros()
    {

        string status;

        if (livros.Count == 0)
        {
            System.Console.WriteLine("Nenhum livro cadastrado");
            return;
        }

        foreach (Livro livro in livros)
        {

            if (livro.Emprestado)
            {
                status = "Indisponível";
            }
            else
            {
                status = "Disponível";
            }

            System.Console.WriteLine($"Título: {livro.Titulo}");
            System.Console.WriteLine($"Autor: {livro.Autor}");
            System.Console.WriteLine($"Status: {status}");
        }

        System.Console.WriteLine($"Total de livros: {livros.Count}");
    }

    public void ListarLivrosDisponiveis()
    {
        var livrosDisponiveis = livros
        .Where(livro => !livro.Emprestado)
        .Select(livro => livro.Titulo);

        foreach (string titulosDisponiveis in livrosDisponiveis)
        {
            System.Console.WriteLine(titulosDisponiveis);
        }
    }

    public void EmprestarLivro()
    {

        string titulo = VerificarTexto("Digite o livro que deseja emprestar: ");

            Livro? livroBuscado = BuscarLivroPorTitulo(titulo);

                if (livroBuscado == null)
                {
                    System.Console.WriteLine("Livro não encontrado.");
                    return;
                }

                if (livroBuscado.Emprestado == true)
                {
                    System.Console.WriteLine("Livro já emprestado");
                    return;
                } else
                {
                    livroBuscado.Emprestado = true;
                    storage.SalvarLivro(livros);
                    System.Console.WriteLine("Livro emprestado com sucesso!");
                    return;
                }

    }

    public void DevolverLivro()
    {
        string titulo = VerificarTexto("Digite o titulo do livro: ");

        foreach (Livro livro in livros)
        {
            if(livro.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase))
            {
                livro.Emprestado = false;
                storage.SalvarLivro(livros);
                System.Console.WriteLine("Livro devolvido com sucesso!");
                return;
            }
        }
         System.Console.WriteLine("Livro não encontrado");
    }

    public void MostrarLivro()
    {

        System.Console.WriteLine("Digite o titulo do livro: ");
        string pesquisaLivro = Console.ReadLine()!;

        List<LivroBusca>livroBuscaLista = BuscarLivro(pesquisaLivro);

        if(livroBuscaLista.Count == 0)
        {
            System.Console.WriteLine("Nenhum livro com esse titulo foi encontrado :(");
            return;
        } else if(livroBuscaLista.Count == 1)
        {
            System.Console.WriteLine("Livro encontrado com sucesso!");
            System.Console.WriteLine($"Título: {livroBuscaLista[0].Titulo}");
            System.Console.WriteLine($"Autor: {livroBuscaLista[0].Autor}");
            System.Console.WriteLine($"Status: {livroBuscaLista[0].Status}");
            return;
        
        } else
        {
            System.Console.WriteLine($"Foram encontradas {livroBuscaLista.Count} opções: ");
            int contador = 0;
            foreach (LivroBusca livroBusca in livroBuscaLista)
            {
                contador++;
                System.Console.WriteLine($"[{contador}] {livroBusca.Titulo}");
            }

            System.Console.WriteLine("Qual das opções deseja escolher? ");
            int opcao;

            while(true)
            {
                if(!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Digite apenas números!");
                    continue;
                }

                if(opcao < 1 || opcao > livroBuscaLista.Count)
                {
                    Console.WriteLine("Número fora do intervalo!");
                    continue;
                }

                break;
            }
            

            System.Console.WriteLine($"\nTítulo: {livroBuscaLista[opcao-1].Titulo}");
            System.Console.WriteLine($"Autor: {livroBuscaLista[opcao-1].Autor}");
            System.Console.WriteLine($"Status: {livroBuscaLista[opcao-1].Status}");
        }

    }

    public void RemoverLivro()
    {
        string titulo = VerificarTexto("Digite o título completo do livro que deseja remover: ");

        Livro? livroEncontrado = null;

        foreach (Livro livro in livros)
        {
            if(livro.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase))
            {
                livroEncontrado = livro;
                break;
            }
        }

        if(livroEncontrado != null)
        {
            livros.Remove(livroEncontrado);
            System.Console.WriteLine("Livro removido com sucesso!");
        } else
        {
            System.Console.WriteLine("O livro pesquisado não foi encontrado");
        }
        
    }

    public List<LivroBusca> BuscarLivro(string pesquisaLivro)
    {

        string status = null!;
        int totalEncontrado = 0;
        List<LivroBusca>livroBuscaLista = new List<LivroBusca>();

        foreach (Livro livro in livros)
        {
            if(livro.Titulo.Equals(pesquisaLivro, StringComparison.OrdinalIgnoreCase))
            {
                totalEncontrado++;
                if(livro.Emprestado) {
                    status = "Indisponível";
                } else
                {
                    status = "Disponível";
                }

                LivroBusca resultado = new LivroBusca(livro.Titulo, livro.Autor, status);
                livroBuscaLista.Add(resultado);
            }           
        }

        return livroBuscaLista;

    }

    private Livro? BuscarLivroPorTitulo(string titulo)
    {
        return livros.FirstOrDefault(
            livro => livro.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase)
        );
    }

    private string VerificarTexto(string mensagem)
    {
        string texto;

        while(true)
        {
            Console.WriteLine(mensagem);

            texto = Console.ReadLine()!;
            if(!string.IsNullOrWhiteSpace(texto))
            {
                return texto;
            } else
            {
                System.Console.WriteLine("Digite um valor válido!");
            }
        }
    }

}