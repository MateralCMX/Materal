using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace WorkflowCore.Sample03.Steps
{
    public class CustomMessage : StepBody
    {

        public string Message { get; set; } = string.Empty;

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.WriteLine(Message);
            return ExecutionResult.Next();
        }
    }
}
