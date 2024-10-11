using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StackExchange.Redis;
using web_app_domain;
using web_app_repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace web_performance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private static ConnectionMultiplexer redis;
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsuario()
        {

            //string key = "getusuario";
            //redis = ConnectionMultiplexer.Connect("localhost:6379");
            //IDatabase db = redis.GetDatabase();
            //await db.KeyExpireAsync(key, TimeSpan.FromSeconds(10));
            //string user = await db.StringGetAsync(key);

            //if (!string.IsNullOrEmpty(user))
            //{
            //    return Ok(user);
            //}

            var usuarios = await _repository.ListarUsuarios();
            if (usuarios == null)
            {
                return NotFound();
            }

            string usuariosJson = JsonConvert.SerializeObject(usuarios);
            //await db.StringSetAsync(key, usuariosJson);
            return Ok(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            await _repository.SalvarUsuario(usuario);

            //apagar cache
            //string key = "getusuario";
            //redis = ConnectionMultiplexer.Connect("localhost:6379");
            //IDatabase db = redis.GetDatabase();
            //await db.KeyDeleteAsync(key);



            return Ok(new {mensagem = "Criado com sucesso"});
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Usuario usuario)
        {

            await _repository.AtualizarUsuario(usuario);

            //apagar cache
            string key = "getusuario";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete (int id)
        {

            await _repository.RemoverUsuario(id);

         

            //apagar cache
            string key = "getusuario";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();
        }
    }
}

