using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationExerciseTests.Pages;

public class ProductPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    // Site removed data-qa='add-to-cart' attribute; current selector is the .cart class on the button.
    private static readonly By AddToCartButton = By.CssSelector("button.cart");
    private static readonly By ContinueShoppingButton = By.CssSelector("[data-dismiss='modal']");
    private static readonly By ViewCartLink = By.CssSelector("p > a[href='/view_cart']");

    public ProductPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void NavigateTo(int productId) =>
        driver.Navigate().GoToUrl($"https://www.automationexercise.com/product_details/{productId}");

    public ProductPage AddToCart()
    {
        JsClick(wait.Until(d => d.FindElement(AddToCartButton)));
        // Modal appears after add — dismiss it to stay on product page
        wait.Until(d => d.FindElement(ContinueShoppingButton)).Click();
        return this;
    }

    public CartPage AddToCartAndViewCart()
    {
        JsClick(wait.Until(d => d.FindElement(AddToCartButton)));
        // Modal appears — click "View Cart". Modal animation may still be running so use JS click.
        JsClick(wait.Until(d => d.FindElement(ViewCartLink)));
        return new CartPage(driver);
    }

    // The site shows ad overlays that intercept regular Selenium clicks.
    // JS click bypasses the overlay and fires directly on the element.
    private void JsClick(IWebElement element) =>
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element);
}
