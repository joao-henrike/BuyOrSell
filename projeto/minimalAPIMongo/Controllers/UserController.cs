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
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserController(MongoDbService mongoDbService)
        {
            _userCollection = mongoDbService.GetDatabase("ProductDatabase_Tarde").GetCollection<User>("user");
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            try
            {
                var users = await _userCollection.Find(FilterDefinition<User>.Empty).ToListAsync();
                return Ok(users);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao buscar usuários.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(User user)
        {
            try
            {
                await _userCollection.InsertOneAsync(user);
                return StatusCode(201, user);
            }
            catch (Exception)
            {
                return BadRequest("Erro ao inserir usuário.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(string id)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, id);
                var user = await _userCollection.Find(filter).FirstOrDefaultAsync();

                return user is not null ? Ok(user) : NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Erro ao buscar usuário por ID.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(User user)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, user.Id);
                var result = await _userCollection.ReplaceOneAsync(filter, user);

                return result.ModifiedCount > 0 ? Ok() : NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Erro ao atualizar usuário.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq(x => x.Id, id);
                var result = await _userCollection.DeleteOneAsync(filter);

                return result.DeletedCount > 0 ? Ok() : NotFound();
            }
            catch (Exception)
            {
                return BadRequest("Erro ao deletar usuário.");
            }
        }
    }
}
