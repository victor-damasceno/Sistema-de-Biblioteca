public class Livro
{
    public Guid Id {get;set;}
    public string Titulo {get;set;}
    public string Autor {get;set;}
    public bool Emprestado {get;set;}

    public Livro(string titulo, string autor)
    {

        Id = Guid.NewGuid();

        Titulo = titulo;
        Autor = autor;
        Emprestado = false;
    }
    
}