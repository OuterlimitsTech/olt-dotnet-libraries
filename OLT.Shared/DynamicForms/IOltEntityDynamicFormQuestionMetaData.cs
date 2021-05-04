namespace OLT.Core
{
    public interface IOltEntityDynamicFormQuestionMetaData<TQuestion> : IOltEntityDynamicFormQuestionMetaData
        where TQuestion : IOltEntityDynamicFormQuestion
    {
        int TemplateQuestionId { get; set; }
        TQuestion TemplateQuestion { get; set; }
    }

    public interface IOltEntityDynamicFormQuestionMetaData : IOltEntity, IOltEntitySortable
    {
        string Type { get; set; }
        string Text { get; set; }
        string Value { get; set; }
        string TemplateName { get; set; }
    }
}