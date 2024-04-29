using System.Collections;

using Microsoft.Extensions.Logging;

using Moq;

using RequestResponseLogging.Authentication;
using RequestResponseLogging.Models;

namespace RequestResponseLoggingTests;
internal class UserAuthenticationControllerMotherObject
{
    public static UserAuthenticationController GetUserAuthenticationController()
    {
        Mock<ILogger<UserAuthenticationController>> logger = new();
        return new(logger.Object);
    }


}

public class UserClassEmptywithSpaceData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new UserModel { UserName = " ", Password = "  " } };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class UserClassEmptyData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new UserModel { UserName = "", Password = "" } };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class UserClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new UserModel { UserName = "Guest", Password = "Guest" } };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
