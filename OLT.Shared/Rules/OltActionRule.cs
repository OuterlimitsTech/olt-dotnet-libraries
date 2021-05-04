namespace OLT.Core
{
    public abstract class OltActionRule : OltDisposable, IOltActionRule
    {
        public abstract IOltRuleResult CanExecute(IOltRuleRequest request);
        public abstract IOltRuleResult Execute(IOltRuleRequest request);
        //public virtual string RuleName => BuildName<TObj1, TObj2>();
        //public abstract string RuleName { get; }
        public virtual string RuleName => this.GetType().FullName;

        protected virtual IOltRuleResult Success() => new OltRuleResult();
        protected virtual IOltRuleResult BadRequest(OltValidationSeverityTypes severity, string message) => new OltRuleResult(severity, message);
    }


    public abstract class OltActionRule<TRequest> : OltDisposable, IOltActionRule<TRequest>
        where TRequest : class, IOltRuleRequest
    {
        public abstract IOltRuleResult CanExecute(TRequest request);
        public abstract IOltRuleResult Execute(TRequest request);
        //public abstract string RuleName { get; }
        public virtual string RuleName => this.GetType().FullName;
        protected virtual IOltRuleResult Success() => new OltRuleResult();
        protected virtual IOltRuleResult BadRequest(OltValidationSeverityTypes severity, string message) => new OltRuleResult(severity, message);

    }
}