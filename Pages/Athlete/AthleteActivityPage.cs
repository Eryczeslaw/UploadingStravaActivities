using OpenQA.Selenium;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using UploadingStravaActivities.FilesModification;

namespace UploadingStravaActivities.Pages.Athlete
{
    class AthleteActivityPage
    {
        private string xPathActionsButton;
        private string XPathExportGpx;
        private IWebElement title;
        private IWebElement date;
        private IWebElement movingTime;

        private IWebDriver driver;

        public AthleteActivityPage(IWebDriver _driver, string activityHref)
        {
            driver = _driver;
            driver.Navigate().GoToUrl(activityHref);

            xPathActionsButton = "//*[@title='Actions']";
            XPathExportGpx = "//a[text()='Export GPX']";
            title = driver.FindElement(By.XPath("//*[@id='heading']//div[@class='details']/h1"));
            date = driver.FindElement(By.XPath("//*[@id='heading']//div[@class='details']/time"));
            movingTime = driver.FindElement(By.XPath("//*[@id='heading']/div/div/div[2]/ul[1]/li[2]/strong"));
        }

        public void Download(string fileTime)
        {
            try
            {
                Wait(xPathActionsButton, 4);
                Wait(XPathExportGpx, 2);
            }
            catch (TimeoutException)
            {
                driver.SwitchTo().Window(driver.WindowHandles.First());
                return;
            }

            string fileName = title.Text;
            string fileDate = date.Text;
            string movingTime = this.movingTime.Text;

            try
            {
                string newPath = DownloadFiles.Download(StravaTests.downloadPath, fileName, fileDate, fileTime);

                //TxtEdit.Update(newPath, fileDate, fileTime, movingTime);
                GpxEdit.Update(newPath, fileDate, fileTime, movingTime);
            }
            catch (FileNotFoundException)
            {

            }
        }

        private void Wait(string xPath, int seconds)
        {
            for (int i = 0; i < seconds * 50; i++)
            {
                try
                {
                    IWebElement element = driver.FindElement(By.XPath(xPath));
                    element.Click();
                    return;
                }
                catch (NoSuchElementException)
                {

                }

                Thread.Sleep(20);
            }
        }
    }
}
