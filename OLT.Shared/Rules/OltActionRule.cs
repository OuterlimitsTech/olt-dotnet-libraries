namespace OLT.Core
{
    public abstract class OltActionRule : OltDisposable, IOltActionRule
    {
        public abstract IOltResult CanExecute(IOltRequest request);
        public abstract IOltResult Execute(IOltRequest request);
        public virtual string RuleName => this.GetType().FullName;

        protected virtual IOltResult Success() => new OltRuleResult();
        protected virtual IOltResult BadRequest(OltValidationSeverityTypes severity, string message) => new OltRuleResult(severity, message);
    }


    public abstract class OltActionRule<TRequest> : OltDisposable, IOltActionRule<TRequest>
        where TRequest : class, IOltRequest
    {
        public abstract IOltResult CanExecute(TRequest request);
        public abstract IOltResult Execute(TRequest request);
        public virtual string RuleName => this.GetType().FullName;
        protected virtual IOltResult Success() => new OltRuleResult();
        protected virtual IOltResult BadRequest(OltValidationSeverityTypes severity, string message) => new OltRuleResult(severity, message);
    }
}