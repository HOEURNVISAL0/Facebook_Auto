using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using System;
using System.IO;

namespace FacebookLoginTests.Base
{
    public abstract class AppiumDriverBase
    {
        // Appium local service
        protected AppiumLocalService AppiumService;

        // Android driver instance
        protected AndroidDriver Driver;

        // Method to set up the Appium driver before each test
        [SetUp]
        public void Setup()
        {
            try
            {
                // Start the Appium server programmatically
                AppiumService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
                if (!AppiumService.IsRunning)
                {
                    AppiumService.Start();
                }

                // Define the Appium capabilities for Android
                var appiumOptions = new AppiumOptions();
                appiumOptions.PlatformName = "Android";
                appiumOptions.DeviceName = "sdk_gphone64_x86_64"; // Example: A common emulator name from Android Studio
                appiumOptions.AutomationName = "UiAutomator2";
                // The following line is removed as the app is already installed on the emulator.
                // appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.App, "C:\\Users\\JohnDoe\\Downloads\\facebook-debug-v1.apk");
                appiumOptions.AddAdditionalAppiumOption("appPackage", "com.facebook.katana");
                appiumOptions.AddAdditionalAppiumOption("appActivity", "com.facebook.katana.LoginActivity"); // The launch activity of the Facebook app

                // Initialize the Android driver and set an implicit wait
                Driver = new AndroidDriver(AppiumService, appiumOptions);
                Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during Appium setup: {ex.Message}");
                throw; // Re-throw the exception to fail the test setup
            }
        }

        // Method to tear down the Appium driver after each test
        [TearDown]
        public void Teardown()
        {
            if (Driver != null)
            {
                Driver.Quit();
                // Stop the Appium server
                AppiumService.Dispose();
            }
        }
    }
}