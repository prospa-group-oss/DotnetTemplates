using System.Threading.Tasks;
using NServiceBus;
using V1.Commands;

namespace ProspaWorkerNsb.Application.Handlers
{
    public class SampleCommandHandler : IHandleMessages<SampleCommand>
    {
        public Task Handle(SampleCommand message, IMessageHandlerContext context)
        {
            context.Logger().Information("Recieved sample command");

            return Task.CompletedTask;
        }
    }
}
