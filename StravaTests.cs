using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UploadingStravaActivities.Pages;

namespace UploadingStravaActivities
{
    public class StravaTests
    {
        private IWebDriver driver;
        private readonly string email = "*****";
        private readonly string password = "*****";
        private readonly string downloadPath = $@"C:\Users\erykh\Downloads";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Position = new System.Drawing.Point(30, 30);
            driver.Manage().Window.Size = new System.Drawing.Size(1800, 1000);

            driver.Manage().Timeouts().PageLoad = System.TimeSpan.FromSeconds(15);
        }

        [Test]
        public void Test1()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login(email, password);
            //AthletePage athlete = new AthletePage(driver, "85532528");
            //athlete.Navigate();
            Strava strava = new Strava(driver);
            strava.DownloadActivities("22887934");
        }

        [Test]
        public void Test2()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login(email, password);

            UploadPage uploadPage = new UploadPage(driver);
            uploadPage.Upload(downloadPath);
        }

        [Test]
        public void Test3()
        {
            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login(email, password);
            Strava strava = new Strava(driver);
            strava.DownloadActivitiesNewTab("85532528");
        }

        [TearDown]
        public void DriverQuit()
        {
            driver.Quit();
        }
    }
}