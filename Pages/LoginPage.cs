using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationExerciseTests.Pages;

public class LoginPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private static readonly By EmailInput = By.CssSelector("[data-qa='login-email']");
    private static readonly By PasswordInput = By.CssSelector("[data-qa='login-password']");
    private static readonly By LoginButton = By.CssSelector("[data-qa='login-button']");
    private static readonly By PageBody = By.TagName("body");

    public LoginPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void NavigateTo() =>
        driver.Navigate().GoToUrl("https://www.automationexercise.com/login");

    public void Login(string email, string password)
    {
        wait.Until(d => d.FindElement(EmailInput)).SendKeys(email);
        wait.Until(d => d.FindElement(PasswordInput)).SendKeys(password);
        wait.Until(d => d.FindElement(LoginButton)).Click();
    }

    public void LoginWithoutEmail(string password)
    {
        wait.Until(d => d.FindElement(PasswordInput)).SendKeys(password);
        wait.Until(d => d.FindElement(LoginButton)).Click();
    }

    public void LoginWithoutPassword(string email)
    {
        wait.Until(d => d.FindElement(EmailInput)).SendKeys(email);
        wait.Until(d => d.FindElement(LoginButton)).Click();
    }

    public bool ErrorIsDisplayed(string errorText) =>
        wait.Until(d => d.FindElement(PageBody).Text.Contains(errorText));
}
