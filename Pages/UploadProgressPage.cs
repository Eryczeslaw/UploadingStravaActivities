using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using UploadingStravaActivities.FilesModification;

namespace UploadingStravaActivities.Pages
{
    class UploadProgressPage
    {
        [FindsBy(How = How.Name, Using = "name")]
        [CacheLookup]
        private IWebElement Title { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='uploadProgress']//*[@class='selection']")]
        [CacheLookup]
        private IWebElement ActivityType { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='uploadProgress']//*[@data-value = 'Ride']")]
        [CacheLookup]
        private IWebElement RideType { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='uploadFooter']/button[1]")]
        [CacheLookup]
        private IWebElement SaveButton { get; set; }

        private IWebDriver driver;

        public UploadProgressPage(IWebDriver _driver)
        {
            driver = _driver;
            PageFactory.InitElements(driver, this);
        }

        public void Upload(string file)
        {
            string activityTitle = DataEdit.Title(file);
            Title.Clear();
            Title.SendKeys(activityTitle);

            if (ActivityType.Text != "Ride")
            {
                ActivityType.Click();
                RideType.Click();
            }

            SaveButton.Click();

            UploadFinishPage uploadFinish = new UploadFinishPage(driver);
            uploadFinish.Wait(15);
        }
    }
}
