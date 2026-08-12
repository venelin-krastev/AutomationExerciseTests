using NUnit.Framework;
using AutomationExerciseTests.Pages;

namespace AutomationExerciseTests;

[SetUpFixture]
public class AssemblySetup
{
    public static string RegisteredEmail { get; private set; } = string.Empty;
    public const string RegisteredPassword = "Test1234!";

    [OneTimeSetUp]
    public void RegisterSharedTestAccount()
    {
        RegisteredEmail = $"qa_shared_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}@test.com";
        using var driver = DriverFactory.Create();
        var signup = new SignupPage(driver);
        signup.NavigateTo();
        signup.SubmitSignupForm("QA Shared User", RegisteredEmail);
        new AccountInfoPage(driver).CompleteRegistration();
    }
}
