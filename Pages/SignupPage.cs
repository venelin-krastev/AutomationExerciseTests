using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationExerciseTests.Pages;

public class SignupPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private static readonly By NameInput = By.CssSelector("[data-qa='signup-name']");
    private static readonly By EmailInput = By.CssSelector("[data-qa='signup-email']");
    private static readonly By SignupButton = By.CssSelector("[data-qa='signup-button']");
    private static readonly By PageBody = By.TagName("body");

    public SignupPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void NavigateTo() =>
        driver.Navigate().GoToUrl("https://www.automationexercise.com/login");

    public void SubmitSignupForm(string name, string email)
    {
        wait.Until(d => d.FindElement(NameInput)).SendKeys(name);
        wait.Until(d => d.FindElement(EmailInput)).SendKeys(email);
        wait.Until(d => d.FindElement(SignupButton)).Click();
    }

    public void SubmitSignupFormWithoutName(string email)
    {
        wait.Until(d => d.FindElement(EmailInput)).SendKeys(email);
        wait.Until(d => d.FindElement(SignupButton)).Click();
    }

    public void SubmitSignupFormWithoutEmail(string name)
    {
        wait.Until(d => d.FindElement(NameInput)).SendKeys(name);
        wait.Until(d => d.FindElement(SignupButton)).Click();
    }

    public bool ErrorIsDisplayed(string errorText) =>
        wait.Until(d => d.FindElement(PageBody).Text.Contains(errorText));
}
