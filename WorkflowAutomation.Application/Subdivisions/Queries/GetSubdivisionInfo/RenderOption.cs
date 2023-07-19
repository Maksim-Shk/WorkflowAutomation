using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionInfo
{
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
}
