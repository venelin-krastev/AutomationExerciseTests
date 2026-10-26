using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationExerciseTests.Pages;

public class CheckoutPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private static readonly By OrderCommentField = By.CssSelector("[name='message']");
    private static readonly By PlaceOrderButton = By.CssSelector("[data-qa='place-order']");
    private static readonly By CartSection = By.Id("cart_items");

    public CheckoutPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public bool HasCartItems() =>
        wait.Until(d => d.FindElement(CartSection)).Displayed;

    public CheckoutPage AddComment(string comment)
    {
        wait.Until(d => d.FindElement(OrderCommentField)).SendKeys(comment);
        return this;
    }

    public PaymentPage ProceedToPayment()
    {
        wait.Until(d => d.FindElement(PlaceOrderButton)).Click();
        return new PaymentPage(driver);
    }
}
