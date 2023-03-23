using System.IdentityModel.Tokens.Jwt;

namespace JoggingApp.Options
{
    public static class GeneralExtentions
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return string.Empty;
            }

            var claim = httpContext.User.Claims.FirstOrDefault(x => x.Type == "Id");

            if (claim == null)
                return string.Empty;

            return claim.Value;
        }

        public static void Logout(this HttpContext httpContext)
        {
            httpContext.User = null;
        }
    }
}
