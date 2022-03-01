using OpenQA.Selenium;
using System.Threading;

namespace UploadingStravaActivities.Pages.Upload
{
    class UploadFinishPage
    {
        private IWebDriver driver;

        public UploadFinishPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public void Wait(int seconds)
        {
            for (int i = 0; i < seconds * 50; i++)
            {
                try
                {
                    driver.FindElement(By.Id("heading"));
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
