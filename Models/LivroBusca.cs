public class LivroBusca
{
    public string Titulo {get;set;}
    public string Autor {get;set;}
    public string Status {get;set;}

    public LivroBusca(string titulo, string autor, string status)
    {
        Titulo = titulo;
        Autor = autor;
        Status = status;
    }

}