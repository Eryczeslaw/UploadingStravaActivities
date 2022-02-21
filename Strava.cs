using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Threading;

namespace UploadingStravaActivities
{
    public class Strava
    {
        public static void LogIn(IWebDriver driver, string emailEntry, string passwordEntry)
        {
            driver.Navigate().GoToUrl("http://strava.com/login");

            IWebElement emailField = driver.FindElement(By.CssSelector("[name='email']"));
            emailField.SendKeys(emailEntry);

            IWebElement passwordField = driver.FindElement(By.CssSelector("[name='password']"));
            passwordField.SendKeys(passwordEntry);

            driver.FindElement(By.CssSelector("[id='login-button']")).Click();
        }

        public static void DownloadActivities(IWebDriver driver, string Url)
        {
            driver.Navigate().GoToUrl(Url);

            IReadOnlyList<IWebElement> listOfYears = driver.FindElements(By.XPath("//*[@id='interval-date-range']//li"));
            IReadOnlyList<IWebElement> listOfWeeks;
            IReadOnlyList<IWebElement> activities;
            IWebElement activity;
            IReadOnlyList<IWebElement> ride;
            IReadOnlyList<IWebElement> map;
            string firstYear = driver.FindElement(By.XPath("//*[@id='interval-date-range']")).Text;
            IReadOnlyList<IWebElement> currentYear;

            int numberOfYears = -1;
            do
            {
                int numberOfWeeks = 0;
                do
                {
                    currentYear = driver.FindElements(By.XPath($"//*[@id='interval-date-range']/div/div[text()='{firstYear}']"));
                    if (currentYear.Count == 1 && numberOfYears > -1)
                    {
                        listOfYears = driver.FindElements(By.XPath("//*[@id='interval-date-range']//li"));
                        driver.FindElement(By.XPath("//*[@id='interval-date-range']")).Click();
                        listOfYears[numberOfYears].Click();
                    }

                    Thread.Sleep(1000);
                    listOfWeeks = driver.FindElements(By.XPath("//div[@id='interval-graph-columns']//a"));
                    listOfWeeks[listOfWeeks.Count - numberOfWeeks - 1].Click();
                    Thread.Sleep(1000);

                    activities = driver.FindElements(By.XPath("//*[@id='interval-rides']/div/div"));

                    int numberOfActivities = 0;
                    while (numberOfActivities < activities.Count)
                    {
                        ride = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@title='Ride']"));
                        map = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@class='Activity--entry-media--LkXKR']"));
                        if (ride.Count == 1 && map.Count == 1)
                        {
                            string fileTime = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@class='EntryHeader--media-body--bMdyL']/div/time")).Text;
                            activity = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//h3/a"));
                            activity.Click();

                            driver.FindElement(By.XPath("//*[@title='Actions']")).Click();
                            driver.FindElement(By.XPath("//a[text()='Export GPX']")).Click();

                            string fileName = driver.FindElement(By.XPath("//*[@id='heading']//div[@class='details']/h1")).Text;
                            string fileDate = driver.FindElement(By.XPath("//*[@id='heading']//div[@class='details']/time")).Text;

                            Thread.Sleep(1500);
                            EditFiles.Save(fileName, fileTime, fileDate);

                            driver.Navigate().Back();
                            Thread.Sleep(1000);
                        }
                        numberOfActivities++;
                    }

                    numberOfWeeks++;
                } while (numberOfWeeks < listOfWeeks.Count);

                numberOfYears++;
            } while (numberOfYears != listOfYears.Count);
        }

        public static void UploadActivities(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id='container-nav']/ul[2]/li[4]/a")).Click();
            driver.FindElement(By.XPath("//*[@id='from-file-js']/a")).Click();
            //driver.FindElement(By.XPath("//*[@id='uploadFile']//*[@class='files']")).Click();
        }

        public static void DownloadActivitiesNewTab(IWebDriver driver, string Url)
        {
            Strava.LogIn(driver, "fejk@buziaczek.pl", "!Fejk123");
            IWebDriver newTab = new ChromeDriver();
            newTab = driver.SwitchTo().NewWindow(WindowType.Tab);
            driver.Navigate().GoToUrl("http://strava.com/login");
            newTab.Navigate().GoToUrl("http://strava.com/login");
            newTab.Close();


            IWebElement body = driver.FindElement(By.TagName("body"));
            // body.SendKeys(Keys.Control + "t");
            //Thread.Sleep(5000);
            //body.SendKeys(Keys.Control + "w");
            Actions action = new Actions(driver);
            action.KeyDown(Keys.Control).MoveToElement(body).Click().Perform();
            //action.KeyDown(Keys.Control + "t").Click().Perform();

        }
    }
}
