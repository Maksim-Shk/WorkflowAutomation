using MediatR;

namespace WorkflowAutomation.Application.Positions.Commands.CreateNewPositionCommand
{
    public class CreatePositionCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int? IdSubordination { get; set; }
        public string? ShortName { get; set; }
    }
}
