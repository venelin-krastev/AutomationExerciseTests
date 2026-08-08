using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationExerciseTests;

public static class DriverFactory
{
    public static IWebDriver Create()
    {
        var options = new ChromeOptions();
        if (Environment.GetEnvironmentVariable("CI") == "true")
        {
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
        }
        return new ChromeDriver(options);
    }
}
