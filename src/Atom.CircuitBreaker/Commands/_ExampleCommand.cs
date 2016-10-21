using System;
using System.Threading;
using System.Threading.Tasks;

namespace Atom.CircuitBreaker.Commands
{    
    public abstract class ExampleCommand : AsyncCommand<int>
    {

        public ExampleCommand(ICommandContext ctx)
            :base(ctx,"example1", "Example",  TimeSpan.FromMilliseconds(2000))
        {          
        }
             
        public override Task<int> ExecuteAsync(CancellationToken cancellationToken)
        {
            return null;
        }
    }
}