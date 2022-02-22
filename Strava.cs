using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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
        private WebDriverWait wait;

        public Strava(IWebDriver _driver)
        {
            driver = _driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
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
                            string movingTime = driver.FindElement(By.XPath("//*[@id='heading']/div/div/div[2]/ul[1]/li[2]/strong")).Text;

                            driver.Navigate().Back();
                            string newPath = SavingFiles.Save(fileName, fileDate, fileTime);
                            TxtEdit.Update(newPath, fileDate, fileTime, movingTime);

                            //wait.Until(d => d.FindElement(By.XPath($"//*[@id='interval-rides']/div/div[{numberOfActivities + 1}]//*[@class='EntryHeader--media-body--bMdyL']/div/time[text() ='{fileTime}']")).Displayed);
                            Thread.Sleep(1500);
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
            driver.FindElement(By.XPath("//*[@id='container-nav']/ul[2]/li[4]/a")).Click();
            driver.FindElement(By.XPath("//*[@id='from-file-js']/a")).Click();
            //driver.FindElement(By.XPath("//*[@id='uploadFile']//*[@class='files']")).Click();
        }

        public void DownloadActivitiesNewTab(string Url)
        {
            LogIn("fejk@buziaczek.pl", "!Fejk123");

            ((IJavaScriptExecutor)driver).ExecuteScript("window.open();");

            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.Navigate().GoToUrl("https://www.strava.com/activities/6716663610");

            driver.SwitchTo().Window(driver.WindowHandles.First());
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }
    }
}
