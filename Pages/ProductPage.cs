using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationExerciseTests.Pages;

public class ProductPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private static readonly By AddToCartButton = By.CssSelector("[data-qa='add-to-cart']");
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
        wait.Until(d => d.FindElement(AddToCartButton)).Click();
        // Modal appears after add — dismiss it to stay on product page
        wait.Until(d => d.FindElement(ContinueShoppingButton)).Click();
        return this;
    }

    public CartPage AddToCartAndViewCart()
    {
        wait.Until(d => d.FindElement(AddToCartButton)).Click();
        // Modal appears — click "View Cart" instead of "Continue Shopping"
        wait.Until(d => d.FindElement(ViewCartLink)).Click();
        return new CartPage(driver);
    }
}
