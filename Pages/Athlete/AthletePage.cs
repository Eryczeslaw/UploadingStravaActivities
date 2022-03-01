using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UploadingStravaActivities.Pages.Athlete
{
    class AthletePage
    {
        private IList<IWebElement> listOfYears;
        private IWebDriver driver;

        public AthletePage(IWebDriver _driver, string _athleteNumber)
        {
            driver = _driver;

            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Navigate().GoToUrl("https://www.strava.com/athletes/" + _athleteNumber);

            listOfYears = driver.FindElements(By.XPath("//*[@id='interval-date-range']//li"));
        }

        public void Navigate()
        {
            int numberOfYears = -1;
            do
            {
                if (numberOfYears > -1)
                {
                    driver.FindElement(By.XPath("//*[@id='interval-date-range']")).Click();
                    listOfYears[numberOfYears].Click();
                    Thread.Sleep(1500);
                }

                AthleteYearPage athleteYear = new AthleteYearPage(driver);
                athleteYear.Navigate();

                numberOfYears++;
            } while (numberOfYears != listOfYears.Count);
        }
    }
}
