using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Threading;

namespace UploadingStravaActivities.Pages
{
    class AthleteYearPage
    {
        [FindsBy(How = How.XPath, Using = "//div[@id='interval-graph-columns']//a")]
        [CacheLookup]
        private IList<IWebElement> ListOfWeeks { get; set; }

        private IWebDriver driver;

        public AthleteYearPage(IWebDriver _driver)
        {
            driver = _driver;
            PageFactory.InitElements(driver, this);
        }

        public void Navigate()
        {
            int numberOfWeeks = 0;
            do
            {
                if (ListOfWeeks.Count > 0)
                {
                    ListOfWeeks[ListOfWeeks.Count - numberOfWeeks - 1].Click();
                    Thread.Sleep(1500);

                    AthleteWeekPage athleteWeek = new AthleteWeekPage(driver);
                    athleteWeek.Navigate();
                }
                numberOfWeeks++;
            } while (numberOfWeeks < ListOfWeeks.Count);
        }
    }
}
