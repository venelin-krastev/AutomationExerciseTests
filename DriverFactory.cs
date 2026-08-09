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
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--no-first-run");
        }
        return new ChromeDriver(options);
    }
}
