using OpenQA.Selenium;

namespace UploadingStravaActivities.Pages
{
    class LoginPage
    {
        private IWebElement emailField;
        private IWebElement passwordField;
        private IWebElement loginButton;

        private IWebDriver driver;

        public LoginPage(IWebDriver _driver)
        {
            driver = _driver;
            emailField = driver.FindElement(By.Name("email"));
            passwordField = driver.FindElement(By.Name("password"));
            loginButton = driver.FindElement(By.Id("login-button"));
        }

        public void Login(string email, string password)
        {
            emailField.SendKeys(email);
            passwordField.SendKeys(password);
            loginButton.Click();
        }
    }
}
