namespace OLT.Core
{
    public abstract class OltRule : OltDisposable, IOltRule
    {
        public virtual string RuleName => this.GetType().FullName;
        protected virtual IOltResult Success => OltResultHelper.Success;
        protected virtual IOltResultValidation BadRequest(OltSeverityTypes severity, string message) => new OltResultValidation(severity, message);
        protected virtual IOltResultValidation Valid => OltResultHelper.Valid;
        protected virtual OltRuleException Failure(string message) => new OltRuleException(message);
    }
}