using Shared.Domain.Abstractions.Behaviors;

namespace Shared.Domain.Services
{
    public interface IApprovalRuleEvaluator : IDomainService
    {
        bool CanApprove(IApprovable1Entity document);
        bool CanApprove(IApprovable2Entity document);
        bool CanApprove(IApprovable3Entity document);
        bool CanApprove(IApprovable4Entity document);
    }
}
