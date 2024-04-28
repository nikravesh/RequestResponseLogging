using Microsoft.AspNetCore.Mvc;

namespace RequestResponseLogging.Authentication;

[Route("api/[controller]")]
public class UserAuthentication : ControllerBase
{
    private readonly ILogger<UserAuthentication> _logger;

    public UserAuthentication(ILogger<UserAuthentication> logger)
    {
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] UserModel userModel)
    {
        if (userModel is null || string.IsNullOrWhiteSpace(userModel.UserName) || string.IsNullOrWhiteSpace(userModel.Password))
            throw new ArgumentNullException(nameof(userModel));

        return Ok(userModel);
    }
}
