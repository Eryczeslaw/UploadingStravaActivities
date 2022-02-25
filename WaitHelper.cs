using OpenQA.Selenium;
using System;
using System.Threading;

namespace UploadingStravaActivities
{
    public class WaitHelper
    {
        private IWebDriver driver;

        public WaitHelper(IWebDriver _driver)
        {
            driver = _driver;
        }

        public void WaitUntilElementLoad(string xPath, int time)
        {
            for (int i = 0; i < time * 20; i++)
            {
                Thread.Sleep(50);
                try
                {
                    driver.FindElement(By.XPath(xPath));
                    return;
                }
                catch(NoSuchElementException)
                {

                }
            }

            throw new TimeoutException();
        }

        public bool WaitUntilOneOfThemLoad(string xPathFirst, string xPathSecond, int time)
        {
            for (int i = 0; i < time * 20; i++)
            {
                Thread.Sleep(50);
                try
                {
                    driver.FindElement(By.XPath(xPathFirst));
                    return true;
                }
                catch (NoSuchElementException)
                {
                    
                }

                try
                {
                    driver.FindElement(By.XPath(xPathSecond));
                    return false;
                }
                catch (NoSuchElementException)
                {

                }
            }

            throw new TimeoutException();
        }
    }
}
