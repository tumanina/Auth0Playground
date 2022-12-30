using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ClientApi.Auth
{
    public static class AuthenticationRegistration
    {
        private const string SchemeNameClient = "client";

        public static void SetupAuthentication(this IServiceCollection serviceCollection, Auth0Configuration configuration)
        {
            var authenticationBuilder = serviceCollection
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                });

            SetupClientAuthentication(authenticationBuilder, configuration);

            serviceCollection
                .AddAuthorization(options =>
                {
                    var allSchemes = new List<string> { SchemeNameClient };

                    // Default Policy
                    var defaultPolicy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes(allSchemes.ToArray())
                        .Build();

                    // Ensure this policy is always used:
                    options.DefaultPolicy = defaultPolicy;
                    options.FallbackPolicy = defaultPolicy;
                });
        }

        private static void SetupClientAuthentication(AuthenticationBuilder authenticationBuilder, Auth0Configuration configuration)
        {
            authenticationBuilder.AddJwtBearer(SchemeNameClient, options =>
            {
                options.Authority = configuration.Issuer;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration.Issuer,
                    ValidateAudience = true,
                    ValidAudiences = new List<string> { configuration.Audience },
                    ValidateLifetime = true
                };

                options.Events = new JwtBearerEvents();
#pragma warning disable 1998
                options.Events.OnTokenValidated += async (context) => AddRoleClaimsFromScope(context);
#pragma warning restore 1998
            });
        }

        private static void AddRoleClaimsFromScope(TokenValidatedContext context)
        {
            var claimsToAdd = new List<Claim>();
            var scopes = context.Principal.Claims.Where(c => c.Type == "scope").ToList();

            var claimsIdentity = ((ClaimsIdentity)context.Principal.Identity);

            foreach (var scope in scopes)
            {
                var split = scope.Value.Split(" ");
                foreach (var singleScope in split)
                {
                    claimsToAdd.Add(new Claim(ClaimTypes.Role, singleScope));
                }
            }

            ((ClaimsIdentity)context.Principal.Identity).AddClaims(claimsToAdd);
        }
    }
}