using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UploadingStravaActivities.Pages
{
    class AthleteActivityFramePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@title='Ride']")]
        [CacheLookup]
        private IList<IWebElement> RideSign { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='Activity--entry-media--LkXKR']")]
        [CacheLookup]
        private IList<IWebElement> Map { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='Stat--stat-value--g-Ge3 '][text()]")]
        [CacheLookup]
        private IWebElement Distance { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='interval-rides']/div/div[2]//*[@class='EntryHeader--media-body--bMdyL']/div/time")]
        [CacheLookup]
        private IWebElement FileTime { get; set; }

        [FindsBy(How = How.XPath, Using = "//h3/a")]
        [CacheLookup]
        private IWebElement ActivityHref { get; set; }

        private IWebDriver driver;
        private double minumKM = 15;

        public AthleteActivityFramePage(IWebDriver _driver, int numberOfActivities)
        {
            driver = _driver;
            PageFactory.InitElements(driver, this);
        }
        //*[@id='interval-rides']/div/div[{numberOfActivities + 1}]
        public void Navigate()
        {
            if (RideSign.Count == 1 && Map.Count > 0)
            {
                string distance = Distance.Text;
                double activityKM = Convert.ToDouble(distance.Substring(0, distance.Length - 3).Replace('.', ','));
                if (minumKM < activityKM)
                {
                    string fileTime = FileTime.Text;
                    string activityHref = ActivityHref.GetAttribute("href");

                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    driver.Navigate().GoToUrl(activityHref);

                    AthleteActivityPage athleteActivity = new AthleteActivityPage(driver);
                    athleteActivity.Download(fileTime);

                    driver.SwitchTo().Window(driver.WindowHandles.First());
                }
            }
        }
    }
}
