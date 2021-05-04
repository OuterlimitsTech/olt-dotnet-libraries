namespace OLT.Core
{
    public abstract class OltRule : OltDisposable, IOltRule
    {
        public virtual string RuleName => this.GetType().FullName;
        protected virtual IOltRuleResult Success() => new OltRuleResult();
        protected virtual IOltRuleResult BadRequest(OltValidationSeverityTypes severity, string message) => new OltRuleResult(severity, message);
    }
}