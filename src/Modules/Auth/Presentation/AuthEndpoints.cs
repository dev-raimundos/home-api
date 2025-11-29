using home_api.src.Modules.Auth.Application;

namespace home_api.src.Modules.Auth.Presentation
{
    public static class AuthEndpoints
    {
        public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/auth/login", LoginHandler.Handle);
            app.MapPost("/auth/logout", LogoutHandler.Handle);
            app.MapPost("/auth/refresh", RefreshHandler.Handle);

            return app;
        }
    }

}
