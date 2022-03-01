using OpenQA.Selenium;
using System;
using System.Threading;

namespace UploadingStravaActivities.Pages.Upload
{
    class UploadPage
    {
        private IWebElement selectFiles;
        private IWebDriver driver;

        public UploadPage(IWebDriver _driver)
        {
            driver = _driver;
            selectFiles = driver.FindElement(By.XPath("//*[@id='uploadFile']/form/input[3]"));
        }

        public void Upload(string filePath)
        {
            selectFiles.SendKeys(filePath);

            if (WhichOneFirst(StravaTests.secondsToUpload))
            {
                UploadProgressPage progressPage = new UploadProgressPage(driver);
                progressPage.Upload(filePath);
            }
        }

        private bool WhichOneFirst(int seconds)
        {
            for (int i = 0; i < seconds * 50; i++)
            {
                try
                {
                    driver.FindElement(By.XPath("//*[@id='uploadProgress']/ul/li[@class='error']"));
                    return false;
                }
                catch (NoSuchElementException)
                {

                }

                try
                {
                    driver.FindElement(By.XPath("//*[@id='uploadProgress']/ul/li[@class='finished']"));
                    return true;
                }
                catch (NoSuchElementException)
                {

                }

                Thread.Sleep(20);
            }

            throw new TimeoutException();
        }
    }
}
