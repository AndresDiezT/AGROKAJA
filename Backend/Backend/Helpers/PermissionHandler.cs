using Microsoft.AspNetCore.Authorization;

namespace Backend.Helpers
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // revisa si el usuario tiene el permiso requerido
            if (context.User.HasClaim(c => c.Type == "permission" && c.Value == requirement.Permission))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
