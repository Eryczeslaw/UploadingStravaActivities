using OpenQA.Selenium;
using UploadingStravaActivities.FilesModification;

namespace UploadingStravaActivities.Pages.Upload
{
    class UploadProgressPage
    {
        private IWebElement title;
        private IWebElement activityType;
        private IWebElement rideType;
        private IWebElement saveButton;

        private IWebDriver driver;

        public UploadProgressPage(IWebDriver _driver)
        {
            driver = _driver;
            title = driver.FindElement(By.Name("name"));
            activityType = driver.FindElement(By.XPath("//*[@id='uploadProgress']//*[@class='selection']"));
            rideType = driver.FindElement(By.XPath("//*[@id='uploadProgress']//*[@data-value = 'Ride']"));
            saveButton = driver.FindElement(By.XPath("//*[@id='uploadFooter']/button[1]"));
        }

        public void Upload(string file)
        {
            string activityTitle = DataEdit.Title(file);
            title.Clear();
            title.SendKeys(activityTitle);

            if (activityType.Text != "Ride")
            {
                activityType.Click();
                rideType.Click();
            }

            saveButton.Click();

            UploadFinishPage uploadFinish = new UploadFinishPage(driver);
            uploadFinish.Wait(20);
        }
    }
}
