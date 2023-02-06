using IdentityServer4.Models;

namespace identityserver
{
    public class Config
    {


        public static IEnumerable<IdentityResource> IdentityResources =>
                new List<IdentityResource>
                {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
                };


        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
            new ApiScope("api1")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
            new Client
            {
                ClientId = "client",

                // 没有交互式用户，使用 clientid/secret 进行身份验证
                AllowedGrantTypes = GrantTypes.ClientCredentials,

                // 用于身份验证的密钥
                ClientSecrets =
                {
                    new Secret("secret".Sha256())  //secret加密密钥 Sha256加密方式
                },

                // 客户端有权访问的范围
                AllowedScopes = { "api1" },
                AccessTokenLifetime = 120 //过期时间，默认3600秒
            }
            };

    }
}
