using Microsoft.AspNetCore.Mvc;
using minimalAPIMongo.Domains;
using minimalAPIMongo.Service;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace minimalAPIMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMongoCollection<Client> _clientCollection;

        public ClientController(MongoDbService mongoDbService)
        {
            // Correção na atribuição da coleção para a variável correta
            _clientCollection = mongoDbService.GetDatabase("ProductDatabase_Tarde").GetCollection<Client>("client");
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get()
        {
            try
            {
                var clients = await _clientCollection.Find(FilterDefinition<Client>.Empty).ToListAsync();
                return Ok(clients);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao buscar clientes.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Client client)
        {
            try
            {
                await _clientCollection.InsertOneAsync(client);
                return StatusCode(201, client);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao inserir cliente.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(string id)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(x => x.Id, id);
                var client = await _clientCollection.Find(filter).FirstOrDefaultAsync();

                return client != null ? Ok(client) : NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Erro ao buscar cliente por ID.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(Client client)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(x => x.Id, client.Id);
                await _clientCollection.ReplaceOneAsync(filter, client);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Erro ao atualizar cliente.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var filter = Builders<Client>.Filter.Eq(x => x.Id, id);
                var result = await _clientCollection.DeleteOneAsync(filter);

                return result.DeletedCount > 0 ? Ok() : NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Erro ao deletar cliente.");
            }
        }
    }
}
