using System.Security.Claims;
using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("todoApp", "Todo app full access", new[] { "sub", "name", "email" }),
        };

    public static IEnumerable<Client> Clients(IConfiguration config) =>
        new Client[]
        {
            new Client
            {
                ClientId = "todoApp",
                ClientName = "todoApp",
                ClientSecrets = {new Secret(config["ClientSecret"].Sha256())},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequirePkce = false,
                RedirectUris = {"http://localhost:5000" + "/api/auth/callback/id-server"},
                AllowOfflineAccess = true,
                AllowedScopes = {"todoApp", "openid", "profile"},
                AccessTokenLifetime = 360,
                AlwaysIncludeUserClaimsInIdToken = true,
            }
        };
}
