using Microsoft.AspNetCore.Mvc;
using RequestResponseLogging.Models;

namespace RequestResponseLogging.Authentication;

[Route("api/[controller]")]
public class UserAuthenticationController : ControllerBase
{
    private readonly ILogger<UserAuthenticationController> _logger;

    public UserAuthenticationController(ILogger<UserAuthenticationController> logger)
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
