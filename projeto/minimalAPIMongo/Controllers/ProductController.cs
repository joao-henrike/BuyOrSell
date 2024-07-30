using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Product;
using minimalAPIMongo.Service;
using MongoDB.Driver;

public class ProductController : ControllerBase
{
    private readonly IMongoCollection<Product> _product;

    public ProductController(MongoDbService mongoDbService)
    {
        _product = mongoDbService.GetDatabase("ProductDatabase_Tarde").GetCollection<Product>("product");

    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> Get()
    {
        try
        {
            var products = await _product.Find(FilterDefinition<Product>.Empty).ToListAsync();
            return Ok(products);
        }
        catch (Exception)
        {
            return BadRequest("Erro ao buscar produtos.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post(Product product)
    {
        try
        {
            await _product.InsertOneAsync(product);
            return StatusCode(201, product);
        }
        catch (Exception)
        {
            return BadRequest("Erro ao inserir produto.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(string id)
    {
        try
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            var product = await _product.Find(filter).FirstOrDefaultAsync();

            return product is not null ? Ok(product) : NotFound();
        }
        catch (Exception)
        {
            return BadRequest("Erro ao buscar produto por ID.");
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update(Product product)
    {
        try
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, product.Id);
            await _product.ReplaceOneAsync(filter, product);

            return Ok();
        }
        catch (Exception)
        {
            return BadRequest("Erro ao atualizar produto.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        try
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            var result = await _product.DeleteOneAsync(filter);

            return result.DeletedCount > 0 ? Ok() : NotFound();
        }
        catch (Exception)
        {
            return BadRequest("Erro ao deletar produto.");
        }
    }
}
