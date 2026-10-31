using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationExerciseTests.Pages;

public class PaymentPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private static readonly By NameOnCardInput = By.CssSelector("[data-qa='name-on-card']");
    private static readonly By CardNumberInput = By.CssSelector("[data-qa='card-number']");
    private static readonly By CvcInput = By.CssSelector("[data-qa='cvc']");
    private static readonly By ExpiryMonthInput = By.CssSelector("[data-qa='expiry-month']");
    private static readonly By ExpiryYearInput = By.CssSelector("[data-qa='expiry-year']");
    private static readonly By PayButton = By.CssSelector("[data-qa='pay-button']");

    public PaymentPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public OrderConfirmationPage PayWithCard(string name, string number, string cvc, string month, string year)
    {
        wait.Until(d => d.FindElement(NameOnCardInput)).SendKeys(name);
        wait.Until(d => d.FindElement(CardNumberInput)).SendKeys(number);
        wait.Until(d => d.FindElement(CvcInput)).SendKeys(cvc);
        wait.Until(d => d.FindElement(ExpiryMonthInput)).SendKeys(month);
        wait.Until(d => d.FindElement(ExpiryYearInput)).SendKeys(year);
        wait.Until(d => d.FindElement(PayButton)).Click();
        return new OrderConfirmationPage(driver);
    }
}
