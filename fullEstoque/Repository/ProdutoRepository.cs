using fullEstoque.DbContext;
using fullEstoque.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace fullEstoque.Repository;
public class ProdutoRepository
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Produtos> _collection;
    public ProdutoRepository(FullEstoqueDbContext context)
    {
        _database = context.Database;
        _collection = _database.GetCollection<Produtos>(nameof(Produtos).ToLower());
    }

    public void AdicionarProduto(Produtos produto)
    {
        _collection.InsertOneAsync(produto);
    }

    public List<Produtos> BuscarTodosProdutos()
    {
        var projecao = Builders<Produtos>.Projection.Exclude("_id");
        return _collection.Find(new BsonDocument()).Project<Produtos>(projecao).ToList();
    }

    public Produtos BuscarProdutosPorCodigo(string codigoBusca)
    {
        var filtragem = Builders<Produtos>.Filter.Eq(p => p.Codigo, codigoBusca);
        var projecao = Builders<Produtos>.Projection.Exclude("_id");
        return _collection.Find(filtragem).Project<Produtos>(projecao).FirstOrDefault();
    }

    public void DeletarProdutosPorCodigo(string codigoBusca)
    {
        var filtragem = Builders<Produtos>.Filter.Eq(p => p.Codigo, codigoBusca);
        var projecao = Builders<Produtos>.Projection.Exclude("_id");
        _collection.DeleteOne(filtragem);
    }

    public void AlterarProdutosPorCodigo(string codigoBusca, Produtos produto)
    {
        var filtragem = Builders<Produtos>.Filter.Eq(p => p.Codigo, codigoBusca);

        var update = Builders<Produtos>.Update
            .Set(p => p.Quantidade, produto.Quantidade)
            .Set(p => p.Descricao, produto.Descricao)
            .Set(p => p.Marca, produto.Marca)
            .Set(p => p.Nome, produto.Nome)
            .Set(p => p.Tags, produto.Tags);

        _collection.UpdateOne(filtragem, update);
    }
}
