using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class TestController : ControllerBase
    {
        // GET: api/Test
        /// <summary>
        /// 方法加权
        /// </summary>
        /// <returns></returns>
        [Authorize("api1")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 方法未加权  可直接访问
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Test/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 开放获取token  API 接口
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetToken")]
        public async Task<string> GetToken()
        {
            var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:5058/connect/token",
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
              //  UserName = "Admin",
              //  Password = "123456",
            });

            if (tokenResponse.IsError)
            {
                return tokenResponse.Error;
            }

            return tokenResponse.AccessToken;

        }
    }
}
