using NUnit.Framework;
using OpenQA.Selenium;
using AutomationExerciseTests.Pages;

namespace AutomationExerciseTests;

[TestFixture]
public class LoginTests
{
    private IWebDriver? driver;
    private LoginPage? loginPage;

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
        loginPage!.Login(AssemblySetup.RegisteredEmail, AssemblySetup.RegisteredPassword);

        Assert.That(driver!.Url, Does.Not.Contain("/login"),
            "Successful login should navigate away from the login page");
    }

    [Test]
    public void LoginWithInvalidPassword_ShowsErrorMessage()
    {
        loginPage!.Login(AssemblySetup.RegisteredEmail, "wrongpassword");

        Assert.That(loginPage!.ErrorIsDisplayed("Your email or password is incorrect!"), Is.True,
            "Login with an incorrect password should display an error message");
    }

    [Test]
    public void LoginWithEmptyEmail_StaysOnLoginPage()
    {
        loginPage!.LoginWithoutEmail(AssemblySetup.RegisteredPassword);

        Assert.That(driver!.Url, Does.Contain("/login"),
            "Submitting login without an email should not navigate away from login page");
    }

    [Test]
    public void LoginWithEmptyPassword_StaysOnLoginPage()
    {
        loginPage!.LoginWithoutPassword(AssemblySetup.RegisteredEmail);

        Assert.That(driver!.Url, Does.Contain("/login"),
            "Submitting login without a password should not navigate away from login page");
    }
}
