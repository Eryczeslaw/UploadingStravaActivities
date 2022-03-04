using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using UploadingStravaActivities.Pages;
using UploadingStravaActivities.Pages.Athlete;
using UploadingStravaActivities.Pages.Upload;

namespace UploadingStravaActivities
{
    public class StravaTests
    {
        private IWebDriver driver;
        private readonly string email = "*****";
        private readonly string password = "*****";
        public static string downloadPath = $@"C:\Users\erykh\Downloads";
        public static int secondsToDownload = 20;
        public static int secondsToUpload = 30;
        public static double minumKM = 15;

        [SetUp]
        public void Setup()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", downloadPath);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            chromeOptions.AddArgument("start-maximized");
            
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(15);
        }

        [Test]
        public void Test1()
        {
            driver.Navigate().GoToUrl("http://strava.com/login");

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login(email, password);

            AthletePage athlete = new AthletePage(driver, "85532528");
            athlete.Navigate();
        }

        [Test]
        public void Test2()
        {
            driver.Navigate().GoToUrl("http://strava.com/login");

            LoginPage loginPage = new LoginPage(driver);
            loginPage.Login(email, password);

            IEnumerable<string> files = Directory.EnumerateFiles(downloadPath, "*.gpx");
            foreach (string file in files)
            {
                driver.Navigate().GoToUrl("https://www.strava.com/upload/select");
                UploadPage uploadPage = new UploadPage(driver);
                try
                {
                    uploadPage.Upload(file);
                }
                catch (TimeoutException)
                {

                }
            }
        }

        [TearDown]
        public void DriverQuit()
        {
            driver.Quit();
        }
    }
}