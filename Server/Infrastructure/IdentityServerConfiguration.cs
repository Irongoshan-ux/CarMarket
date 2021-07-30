﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace CarMarket.Server.Infrastructure
{
    public static class IdentityServerConfiguration
    {
        internal static IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource("API", "ServerAPI");
        }

        internal static IEnumerable<Client> GetClients()
        {
            yield return new Client
            {
                ClientId = "client_blazor",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequireConsent = false,
                RequirePkce = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.Address,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                    //"roles"
                },
                RedirectUris = { "https://localhost:5001/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:5001/authentication/logout-callback" },
                ClientSecrets = { new Secret("BlazorSecret".Sha512()) },
                AllowedCorsOrigins = { "https://localhost:5001" }
            };    
        }        

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Address();
            yield return new IdentityResources.Profile();
            yield return new IdentityResources.Email();
            //yield return new IdentityResource("roles", "User role(s)", new List<string> { "role" });
        }

        internal static IEnumerable<ApiScope> GetScopes()
        {
            yield return new ApiScope("API");
        }
    }
}
