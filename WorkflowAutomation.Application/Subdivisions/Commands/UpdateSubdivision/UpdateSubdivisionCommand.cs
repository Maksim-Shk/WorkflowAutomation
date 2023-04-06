using MediatR;

namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision
{
    public class UpdateSubdivisionCommand : IRequest<int>
    {
        /// <summary>
        /// ID измененного подразделения
        /// </summary>
        public int SubdivisionId { get; set; }
        /// <summary>
        /// ID пользователя, обновляющего данные о подразделении
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Название подразделения
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Дата создания подразделения
        /// </summary>
        // задается автоматически при создании, но может быть изменена
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// ID подразделения, к которому будет перепривязано текущее подразделение
        /// </summary>
        public int? SubordinationId { get; set; }
        /// <summary>
        /// Список пользователей с изменениями
        /// </summary>
        public List<UpdatesSubUsers>? UpdatedSubdivisionUsers { get; set; }

    }
}
