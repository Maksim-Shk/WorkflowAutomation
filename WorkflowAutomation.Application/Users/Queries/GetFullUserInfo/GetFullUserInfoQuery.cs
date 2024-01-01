using MediatR;

namespace WorkflowAutomation.Application.Users.Queries.GetFullUserInfo;

public class GetFullUserInfoQuery : IRequest<FullUserInfoDto>
{
    /// <summary>
    /// ������������� ������������ - ��� ���� ����
    /// </summary>
    public string RequestedUserId { get; set; }
    /// <summary>
    /// ������������� ������������ - ��� ��� �����������
    /// </summary>
    public string RequestingUserId { get; set; }


}
