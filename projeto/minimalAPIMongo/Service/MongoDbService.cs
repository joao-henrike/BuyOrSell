using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace minimalAPIMongo.Service
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;

        /// <summary>
        /// Construtor da classe MongoDbService.
        /// </summary>
        /// <param name="configuration">Objeto contendo toda a configuração da aplicação.</param>
        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;

            try
            {
                // Acessa a string de conexão
                var connectionString = _configuration.GetConnectionString("DbConnection");

                // Transforma a string obtida em um MongoUrl
                var mongoUrl = MongoUrl.Create(connectionString);

                // Cria um cliente MongoDB
                var mongoClient = new MongoClient(mongoUrl);

                // Obtém a referência ao banco de dados MongoDB
                _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            }
            catch (Exception ex)
            {
                // Logar ou tratar a exceção conforme necessário
                throw new Exception("Erro ao conectar ao MongoDB", ex);
            }
        }

        /// <summary>
        /// Propriedade para acessar o banco de dados MongoDB.
        /// </summary>
        public IMongoDatabase GetDatabase => _database;

        internal object GetDatabase(string id) => throw new NotImplementedException();
    }
}
