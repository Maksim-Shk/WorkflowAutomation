using System.ComponentModel;

namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionInfo;

public enum RenderOption
{
    [Description("Не отображать")]
    NotRender,
    [Description("Редактировать")]
    Edit,
    [Description("Перевести")]
    ChangeSubdivison,
    [Description("Уволить")]
    Remove
}
