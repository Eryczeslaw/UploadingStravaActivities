using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UploadingStravaActivities.Pages
{
    class AthleteWeekPage
    {
        [FindsBy(How = How.XPath, Using = "//*[@id='interval-rides']/div/div")]
        [CacheLookup]
        private IList<IWebElement> Activities { get; set; }

        private IWebDriver driver;
        private double minumKM = 15;

        public AthleteWeekPage(IWebDriver _driver)
        {
            driver = _driver;
            PageFactory.InitElements(driver, this);
        }

        public void Navigate()
        {
            int numberOfActivities = 0;
            while (numberOfActivities < Activities.Count)
            {
                // AthleteActivityFramePage (numberOfActivities, activity)

                //AthleteActivityFramePage activityFrame = new AthleteActivityFramePage(driver, numberOfActivities);
                //activityFrame.Navigate();

                IReadOnlyList<IWebElement> ride = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@title='Ride']"));
                IReadOnlyList<IWebElement> map = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@class='Activity--entry-media--LkXKR']"));
                if (ride.Count == 1 && map.Count > 0)
                {
                    string distance = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//div[@class='Stat--stat-value--g-Ge3 '][text()]")).Text;
                    double activityKM = Convert.ToDouble(distance.Substring(0, distance.Length - 3).Replace('.', ','));
                    if (minumKM < activityKM)
                    {
                        string fileTime = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@class='EntryHeader--media-body--bMdyL']/div/time")).Text;
                        string activityHref = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//h3/a")).GetAttribute("href");

                        driver.SwitchTo().Window(driver.WindowHandles.Last());
                        driver.Navigate().GoToUrl(activityHref);

                        // AthleteActivityPage (fileTime)

                        AthleteActivityPage athleteActivity = new AthleteActivityPage(driver);
                        athleteActivity.Download(fileTime);

                        // AthleteActivityPage
                        driver.SwitchTo().Window(driver.WindowHandles.First());
                    }
                }

                // AthleteActivityFramePage
                numberOfActivities++;
            }
        }
    }
}
