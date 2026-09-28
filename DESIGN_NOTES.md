# Design Notes — AutomationExerciseTests

Decisions made in this project, why, and the trade-offs involved.

---

## 2026-09-06 — Project Evolution Narrative

**Q: I see two UI automation repos on your GitHub — TheInternetTests and web-ui-automation-csharp. Why two?**
A: `TheInternetTests` is my first UI automation project — written earlier, with a simpler structure and a sentence-style naming convention. `web-ui-automation-csharp` is the evolution: I introduced a `BaseTest` abstraction to manage the driver lifecycle in one place, adopted `Should_X_When_Y` naming for clearer intent, added automatic screenshot capture on failure, and set up GitHub Actions CI. The two repos show iteration — how my thinking developed over time.

**Q: Which one is more recent?**
A: `web-ui-automation-csharp` is the more recent and more mature version. `TheInternetTests` is v1 — I keep it because it demonstrates where I started and shows progression, not because it is production-quality.

---

## 2026-09-06 — Abstract Classes & Test Architecture

**Q: Can an abstract class contain virtual methods with implementations?**
A: Yes. An abstract class can mix abstract members (no body — subclasses must implement) and virtual members (have a body — subclasses may override). This is exactly how `BaseTest` works: it provides a default `SetUp` and `TearDown` implementation that subclasses inherit, while leaving room to override for additional setup in specific test classes.

**Q: Why use BaseTest instead of duplicating SetUp in every test class?**
A: If screenshot-on-failure logic needs updating, I change it in one place — `BaseTest` — not in every test class. It also enforces consistency: every test class disposes the driver correctly because `TearDown` is inherited, not copy-pasted.

---

## 2026-09-06 — Smoke vs Regression

**Q: Walk me through your testing sequence after a new build is deployed.**
A: I run smoke tests first — a quick check that the application starts and the core flows are reachable. If smoke passes, I run regression to verify that the new changes have not broken existing functionality. Smoke is the gate: if it fails, there is no point running the full regression suite against a broken build.

**Q: What is the difference between regression testing and retesting?**
A: Retesting verifies that a specific bug that was fixed is now resolved — you run the exact scenario that failed before. Regression is broader: you re-run the full suite (or a risk-based subset) to check that the fix has not introduced new failures elsewhere. Both happen after a fix, but they have different scope.

---

## 2026-09-28 — Whitespace-padded email actually succeeds, not fails

**Decision:** `LoginWithWhitespacePaddedEmail_ShowsErrorMessage` was renamed to `LoginWithWhitespacePaddedEmail_StillSucceeds` and its assertion flipped from expecting an error to expecting a successful login.
**Why:** The test had been red on every CI run since it was added (#12, #13) — `WebDriverTimeoutException` waiting for an error that never appeared. Root cause, confirmed by driving a real browser through the exact scenario (not guessed): the login email field is `<input type="email">`, and per the HTML5 value sanitization algorithm, the browser strips leading/trailing whitespace from the value before it's ever read by JS or submitted — confirmed by reading `el.value` immediately after typing, before any interaction with the site. So a padded email can't actually reach the server padded through a real browser; login succeeds exactly like an unpadded attempt. The original test encoded an impossible premise — no site-level whitespace handling to test here, because the browser itself sanitizes the input first.
**Trade-off:** none — this isn't a compromise, it's correcting a test that asserted something the platform makes unreachable. If the intent is to test whether the *server* independently trims whitespace (bypassing the browser's own sanitization), that would need an API-level test hitting the endpoint directly, not a Selenium UI test — out of scope here.
