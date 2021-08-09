namespace OLT.Core
{

    public abstract class OltRuleValidate<TRequest> : OltRule, IOltRuleValidate<TRequest>
        where TRequest : class, IOltRequest
    {
        public abstract IOltResult Validate(TRequest request);
    }

}