using WorkflowAutomation.Shared.Identity;

namespace WorkflowAutomation.Client.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginDto loginDto);
    Task Logout();
    Task<RegisterResult> Register(RegisterDto registerDto);
}
