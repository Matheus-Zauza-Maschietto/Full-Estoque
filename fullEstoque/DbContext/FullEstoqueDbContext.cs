using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace fullEstoque.DbContext;

public class FullEstoqueDbContext
{
    private readonly MongoClient _mongoClient;
    public readonly IMongoDatabase Database;
    public FullEstoqueDbContext(IConfiguration configuration)
    {
        _mongoClient = new (configuration["ConnectionStrings:mongoDB"]);
        Database = _mongoClient.GetDatabase("fullEstoque");
    }
}
