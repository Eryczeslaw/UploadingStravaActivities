using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using UploadingStravaActivities.FilesModification;

namespace UploadingStravaActivities
{
    public class Strava
    {
        private IWebDriver driver;
        private WaitHelper wait;
        private WebDriverWait webWait;
        private double minumKM = 15;

        public Strava(IWebDriver _driver)
        {
            driver = _driver;
            webWait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait = new WaitHelper(driver);
        }

        public void LogIn(string emailEntry, string passwordEntry)
        {
            driver.Navigate().GoToUrl("http://strava.com/login");

            IWebElement emailField = driver.FindElement(By.CssSelector("[name='email']"));
            emailField.SendKeys(emailEntry);

            IWebElement passwordField = driver.FindElement(By.CssSelector("[name='password']"));
            passwordField.SendKeys(passwordEntry);

            driver.FindElement(By.CssSelector("[id='login-button']")).Click();
        }

        public void DownloadActivities(string Url)
        {
            driver.Navigate().GoToUrl(Url);

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
                            webWait.Until(d => driver.FindElement(By.Id("interval-value")).Text == week);
                        }
                        catch (TimeoutException e)
                        {
                            listOfWeeks = driver.FindElements(By.XPath("//div[@id='interval-graph-columns']//a"));
                            listOfWeeks[listOfWeeks.Count - numberOfWeeks - 1].Click();
                            Thread.Sleep(1500);
                        }

                        IReadOnlyList<IWebElement> ride = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@title='Ride']"));
                        IReadOnlyList<IWebElement> map = driver.FindElements(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@class='Activity--entry-media--LkXKR']"));
                        if (ride.Count == 1 && map.Count == 1)
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
                                string newPath = SavingFiles.Save(fileName, fileDate, fileTime);
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

        public void UploadActivities()
        {
            WebClient webClient = new WebClient();
            IEnumerable<string> files = Directory.EnumerateFiles(@"C:\Users\erykh\Downloads", "*.gpx");

            foreach (string file in files)
            {
                driver.FindElement(By.XPath("//*[@id='container-nav']/ul[2]/li[4]/a")).Click();
                driver.FindElement(By.XPath("//*[@id='from-file-js']/a")).Click();
                driver.FindElement(By.XPath("//*[@id='uploadFile']/form/input[3]")).SendKeys(file);

                bool IsError = wait.WaitUntilOneOfThemLoad("//*[@id='uploadProgress']/ul/li[@class='error']", "//*[@id='uploadProgress']/ul/li[@class='finished']", 15);
                if (!IsError)
                {
                    string activityTitle = DataEdit.Title(file);
                    driver.FindElement(By.XPath("//*[@id='uploadProgress']//*[@name = 'name']")).Clear();
                    driver.FindElement(By.XPath("//*[@id='uploadProgress']//*[@name = 'name']")).SendKeys(activityTitle);
                    driver.FindElement(By.XPath("//*[@id='uploadProgress']//*[@class='selection']")).Click();
                    driver.FindElement(By.XPath("//*[@id='uploadProgress']//*[@data-value = 'Ride']")).Click();
                    driver.FindElement(By.XPath("//*[@id='uploadFooter']/button[1]")).Click();
                    Thread.Sleep(10000);
                }
            }
        }

        public void DownloadActivitiesNewTab(string Url)
        {
            driver.Navigate().GoToUrl(Url);
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
                }
                Thread.Sleep(1000);

                int numberOfWeeks = 0;
                do
                {
                    listOfWeeks = driver.FindElements(By.XPath("//div[@id='interval-graph-columns']//a"));
                    listOfWeeks[listOfWeeks.Count - numberOfWeeks - 1].Click();
                    Thread.Sleep(1000);

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

                                string newPath = SavingFiles.Save(fileName, fileDate, fileTime);
                                TxtEdit.Update(newPath, fileDate, fileTime, movingTime);
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
