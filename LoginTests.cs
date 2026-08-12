using NUnit.Framework;
using OpenQA.Selenium;
using AutomationExerciseTests.Pages;

namespace AutomationExerciseTests;

[TestFixture]
public class LoginTests
{
    private IWebDriver? driver;
    private LoginPage? loginPage;

    private static readonly string RegisteredEmail =
        $"qa_login_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}@test.com";
    private const string RegisteredPassword = "Test1234!";

    [OneTimeSetUp]
    public void RegisterAccount()
    {
        using var setupDriver = DriverFactory.Create();
        var signup = new SignupPage(setupDriver);
        signup.NavigateTo();
        signup.SubmitSignupForm("QA Login User", RegisteredEmail);
        new AccountInfoPage(setupDriver).CompleteRegistration();
    }

    [SetUp]
    public void Setup()
    {
        driver = DriverFactory.Create();
        loginPage = new LoginPage(driver);
        loginPage.NavigateTo();
    }

    [TearDown]
    public void Teardown()
    {
        driver?.Quit();
        driver?.Dispose();
    }

    [Test]
    public void LoginWithValidCredentials_RedirectsToHomePage()
    {
        loginPage!.Login(RegisteredEmail, RegisteredPassword);

        Assert.That(driver!.Url, Does.Not.Contain("/login"),
            "Successful login should navigate away from the login page");
    }

    [Test]
    public void LoginWithInvalidPassword_ShowsErrorMessage()
    {
        loginPage!.Login(RegisteredEmail, "wrongpassword");

        Assert.That(loginPage!.ErrorIsDisplayed("Your email or password is incorrect!"), Is.True,
            "Login with an incorrect password should display an error message");
    }

    [Test]
    public void LoginWithEmptyEmail_StaysOnLoginPage()
    {
        loginPage!.LoginWithoutEmail(RegisteredPassword);

        Assert.That(driver!.Url, Does.Contain("/login"),
            "Submitting login without an email should not navigate away from login page");
    }

    [Test]
    public void LoginWithEmptyPassword_StaysOnLoginPage()
    {
        loginPage!.LoginWithoutPassword(RegisteredEmail);

        Assert.That(driver!.Url, Does.Contain("/login"),
            "Submitting login without a password should not navigate away from login page");
    }
}
