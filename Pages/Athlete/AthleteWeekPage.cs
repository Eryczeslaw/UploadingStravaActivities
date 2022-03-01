using OpenQA.Selenium;
using System.Collections.Generic;

namespace UploadingStravaActivities.Pages.Athlete
{
    class AthleteWeekPage
    {
        private IList<IWebElement> activities;
        private IWebDriver driver;

        public AthleteWeekPage(IWebDriver _driver)
        {
            driver = _driver;
            activities = driver.FindElements(By.XPath("//*[@id='interval-rides']/div/div"));
        }

        public void Navigate()
        {
            int numberOfActivities = 0;
            while (numberOfActivities < activities.Count)
            {
                AthleteActivityFramePage activityFrame = new AthleteActivityFramePage(driver, numberOfActivities + 1);
                activityFrame.Navigate();

                numberOfActivities++;
            }
        }
    }
}
