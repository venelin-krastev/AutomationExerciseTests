using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationExerciseTests.Pages;

public class AccountInfoPage
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait;

    private static readonly By PasswordInput = By.CssSelector("[data-qa='password']");
    private static readonly By DaysSelect = By.CssSelector("[data-qa='days']");
    private static readonly By MonthsSelect = By.CssSelector("[data-qa='months']");
    private static readonly By YearsSelect = By.CssSelector("[data-qa='years']");
    private static readonly By FirstNameInput = By.CssSelector("[data-qa='first_name']");
    private static readonly By LastNameInput = By.CssSelector("[data-qa='last_name']");
    private static readonly By Address1Input = By.CssSelector("[data-qa='address']");
    private static readonly By CountrySelect = By.CssSelector("[data-qa='country']");
    private static readonly By StateInput = By.CssSelector("[data-qa='state']");
    private static readonly By CityInput = By.CssSelector("[data-qa='city']");
    private static readonly By ZipcodeInput = By.CssSelector("[data-qa='zipcode']");
    private static readonly By MobileInput = By.CssSelector("[data-qa='mobile_number']");
    private static readonly By CreateAccountButton = By.CssSelector("[data-qa='create-account']");

    public AccountInfoPage(IWebDriver driver)
    {
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void CompleteRegistration()
    {
        wait.Until(d => d.FindElement(PasswordInput)).SendKeys("Test1234!");

        new SelectElement(wait.Until(d => d.FindElement(DaysSelect))).SelectByText("15");
        new SelectElement(wait.Until(d => d.FindElement(MonthsSelect))).SelectByText("May");
        new SelectElement(wait.Until(d => d.FindElement(YearsSelect))).SelectByText("1990");

        wait.Until(d => d.FindElement(FirstNameInput)).SendKeys("QA");
        wait.Until(d => d.FindElement(LastNameInput)).SendKeys("Tester");
        wait.Until(d => d.FindElement(Address1Input)).SendKeys("123 Test Street");

        new SelectElement(wait.Until(d => d.FindElement(CountrySelect))).SelectByText("United States");

        wait.Until(d => d.FindElement(StateInput)).SendKeys("England");
        wait.Until(d => d.FindElement(CityInput)).SendKeys("London");
        wait.Until(d => d.FindElement(ZipcodeInput)).SendKeys("SW1A 1AA");
        wait.Until(d => d.FindElement(MobileInput)).SendKeys("07911123456");

        wait.Until(d => d.FindElement(CreateAccountButton)).Click();
    }
}
