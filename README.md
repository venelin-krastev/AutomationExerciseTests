![.NET](https://img.shields.io/badge/.NET-10-purple) ![Selenium](https://img.shields.io/badge/Selenium-4.46-green) ![NUnit](https://img.shields.io/badge/NUnit-4.3-blue) ![Tests](https://img.shields.io/badge/tests-2-brightgreen)

# AutomationExerciseTests

Selenium WebDriver E2E test suite for [automationexercise.com](https://www.automationexercise.com) — a full-featured e-commerce practice site.

## Tech Stack
- C# / .NET 10
- Selenium WebDriver 4.46
- Selenium.Support 4.46 (SelectElement)
- NUnit 4.3
- ChromeDriver

## Project Structure
```
Pages/
  SignupPage.cs        # Page Object for signup form (step 1 — name + email)
  AccountInfoPage.cs   # Page Object for account info form (step 2 — full registration)
DriverFactory.cs       # ChromeDriver factory — headless mode via CI env var
RegistrationTests.cs   # Registration flow tests
```

## Test Coverage

### RegistrationTests.cs

| Test | Description |
|------|-------------|
| `SignupWithNewEmail_RedirectsToAccountInfoPage` | Submitting a new email redirects to the account information form |
| `SignupWithExistingEmail_ShowsErrorMessage` | Submitting an already registered email displays "Email Address already exist!" |

## Key Concepts Demonstrated
- Page Object Model (POM) — separate page classes for each step of a multi-step flow
- `[OneTimeSetUp]` — completes a full account registration once per suite to establish test data
- Dynamic test data — timestamp-based unique email prevents cross-run conflicts
- `SelectElement` — dropdown interaction for date of birth and country fields
- `WebDriverWait` with lambda conditions — no `Thread.Sleep`
- `WebDriverWait` on `body.Text` — robust error detection independent of CSS class names
- Headless Chrome in CI via `DriverFactory` and `CI` environment variable
- GitHub Actions CI/CD — automated test run on every push

## How to Run
```bash
dotnet test
```

## Author
Venelin Krastev — Junior QA Automation Engineer, Sofia
