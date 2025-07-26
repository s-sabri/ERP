using Shared.Domain.Entities;
using Shared.Domain.Rules.Base;

namespace Shared.Domain.Rules.Approval
{
    public class Level1MustBeApprovedRule<TKey> : BaseBusinessRule where TKey : notnull
    {
        private readonly BaseApprovable2Entity<TKey> _entity;

        public Level1MustBeApprovedRule(BaseApprovable2Entity<TKey> entity)
        {
            _entity = entity;
        }

        public override string Message => "تایید سند انجام نشده است";
        public override bool IsBroken() => _entity.IsApproved2 && !_entity.IsApproved1;
    }
}
