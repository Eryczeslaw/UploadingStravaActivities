using OpenQA.Selenium;

namespace UploadingStravaActivities.Pages
{
    class AthletePage
    {
        private IWebDriver driver;
        private string athleteNumber;

        public AthletePage(IWebDriver _driver, string _athleteNumber)
        {
            driver = _driver;
            athleteNumber = _athleteNumber;
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl("https://www.strava.com/athletes/" + athleteNumber);
        }


    }
}
