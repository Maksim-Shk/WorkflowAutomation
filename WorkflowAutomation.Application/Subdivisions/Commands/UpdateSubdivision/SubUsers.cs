namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision;

public class UpdatesSubUsers
{
    /// <summary>
    /// ID пользователя пользователя с изменениями
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
    /// <summary>
    /// ID новой должности пользователя
    /// </summary>
    public int? NewPositionId { get; set; }
}