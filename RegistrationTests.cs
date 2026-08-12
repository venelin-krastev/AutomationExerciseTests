using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AutomationExerciseTests.Pages;

namespace AutomationExerciseTests;

[TestFixture]
public class RegistrationTests
{
    private IWebDriver? driver;
    private SignupPage? signupPage;

    [SetUp]
    public void Setup()
    {
        driver = DriverFactory.Create();
        signupPage = new SignupPage(driver);
        signupPage.NavigateTo();
    }

    [TearDown]
    public void Teardown()
    {
        driver?.Quit();
        driver?.Dispose();
    }

    [Test]
    public void SignupWithNewEmail_RedirectsToAccountInfoPage()
    {
        var uniqueEmail = $"qa_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}@test.com";

        signupPage!.SubmitSignupForm("QA Test User", uniqueEmail);

        new WebDriverWait(driver!, TimeSpan.FromSeconds(15))
            .Until(d => d.Url.Contains("/signup"));

        Assert.That(driver!.Url, Does.Contain("/signup"),
            "Signing up with a new email should redirect to the account information page");
    }

    [Test]
    public void SignupWithExistingEmail_ShowsErrorMessage()
    {
        signupPage!.SubmitSignupForm("QA Test User", AssemblySetup.RegisteredEmail);

        Assert.That(signupPage!.ErrorIsDisplayed("Email Address already exist!"), Is.True,
            "Signing up with an already registered email should display an error message");
    }

    [Test]
    public void SignupWithEmptyName_StaysOnLoginPage()
    {
        signupPage!.SubmitSignupFormWithoutName("qa_noname@test.com");

        Assert.That(driver!.Url, Does.Contain("/login"),
            "Submitting signup without a name should not navigate away from login page");
    }

    [Test]
    public void SignupWithEmptyEmail_StaysOnLoginPage()
    {
        signupPage!.SubmitSignupFormWithoutEmail("QA Test User");

        Assert.That(driver!.Url, Does.Contain("/login"),
            "Submitting signup without an email should not navigate away from login page");
    }

    [Test]
    public void CompletedRegistration_ShowsAccountCreatedPage()
    {
        var uniqueEmail = $"qa_full_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}@test.com";

        signupPage!.SubmitSignupForm("QA Full User", uniqueEmail);

        new WebDriverWait(driver!, TimeSpan.FromSeconds(15))
            .Until(d => d.Url.Contains("/signup"));

        new AccountInfoPage(driver!).CompleteRegistration();

        Assert.That(driver!.Url, Does.Contain("/account_created"),
            "Completing the full registration form should navigate to the account created confirmation page");
    }
}
