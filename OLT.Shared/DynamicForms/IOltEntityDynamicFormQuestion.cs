using System.Collections.Generic;

namespace OLT.Core
{
    public interface IOltEntityDynamicFormQuestion<TTemplate, TMetaData> : IOltEntityDynamicFormQuestion
        where TTemplate : IOltEntityDynamicForm
        where TMetaData : IOltEntityDynamicFormQuestionMetaData
    {
        int TemplateId { get; set; }
        TTemplate Template { get; set; }

        ICollection<TMetaData> MetaData { get; }
    }

    public interface IOltEntityDynamicFormQuestion : IOltEntity, IOltEntitySortable
    {
        string Question { get; set; }
        string TemplateName { get; set; }
        string StoreEndpoint { get; set; }
    }
}