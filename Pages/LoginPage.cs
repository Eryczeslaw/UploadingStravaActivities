using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace UploadingStravaActivities.Pages
{
    class LoginPage
    {
        [FindsBy(How = How.Name, Using = "email")]
        [CacheLookup]
        private IWebElement EmailField { get; set; }

        [FindsBy(How = How.Name, Using = "password")]
        [CacheLookup]
        private IWebElement PasswordField { get; set; }

        [FindsBy(How = How.Id, Using = "login-button")]
        [CacheLookup]
        private IWebElement LoginButton { get; set; }

        private IWebDriver driver;


        public LoginPage(IWebDriver _driver)
        {
            driver = _driver;
            PageFactory.InitElements(driver, this);
        }

        public void Login(string email, string password)
        {
            driver.Navigate().GoToUrl("http://strava.com/login");

            EmailField.SendKeys(email);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }
    }
}
