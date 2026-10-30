using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationExerciseTests.Pages;

public class OrderConfirmationPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private static readonly By SuccessMessage = By.CssSelector("[data-qa='order-placed']");

    public OrderConfirmationPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public bool IsOrderPlaced() =>
        wait.Until(d => {
            try { return d.FindElement(SuccessMessage).Displayed; }
            catch (NoSuchElementException) { return false; }
        });

    public string GetConfirmationText() =>
        wait.Until(d => d.FindElement(SuccessMessage)).Text;
}
