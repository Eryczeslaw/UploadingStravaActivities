using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Threading;

namespace UploadingStravaActivities.Pages
{
    class UploadFinishPage
    {
        [FindsBy(How = How.Id, Using = "heading")]
        [CacheLookup]
        private IWebElement ActivityHeading { get; set; }

        private IWebDriver driver;

        public UploadFinishPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public void Wait(int seconds)
        {
            PageFactory.InitElements(driver, this);

            for (int i = 0; i < seconds * 50; i++)
            {
                PageFactory.InitElements(driver, this);
                try
                {
                    if (ActivityHeading.Displayed)
                    {
                        return;
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
