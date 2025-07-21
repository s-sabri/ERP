using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Domain.Services
{
    public class ApprovalRuleEvaluator : IApprovalRuleEvaluator
    {
        public bool CanApprove(IApprovable1Entity doc)
        {
            return true;
        }

        public bool CanApprove(IApprovable2Entity document)
        {
            return true;
        }

        public bool CanApprove(IApprovable3Entity document)
        {
            return true;
        }

        public bool CanApprove(IApprovable4Entity document)
        {
            return true;
        }
    }
}
