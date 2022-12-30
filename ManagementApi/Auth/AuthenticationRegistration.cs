using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace ManagementApi.Auth
{
    public static class AuthenticationRegistration
    {
        private const string SchemeNameMachine = "machine";

        public static void SetupAuthentication(this IServiceCollection serviceCollection, Auth0Configuration configuration)
        {
            var authenticationBuilder = serviceCollection
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                });

            SetupMachineAuthentication(authenticationBuilder, configuration);

            serviceCollection
                .AddAuthorization(options =>
                {
                    var allSchemes = new List<string> { SchemeNameMachine };

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

        private static void SetupMachineAuthentication(AuthenticationBuilder authenticationBuilder, Auth0Configuration configuration)
        {
            authenticationBuilder.AddJwtBearer(SchemeNameMachine, options =>
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
            });
        }
    }
}