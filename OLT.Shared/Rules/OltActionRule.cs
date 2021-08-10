namespace OLT.Core
{
    public abstract class OltRuleAction : OltDisposable, IOltRuleAction
    {
        public abstract IOltResultValidation CanExecute(IOltRequest request);
        public abstract IOltResult Execute(IOltRequest request);
        public virtual string RuleName => this.GetType().FullName;

        protected virtual IOltResult Success() => new OltResultValidation();
        protected virtual IOltResult BadRequest(OltValidationSeverityTypes severity, string message) => new OltResultValidation(severity, message);
    }


    public abstract class OltRuleAction<TRequest> : OltDisposable, IOltRuleAction<TRequest>
        where TRequest : class, IOltRequest
    {
        public abstract IOltResultValidation CanExecute(TRequest request);
        public abstract IOltResult Execute(TRequest request);
        public virtual string RuleName => this.GetType().FullName;
        protected virtual IOltResult Success() => new OltResultValidation();
        protected virtual IOltResult BadRequest(OltValidationSeverityTypes severity, string message) => new OltResultValidation(severity, message);
    }
}