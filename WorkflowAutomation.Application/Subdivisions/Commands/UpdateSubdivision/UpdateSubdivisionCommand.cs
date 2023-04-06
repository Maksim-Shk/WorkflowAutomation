using MediatR;

namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision
{
    public class UpdateSubdivisionCommand : IRequest<int>
    {
        /// <summary>
        /// ID ����������� �������������
        /// </summary>
        public int SubdivisionId { get; set; }
        /// <summary>
        /// ID ������������, ������������ ������ � �������������
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// �������� �������������
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// ���� �������� �������������
        /// </summary>
        // �������� ������������� ��� ��������, �� ����� ���� ��������
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// ID �������������, � �������� ����� ������������� ������� �������������
        /// </summary>
        public int? SubordinationId { get; set; }
        /// <summary>
        /// ������ ������������� � �����������
        /// </summary>
        public List<UpdatesSubUsers>? UpdatedSubdivisionUsers { get; set; }

    }
}
