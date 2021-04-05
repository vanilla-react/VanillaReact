using Microsoft.AspNetCore.Authorization;

namespace VanillaReact.Api.Policies
{
    public class IsAuthenticatedPolicy : IAuthorizationRequirement
    {
        public IsAuthenticatedPolicy()
        {
        }
    }
}
