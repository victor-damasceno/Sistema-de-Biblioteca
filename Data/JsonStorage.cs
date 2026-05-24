using System.Text.Json;
public class JsonStorage
{

    public List<Livro> CarregarLivros()
    {

        try
        {
            if(File.Exists("livros.json"))
        {
            string json = File.ReadAllText("livros.json");

            List <Livro>? livros = JsonSerializer.Deserialize<List<Livro>>(json);
            
            return livros ?? new List<Livro>();
        }

        return new List<Livro>();

        } catch (Exception erro)
        {
            System.Console.WriteLine($"Não foi possível carregar a lista dos livros: {erro.Message}");
            return new List<Livro>();
        }
        
    }
    public void SalvarLivro(List<Livro> livros)
    {

        try
        {
            string json = JsonSerializer.Serialize(
            livros,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText("livros.json", json);
            System.Console.WriteLine("Livro cadastrado com sucesso!");  
        } catch
        {
            System.Console.WriteLine("Não foi possível cadastrar o livro!");
        }
        
    }
}