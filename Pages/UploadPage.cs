using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace UploadingStravaActivities.Pages
{
    class UploadPage
    {
        [FindsBy(How = How.XPath, Using = "//*[@id='uploadFile']/form/input[3]")]
        [CacheLookup]
        private IWebElement SelectFiles { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='uploadProgress']/ul/li[@class='error']")]
        [CacheLookup]
        private IWebElement ErrorMessage { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='uploadProgress']/ul/li[@class='finished']")]
        [CacheLookup]
        private IWebElement ProgressPage { get; set; }

        private IWebDriver driver;
        private IEnumerable<string> files;

        public UploadPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public void Upload(string downloadPath)
        {
            files = Directory.EnumerateFiles(downloadPath, "*.gpx");

            foreach (string file in files)
            {
                driver.Navigate().GoToUrl("https://www.strava.com/upload/select");
                PageFactory.InitElements(driver, this);
                SelectFiles.SendKeys(file);

                if (WhichOneFirst())
                {
                    UploadProgressPage progressPage = new UploadProgressPage(driver);
                    progressPage.Upload(file);
                }
            }
        }

        private bool WhichOneFirst()
        {
            while (true)
            {
                PageFactory.InitElements(driver, this);

                try
                {
                    if (ProgressPage.Displayed)
                    {
                        return true;
                    }
                }
                catch (NoSuchElementException)
                {

                }

                try
                {
                    if (ErrorMessage.Displayed)
                    {
                        return false;
                    }
                }
                catch (NoSuchElementException)
                {

                }

                Thread.Sleep(20);
            }
        }
    }
}
