namespace Shared.Domain.Rules
{
    public class UniqueNationalCodeRule : BusinessRuleBase
    {
        private readonly string _nationalCode;
        private readonly Func<string, bool> _exists;

        public UniqueNationalCodeRule(string nationalCode, Func<string, bool> exists)
        {
            _nationalCode = nationalCode;
            _exists = exists;
        }

        public override bool IsBroken() => _exists(_nationalCode);
        public override string Message => $"کد ملی '{_nationalCode}' تکراری هست";
    }
}
