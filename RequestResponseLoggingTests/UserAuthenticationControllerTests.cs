using Microsoft.AspNetCore.Mvc.Infrastructure;
using RequestResponseLogging.Authentication;
using RequestResponseLogging.Models;
namespace RequestResponseLoggingTests;

public class UserAuthenticationControllerTests
{
    [Fact]
    public async Task CheckRequestModeIsNull()
    {
        //Arrange
        UserAuthenticationController sut = UserAuthenticationControllerMotherObject.GetUserAuthenticationController();

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.LoginUserAsync(null));
    }

    [Theory, ClassData(typeof(UserClassEmptyData))]
    public async Task CheckRequestModelWhenIsEmptyString(UserModel user)
    {
        //Arrange
        UserAuthenticationController sut = UserAuthenticationControllerMotherObject.GetUserAuthenticationController();

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.LoginUserAsync(user));
    }

    [Theory, ClassData(typeof(UserClassEmptywithSpaceData))]
    public async Task CheckRequestModelWhenIsEmptyStringWithSpace(UserModel user)
    {
        //Arrange
        UserAuthenticationController sut = UserAuthenticationControllerMotherObject.GetUserAuthenticationController();

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.LoginUserAsync(user));
    }

    [Theory, ClassData(typeof(UserClassData))]
    public async Task CheckRequestModelWhenUserModelIsCorrect(UserModel user)
    {
        //Arrange
        UserAuthenticationController sut = UserAuthenticationControllerMotherObject.GetUserAuthenticationController();

        //Act
        var actualResult = await sut.LoginUserAsync(user);

        //Assert
        Assert.Equal(((IStatusCodeActionResult)actualResult).StatusCode, 200);
    }
}