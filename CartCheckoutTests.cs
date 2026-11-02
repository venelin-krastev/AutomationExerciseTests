using NUnit.Framework;
using OpenQA.Selenium;
using AutomationExerciseTests.Pages;

namespace AutomationExerciseTests;

[TestFixture]
public class CartCheckoutTests
{
    private IWebDriver? driver;
    private LoginPage? loginPage;

    [SetUp]
    public void Setup()
    {
        driver = DriverFactory.Create();
        loginPage = new LoginPage(driver);
        loginPage.NavigateTo();
        loginPage.Login(AssemblySetup.RegisteredEmail, AssemblySetup.RegisteredPassword);
    }

    [TearDown]
    public void Teardown()
    {
        driver?.Quit();
        driver?.Dispose();
    }

    [Test]
    public void AddProductToCart_IncreasesCartItemCount()
    {
        var productPage = new ProductPage(driver!);
        productPage.NavigateTo(1);

        var cartPage = productPage.AddToCartAndViewCart();

        Assert.That(cartPage.GetCartItemCount(), Is.GreaterThan(0),
            "Cart should contain at least one item after adding a product");
    }

    [Test]
    public void AddProductToCart_CartIsNotEmpty()
    {
        var productPage = new ProductPage(driver!);
        productPage.NavigateTo(1);
        productPage.AddToCartAndViewCart();

        var cartPage = new CartPage(driver!);

        Assert.That(cartPage.IsEmpty(), Is.False,
            "Cart should not be empty after adding a product");
    }

    [Test]
    public void CheckoutPage_ShowsCartItems_WhenLoggedIn()
    {
        var productPage = new ProductPage(driver!);
        productPage.NavigateTo(1);
        var cartPage = productPage.AddToCartAndViewCart();

        var checkoutPage = cartPage.ProceedToCheckout();

        Assert.That(checkoutPage.HasCartItems(), Is.True,
            "Checkout page should display the cart items from the previous step");
    }

    [Test]
    public void PlaceOrder_WithValidPaymentDetails_ShowsOrderConfirmation()
    {
        var productPage = new ProductPage(driver!);
        productPage.NavigateTo(1);
        var cartPage = productPage.AddToCartAndViewCart();
        var checkoutPage = cartPage.ProceedToCheckout();
        var paymentPage = checkoutPage.AddComment("Test order").ProceedToPayment();

        var confirmationPage = paymentPage.PayWithCard(
            name: "Test User",
            number: "4111111111111111",
            cvc: "123",
            month: "12",
            year: "2027"
        );

        Assert.That(confirmationPage.IsOrderPlaced(), Is.True,
            "A successful payment should display the order confirmation message");
    }
}
