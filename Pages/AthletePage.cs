using OpenQA.Selenium;

namespace UploadingStravaActivities.Pages
{
    class AthletePage
    {
        private IWebDriver driver;
        private string athleteNumber;

        public AthletePage(IWebDriver _driver, string athleteNumber)
        {
            driver = _driver;
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl("https://www.strava.com/athletes/" + athleteNumber);
        }


    }
}
