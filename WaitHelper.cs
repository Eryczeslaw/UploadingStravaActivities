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

        public void WaitUntilElementDisappear(string xPath, int seconds)
        {
            for (int j = 0; j < seconds * 50; j++)
            {
                try
                {
                    driver.FindElement(By.XPath(xPath));
                }
                catch (NoSuchElementException)
                {
                    return;
                }

                Thread.Sleep(20);
            }

            throw new TimeoutException();
        }
        public void WaitUntilElementLoad(string xPath, string text, int seconds)
        {
            for (int i = 0; i < seconds * 50; i++)
            {
                try
                {
                    if (driver.FindElement(By.XPath(xPath)).Text == text)
                    {
                        return;
                    }
                }
                catch (NoSuchElementException)
                {

                }

                Thread.Sleep(20);
            }

            throw new TimeoutException();
        }

        public void WaitUntilElementLoad(string xPath, int seconds)
        {
            for (int i = 0; i < seconds * 50; i++)
            {
                try
                {
                    driver.FindElement(By.XPath(xPath));
                    return;
                }
                catch (NoSuchElementException)
                {

                }

                Thread.Sleep(20);
            }

            throw new TimeoutException();
        }

        public bool WaitUntilOneOfThemLoad(string xPathFirst, string xPathSecond, int seconds)
        {
            for (int i = 0; i < seconds * 50; i++)
            {
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

                Thread.Sleep(20);
            }

            throw new TimeoutException();
        }
    }
}
