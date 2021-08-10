namespace OLT.Core
{
    public abstract class OltRule : OltDisposable, IOltRule
    {
        public virtual string RuleName => this.GetType().FullName;
        protected virtual IOltResult Success() => new OltResultSuccess();
        protected virtual IOltResult BadRequest(OltValidationSeverityTypes severity, string message) => new OltResultValidation(severity, message);
    }
}