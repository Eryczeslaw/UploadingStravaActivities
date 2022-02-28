using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using UploadingStravaActivities.FilesModification;

namespace UploadingStravaActivities.Pages
{
    class AthleteActivityPage
    {
        [FindsBy(How = How.XPath, Using = "//*[@title='Actions']")]
        [CacheLookup]
        private IWebElement ActionsButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[text()='Export GPX']")]
        [CacheLookup]
        private IWebElement ExportGpx { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='heading']//div[@class='details']/h1")]
        [CacheLookup]
        private IWebElement Title { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='heading']//div[@class='details']/time")]
        [CacheLookup]
        private IWebElement Date { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='heading']/div/div/div[2]/ul[1]/li[2]/strong")]
        [CacheLookup]
        private IWebElement MovingTime { get; set; }

        private IWebDriver driver;
        private string downloadPath = $@"C:\Users\erykh\Downloads";

        public AthleteActivityPage(IWebDriver _driver)
        {
            driver = _driver;
            PageFactory.InitElements(driver, this);
        }

        public void Download(string fileTime)
        {
            try
            {
                Wait(ActionsButton, 5);
                Wait(ExportGpx, 5);
            }
            catch (TimeoutException)
            {
                driver.SwitchTo().Window(driver.WindowHandles.First());
                return;
            }

            string fileName = Title.Text;
            string fileDate = Date.Text;
            string movingTime = MovingTime.Text;

            try
            {
                string newPath = DownloadFiles.Download(downloadPath, fileName, fileDate, fileTime, 15);
                //TxtEdit.Update(newPath, fileDate, fileTime, movingTime);
                GpxEdit.Update(newPath, fileDate, fileTime, movingTime);
            }
            catch (FileNotFoundException)
            {

            }
        }

        private void Wait(IWebElement element, int seconds)
        {
            for (int i = 0; i < seconds * 50; i++)
            {
                try
                {
                    if (element.Displayed)
                    {
                        element.Click();
                        return;
                    }
                }
                catch (NoSuchElementException)
                {

                }

                Thread.Sleep(20);
                PageFactory.InitElements(driver, this);
            }
        }
    }
}
