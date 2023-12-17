using fullEstoque.Models;
using fullEstoque.Repository;
using Microsoft.AspNetCore.Mvc;

namespace fullEstoque.Endpoints;

public static class ProdutosEndpoints
{
    public static void SetProdutosEndpoint(this WebApplication app)
    {
        app.MapPost("/produto", PostProduto);
        app.MapGet("/produto", GetTodosProdutos);
        app.MapGet("/produto/{codigo}", GetProdutoPorCodigo);
        app.MapDelete("/produto/{codigo}", DeleteProdutoPorCodigo);
        app.MapPut("/produto/{codigo}", PutProdutoPorCodigo);
    }

    private static IResult PostProduto([FromBody]Produtos produto, ProdutoRepository repository)
    {
        try
        {
            repository.AdicionarProduto(produto);
        }
        catch(Exception)
        {
            return Results.Problem("Houve um problema ao criar o produto");
        }
        return Results.Ok("Produto criado com sucesso");
    }

    private static IResult GetTodosProdutos(ProdutoRepository repository)
    {
        try
        {
            List<Produtos> produtos = repository.BuscarTodosProdutos();
            return Results.Ok(produtos);
        }
        catch(Exception e)
        {
            return Results.Problem($"Houve um erro ao tentar buscar os produtos. Exception: {e.Message}");
        }
    }

    private static IResult GetProdutoPorCodigo([FromRoute]string codigo, ProdutoRepository repository)
    {
        try
        {
            Produtos produtos = repository.BuscarProdutosPorCodigo(codigo);
            return Results.Ok(produtos);
        }
        catch(Exception e)
        {
            return Results.Problem($"Houve um erro ao tentar buscar os produtos. Exception: {e.Message}");
        }
    }

    private static IResult DeleteProdutoPorCodigo([FromRoute]string codigo, ProdutoRepository repository)
    {
        try
        {
            repository.DeletarProdutosPorCodigo(codigo);
            return Results.Ok($"Produto de Codigo: {codigo} foi deletado");
        }
        catch(Exception e)
        {
            return Results.Problem($"Houve um erro ao tentar buscar os produtos. Exception: {e.Message}");
        }
    }

    private static IResult PutProdutoPorCodigo([FromRoute]string codigo, [FromBody]Produtos produto, ProdutoRepository repository)
    {
        try
        {
            repository.AlterarProdutosPorCodigo(codigo, produto);
            return Results.Ok($"Produto de Codigo: {codigo} foi deletado");
        }
        catch(Exception e)
        {
            return Results.Problem($"Houve um erro ao tentar buscar os produtos. Exception: {e.Message}");
        }
    }
}
