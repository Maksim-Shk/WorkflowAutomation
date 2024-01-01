using MediatR;

namespace WorkflowAutomation.Application.Users.Queries.GetFullUserInfo;

public class GetFullUserInfoQuery : IRequest<FullUserInfoDto>
{
    /// <summary>
    /// Запрашиваемый пользователь - тот кого ищут
    /// </summary>
    public string RequestedUserId { get; set; }
    /// <summary>
    /// Запрашивающий пользователь - тот кто запрашивает
    /// </summary>
    public string RequestingUserId { get; set; }


}
