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
            Strava strava = new Strava(driver);
            strava.LogIn("fejk@buziaczek.pl", "!Fejk123");
            //strava.DownloadActivities("https://www.strava.com/athletes/22887934");
            strava.DownloadActivities("https://www.strava.com/athletes/85532528");
            //strava.DownloadActivities("https://www.strava.com/athletes/94496430");
        }

        [Test]
        public void Test2()
        {
            Strava strava = new Strava(driver);
            strava.LogIn("fejk@buziaczek.pl", "!Fejk123");
            strava.UploadActivities();
        }

        [Test]
        public void Test3()
        {
            Strava strava = new Strava(driver);
            strava.DownloadActivitiesNewTab("https://google.com");
        }

        [TearDown]
        public void DriverQuit()
        {
            driver.Quit();
        }
    }
}