using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UploadingStravaActivities.Pages.Athlete
{
    class AthleteActivityFramePage
    {
        private IList<IWebElement> rideSign;
        private IList<IWebElement> map;
        private IWebElement distance;
        private IWebElement fileTime;
        private IWebElement activityHref;

        private IWebDriver driver;

        public AthleteActivityFramePage(IWebDriver _driver, int activityNumber)
        {
            driver = _driver;
            rideSign = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{activityNumber}]//*[@title='Ride']"));
            map = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{activityNumber}]//*[@class='Activity--entry-media--LkXKR']"));

            if (rideSign.Count == 1 && map.Count > 0)
            {
                distance = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{activityNumber}]//div[@class='Stat--stat-value--g-Ge3 '][text()]"));
                fileTime = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{activityNumber}]//*[@class='EntryHeader--media-body--bMdyL']/div/time"));
                activityHref = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{activityNumber}]//h3/a"));
            }
        }

        public void Navigate()
        {
            if (rideSign.Count == 1 && map.Count > 0)
            {
                string distance = this.distance.Text;
                double activityKM = Convert.ToDouble(distance.Substring(0, distance.Length - 3).Replace('.', ','));
                if (StravaTests.minumKM < activityKM)
                {
                    string fileTime = this.fileTime.Text;
                    string activityHref = this.activityHref.GetAttribute("href");

                    driver.SwitchTo().Window(driver.WindowHandles.Last());

                    AthleteActivityPage athleteActivity = new AthleteActivityPage(driver, activityHref);
                    athleteActivity.Download(fileTime);

                    driver.SwitchTo().Window(driver.WindowHandles.First());
                }
            }
        }
    }
}
