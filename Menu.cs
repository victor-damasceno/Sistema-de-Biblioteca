using System.Runtime.CompilerServices;

Biblioteca biblioteca = new Biblioteca();

Boolean executando = true;

while (executando) {

    System.Console.WriteLine("=== SISTEMA DE BIBLIOTECA ===");
    System.Console.WriteLine("1- Cadastrar livro");
    System.Console.WriteLine("2- Listar todos os livros");
    System.Console.WriteLine("3- Listar livros disponíveis");
    System.Console.WriteLine("4- Emprestar livro");
    System.Console.WriteLine("5- Devolver livro");
    System.Console.WriteLine("6- Mostrar livro específico");
    System.Console.WriteLine("7- Remover livro");
    System.Console.WriteLine("0- Sair");

    System.Console.WriteLine("\nEscolha uma opção: ");
    int opcao;
    while(true)
    {
        if(!int.TryParse(Console.ReadLine(), out opcao))
        {
            Console.WriteLine("Digite apenas números!");
            continue;
        }

        if(opcao < 0 || opcao > 7)
        {
            Console.WriteLine("Número fora do intervalo!");
            continue;
        }

        break;
    }

    switch (opcao)
    {
        case 1:
        biblioteca.CadastrarLivro();
        break;

        case 2:
        biblioteca.ListarLivros();
        break;

        case 3:
        biblioteca.ListarLivrosDisponiveis();
        break;


        case 4:
        biblioteca.EmprestarLivro();
        break;

        case 5:
        biblioteca.DevolverLivro();
        break;

        case 6:
        biblioteca.MostrarLivro();
        break;

        case 7:
        biblioteca.RemoverLivro();
        break;

        case 0:
        executando = false;
        break;

    }
}