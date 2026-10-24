using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationExerciseTests.Pages;

public class CartPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private static readonly By CartRows = By.CssSelector("#cart_info_table tbody tr");
    private static readonly By ProceedToCheckoutButton = By.CssSelector("[data-qa='proceed-to-checkout']");
    private static readonly By LoginToCheckoutLink = By.CssSelector(".modal-body a[href='/login']");

    public CartPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void NavigateTo() =>
        driver.Navigate().GoToUrl("https://www.automationexercise.com/view_cart");

    public int GetCartItemCount() =>
        wait.Until(d => d.FindElements(CartRows)).Count;

    public bool IsEmpty() => GetCartItemCount() == 0;

    public CheckoutPage ProceedToCheckout()
    {
        wait.Until(d => d.FindElement(ProceedToCheckoutButton)).Click();
        return new CheckoutPage(driver);
    }

    public LoginPage ProceedToCheckoutAsGuest()
    {
        wait.Until(d => d.FindElement(ProceedToCheckoutButton)).Click();
        // Modal prompts login — click "Register / Login"
        wait.Until(d => d.FindElement(LoginToCheckoutLink)).Click();
        return new LoginPage(driver);
    }
}
