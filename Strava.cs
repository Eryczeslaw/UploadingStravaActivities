using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UploadingStravaActivities.FilesModification;

namespace UploadingStravaActivities
{
    public class Strava
    {
        private IWebDriver driver;
        private WaitHelper wait;
        private double minumKM = 15;
        private string downloadPath = $@"C:\Users\erykh\Downloads";

        public Strava(IWebDriver _driver)
        {
            driver = _driver;
            wait = new WaitHelper(driver);
        }

        public void DownloadActivities(string athleteNumber)
        {
            driver.Navigate().GoToUrl("https://www.strava.com/athletes/" + athleteNumber);

            IReadOnlyList<IWebElement> listOfYears = driver.FindElements(By.XPath("//*[@id='interval-date-range']//li"));
            IReadOnlyList<IWebElement> listOfWeeks;
            string firstYear = driver.FindElement(By.XPath("//*[@id='interval-date-range']")).Text;

            int numberOfYears = -1;
            do
            {
                int numberOfWeeks = 0;
                do
                {
                    IReadOnlyList<IWebElement> currentYear = driver.FindElements(By.XPath($"//*[@id='interval-date-range']/div/div[text()='{firstYear}']"));
                    if (currentYear.Count == 1 && numberOfYears > -1)
                    {
                        listOfYears = driver.FindElements(By.XPath("//*[@id='interval-date-range']//li"));
                        driver.FindElement(By.XPath("//*[@id='interval-date-range']")).Click();
                        listOfYears[numberOfYears].Click();
                        Thread.Sleep(1500);
                    }

                    listOfWeeks = driver.FindElements(By.XPath("//div[@id='interval-graph-columns']//a"));
                    listOfWeeks[listOfWeeks.Count - numberOfWeeks - 1].Click();
                    Thread.Sleep(1500);
                    string week = driver.FindElement(By.Id("interval-value")).Text;

                    IReadOnlyList<IWebElement> activities = driver.FindElements(By.XPath("//*[@id='interval-rides']/div/div"));
                    int numberOfActivities = 0;
                    while (numberOfActivities < activities.Count)
                    {
                        try
                        {
                            wait.WaitUntilElementLoad("//*[@id='interval-value']", week, 5);
                        }
                        catch (TimeoutException)
                        {
                            listOfWeeks = driver.FindElements(By.XPath("//div[@id='interval-graph-columns']//a"));
                            listOfWeeks[listOfWeeks.Count - numberOfWeeks - 1].Click();
                            Thread.Sleep(1500);
                        }

                        IReadOnlyList<IWebElement> ride = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@title='Ride']"));
                        IReadOnlyList<IWebElement> map = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@class='Activity--entry-media--LkXKR']"));
                        if (ride.Count == 1 && map.Count > 0)
                        {
                            string distance = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//div[@class='Stat--stat-value--g-Ge3 '][text()]")).Text;
                            double activityKM = Convert.ToDouble(distance.Substring(0, distance.Length - 3).Replace('.', ','));
                            if (minumKM < activityKM)
                            {
                                string fileTime = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@class='EntryHeader--media-body--bMdyL']/div/time")).Text;
                                IWebElement activity = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//h3/a"));
                                activity.Click();

                                driver.FindElement(By.XPath("//*[@title='Actions']")).Click();
                                driver.FindElement(By.XPath("//a[text()='Export GPX']")).Click();

                                string fileName = driver.FindElement(By.XPath("//*[@id='heading']//div[@class='details']/h1")).Text;
                                string fileDate = driver.FindElement(By.XPath("//*[@id='heading']//div[@class='details']/time")).Text;
                                string movingTime = driver.FindElement(By.XPath("//*[@id='heading']/div/div/div[2]/ul[1]/li[2]/strong")).Text;

                                Thread.Sleep(1000);
                                string newPath = SavingFiles.Save(downloadPath, fileName, fileDate, fileTime);
                                TxtEdit.Update(newPath, fileDate, fileTime, movingTime);
                                //GpxEdit.Update(newPath, fileDate, fileTime, movingTime);

                                driver.Navigate().Back();
                            }
                        }
                        numberOfActivities++;
                    }

                    numberOfWeeks++;
                } while (numberOfWeeks < listOfWeeks.Count);

                numberOfYears++;
            } while (numberOfYears != listOfYears.Count);
        }

        public void DownloadActivitiesNewTab(string athleteNumber)
        {
            driver.Navigate().GoToUrl("https://www.strava.com/athletes/" + athleteNumber);
            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");
            driver.SwitchTo().Window(driver.WindowHandles.First());

            IReadOnlyList<IWebElement> listOfYears = driver.FindElements(By.XPath("//*[@id='interval-date-range']//li"));
            IReadOnlyList<IWebElement> listOfWeeks;
            string firstYear = driver.FindElement(By.XPath("//*[@id='interval-date-range']")).Text;

            int numberOfYears = -1;
            do
            {
                IReadOnlyList<IWebElement> currentYear = driver.FindElements(By.XPath($"//*[@id='interval-date-range']/div/div[text()='{firstYear}']"));
                if (currentYear.Count == 1 && numberOfYears > -1)
                {
                    listOfYears = driver.FindElements(By.XPath("//*[@id='interval-date-range']//li"));
                    driver.FindElement(By.XPath("//*[@id='interval-date-range']")).Click();
                    listOfYears[numberOfYears].Click();
                    Thread.Sleep(1500);
                }

                int numberOfWeeks = 0;
                do
                {
                    listOfWeeks = driver.FindElements(By.XPath("//div[@id='interval-graph-columns']//a"));
                    listOfWeeks[listOfWeeks.Count - numberOfWeeks - 1].Click();
                    Thread.Sleep(1500);

                    IReadOnlyList<IWebElement> activities = driver.FindElements(By.XPath("//*[@id='interval-rides']/div/div"));
                    int numberOfActivities = 0;
                    while (numberOfActivities < activities.Count)
                    {
                        IReadOnlyList<IWebElement> ride = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@title='Ride']"));
                        IReadOnlyList<IWebElement> map = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@class='Activity--entry-media--LkXKR']"));
                        if (ride.Count == 1 && map.Count == 1)
                        {
                            string distance = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//div[@class='Stat--stat-value--g-Ge3 '][text()]")).Text;
                            double activityKM = Convert.ToDouble(distance.Substring(0, distance.Length - 3).Replace('.', ','));
                            if (minumKM < activityKM)
                            {
                                string fileTime = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@class='EntryHeader--media-body--bMdyL']/div/time")).Text;
                                string activityHref = driver.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//h3/a")).GetAttribute("href");

                                driver.SwitchTo().Window(driver.WindowHandles.Last());
                                driver.Navigate().GoToUrl(activityHref);

                                driver.FindElement(By.XPath("//*[@title='Actions']")).Click();
                                driver.FindElement(By.XPath("//a[text()='Export GPX']")).Click();
                                Thread.Sleep(1500);

                                string fileName = driver.FindElement(By.XPath("//*[@id='heading']//div[@class='details']/h1")).Text;
                                string fileDate = driver.FindElement(By.XPath("//*[@id='heading']//div[@class='details']/time")).Text;
                                string movingTime = driver.FindElement(By.XPath("//*[@id='heading']/div/div/div[2]/ul[1]/li[2]/strong")).Text;

                                driver.SwitchTo().Window(driver.WindowHandles.First());

                                string newPath = SavingFiles.Save(downloadPath, fileName, fileDate, fileTime);
                                TxtEdit.Update(newPath, fileDate, fileTime, movingTime);
                                //GpxEdit.Update(newPath, fileDate, fileTime, movingTime);
                            }
                        }
                        numberOfActivities++;
                    }
                    numberOfWeeks++;
                } while (numberOfWeeks < listOfWeeks.Count);
                numberOfYears++;
            } while (numberOfYears != listOfYears.Count);
        }
    }
}
