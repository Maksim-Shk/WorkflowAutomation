namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision
{
    public class UpdatesSubUsers
    {
        /// <summary>
        /// ID меняющего подразделение пользователя
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Дата привязки пользователя к подразделению
        /// </summary>
        // задается автоматически при создании, но может быть изменена
        public DateTime? AppointmentDate { get; set; }
        /// <summary>
        /// ID подразделения, к которому перепривязывается пользователь
        /// </summary>
        public int? NewSubdivisionId { get; set; }
        /// <summary>
        /// дата удаления пользователя (без привязки к другому подразделению)
        /// </summary>
        public DateTime? RemovalDate { get; set;}
    }
}