using System;

namespace OLT.Core
{
    [Obsolete("Use https://fluentvalidation.net/ instead")]
    public abstract class OltRuleValidation : OltRule, IOltRuleValidation
    {
        public abstract IOltResultValidation Validate(IOltRequest request);
    }

    [Obsolete("Use https://fluentvalidation.net/ instead")]
    public abstract class OltRuleValidation<TRequest, TResult> : OltRule, IOltRuleValidation<TRequest, TResult>
        where TRequest : IOltRequest
        where TResult : IOltResultValidation
    {
        public abstract TResult Validate(TRequest request);
    }

    [Obsolete("Use https://fluentvalidation.net/ instead")]
    public abstract class OltRuleValidation<TRequest> : OltRule, IOltRuleValidation<TRequest>
        where TRequest : IOltRequest
    {
        public abstract IOltResultValidation Validate(TRequest request);
    }
}