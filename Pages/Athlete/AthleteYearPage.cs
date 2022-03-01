using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;

namespace UploadingStravaActivities.Pages.Athlete
{
    class AthleteYearPage
    {
        private IList<IWebElement> listOfWeeks;
        private IWebDriver driver;

        public AthleteYearPage(IWebDriver _driver)
        {
            driver = _driver;
            listOfWeeks = driver.FindElements(By.XPath("//div[@id='interval-graph-columns']//a"));
        }

        public void Navigate()
        {
            int numberOfWeeks = 0;
            do
            {
                if (listOfWeeks.Count > 0)
                {
                    listOfWeeks[listOfWeeks.Count - numberOfWeeks - 1].Click();
                    Thread.Sleep(1500);

                    AthleteWeekPage athleteWeek = new AthleteWeekPage(driver);
                    athleteWeek.Navigate();
                }
                numberOfWeeks++;
            } while (numberOfWeeks < listOfWeeks.Count);
        }
    }
}
