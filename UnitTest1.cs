using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UploadingStravaActivities
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Position = new System.Drawing.Point(30, 30);
            driver.Manage().Window.Size = new System.Drawing.Size(1800, 1000);

            //driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(5);
            driver.Manage().Timeouts().PageLoad = System.TimeSpan.FromSeconds(10);
        }

        [Test]
        public void Test1()
        {
            Strava.LogIn(driver, "fejk@buziaczek.pl", "!Fejk123");
            //Strava.DownloadActivities(driver, "https://www.strava.com/athletes/22887934");
            Strava.DownloadActivities(driver, "https://www.strava.com/athletes/85532528");
            //Strava.DownloadActivities(driver, "https://www.strava.com/athletes/94496430");
        }

        [Test]
        public void Test2()
        {
            Strava.LogIn(driver, "fejk@buziaczek.pl", "!Fejk123");
            Strava.UploadActivities(driver);
        }

        [Test]
        public void Test3()
        {

            Strava.DownloadActivitiesNewTab(driver, "https://google.com");
        }

        [TearDown]
        public void DriverQuit()
        {
            driver.Quit();
        }
    }
}