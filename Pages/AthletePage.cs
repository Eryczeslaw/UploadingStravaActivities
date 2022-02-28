using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UploadingStravaActivities.Pages
{
    class AthletePage
    {
        [FindsBy(How = How.XPath, Using = "//*[@id='interval-date-range']//li")]
        [CacheLookup]
        private IList<IWebElement> ListOfYears { get; set; }

        private IWebDriver driver;

        public AthletePage(IWebDriver _driver, string _athleteNumber)
        {
            driver = _driver;

            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.Navigate().GoToUrl("https://www.strava.com/athletes/" + _athleteNumber);

            PageFactory.InitElements(driver, this);
        }

        public void Navigate()
        {
            int numberOfYears = -1;
            do
            {
                if (numberOfYears > -1)
                {
                    PageFactory.InitElements(driver, this);
                    driver.FindElement(By.XPath("//*[@id='interval-date-range']")).Click();
                    ListOfYears[numberOfYears].Click();
                    Thread.Sleep(1500);
                }

                AthleteYearPage athleteYear = new AthleteYearPage(driver);
                athleteYear.Navigate();

                numberOfYears++;
            } while (numberOfYears != ListOfYears.Count);
        }
    }
}
