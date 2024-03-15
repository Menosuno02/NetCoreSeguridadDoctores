using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NetCoreSeguridadDoctores.Policies
{
    public class OverSalarioRequirement
        : AuthorizationHandler<OverSalarioRequirement>,
        IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context, OverSalarioRequirement requirement)
        {
            if (!context.User.HasClaim
                (u => u.Type == "SALARIO"))
            {
                context.Fail();
            }
            else
            {
                string data = context.User.FindFirst("SALARIO").Value;
                int salario = int.Parse(data);
                if (salario >= 220000)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}
