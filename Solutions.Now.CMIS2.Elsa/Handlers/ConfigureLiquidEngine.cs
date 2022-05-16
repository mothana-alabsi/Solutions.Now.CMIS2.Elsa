using System.Threading.Tasks;
using System.Threading;
using Fluid;
using MediatR;
using Solutions.Now.CMIS2.Elsa.Models;
using Elsa.Scripting.Liquid.Messages;

namespace Solutions.Now.CMIS2.Elsa.Handlers
{
    public class ConfigureLiquidEngine : INotificationHandler<EvaluatingLiquidExpression>
    {
        public Task Handle(EvaluatingLiquidExpression notification, CancellationToken cancellationToken)
        {
            notification.TemplateContext.Options.MemberAccessStrategy.Register<OutputActivityData>();
            notification.TemplateContext.Options.MemberAccessStrategy.Register<string>();
            return Task.CompletedTask;
        }
    }
}
