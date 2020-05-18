using System;
using NServiceBus;

// ReSharper disable CheckNamespace
namespace V1.Commands
// ReSharper restore CheckNamespace
{
    public class SampleCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
