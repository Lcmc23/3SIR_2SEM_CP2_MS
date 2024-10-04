namespace web_app_repository;


using MySqlConnector;
using Dapper;
using web_app_domain;

public class ProdutoRepository : IProdutoRepository
{
    private readonly MySqlConnection mySqlConnection;

    public ProdutoRepository()
    {
        string connectionString = "Server=localhost;Database=sys;User=root;Password=123;";
        mySqlConnection = new MySqlConnection(connectionString);
    }

    public async Task<IEnumerable<Produto>> ListarProdutos()
    {
        await mySqlConnection.OpenAsync();
        string query = "select id, nome, preco, quantidade_estoque, data_criacao from produtos;";
        var produtos = await mySqlConnection.QueryAsync<Produto>(query);
        await mySqlConnection.CloseAsync();

        return produtos;
    }

    public async Task SalvarProduto(Produto produto)
    {
        await mySqlConnection.OpenAsync();
        string sql = "insert into produtos(nome, preco, quantidade_estoque, data_criacao) values(@nome, @preco, @quantidade_estoque, @data_criacao);";
        await mySqlConnection.ExecuteAsync(sql, produto);
        await mySqlConnection.CloseAsync();

    }

    public async Task AtualizarProduto(Produto produto)
    {
        await mySqlConnection.OpenAsync();

        string sql = "update produtos\nset nome = @nome,\n\tpreco = @preco,\n\tquantidade_estoque = @quantidade_estoque,\n\tdata_criacao = @data_criacao\nwhere id = @id;";
        await mySqlConnection.ExecuteAsync(sql, produto);
        await mySqlConnection.CloseAsync();

    }

    public async Task RemoverProduto(int id)
    {
        await mySqlConnection.OpenAsync();
        string sql = "delete from produtos where id = @id;";
        await mySqlConnection.ExecuteAsync(sql, new { id });
        await mySqlConnection.CloseAsync();
    }

}

