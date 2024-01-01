namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision;

public class UpdatesSubUsers
{
    /// <summary>
    /// ID ������������ ������������ � �����������
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// ���� �������� ������������ � �������������
    /// </summary>
    // �������� ������������� ��� ��������, �� ����� ���� ��������
    public DateTime? AppointmentDate { get; set; }
    /// <summary>
    /// ID �������������, � �������� ����������������� ������������
    /// </summary>
    public int? NewSubdivisionId { get; set; }
    /// <summary>
    /// ���� �������� ������������ (��� �������� � ������� �������������)
    /// </summary>
    public DateTime? RemovalDate { get; set;}
    /// <summary>
    /// ID ����� ��������� ������������
    /// </summary>
    public int? NewPositionId { get; set; }
}