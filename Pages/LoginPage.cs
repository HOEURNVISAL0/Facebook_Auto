using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace FacebookLoginTests.Pages
{
    public class LoginPage
    {
        private readonly AndroidDriver driver;
        private readonly WebDriverWait wait;

        // Constructor to initialize the driver and wait object
        public LoginPage(AndroidDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        // --- Page Methods ---

        // Method to get an element with error handling
        private IWebElement GetElement(By by, string elementName)
        {
            try
            {
                return wait.Until(d => d.FindElement(by));
            }
            catch (WebDriverTimeoutException)
            {
                throw new NoSuchElementException($"Element '{elementName}' with locator '{by}' was not found within the timeout period.");
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred while finding element '{elementName}': {ex.Message}", ex);
            }
        }

        // Method to enter the email or phone number
        public void EnterUsername(string username)
        {
            // Updated locator using the provided XPath
            var emailField = GetElement(MobileBy.XPath("//android.widget.EditText[@content-desc='Mobile number or email,']"), "Email Field");
            emailField.SendKeys(username);
        }

        // Method to enter the password
        public void EnterPassword(string password)
        {
            // Updated locator using the provided XPath
            var passwordField = GetElement(MobileBy.XPath("//android.widget.EditText[@content-desc='Password,']"), "Password Field");
            passwordField.SendKeys(password);
        }

        // Method to click the login button
        public void ClickLoginButton()
        {
            // Updated locator using the provided XPath
            var loginButton = GetElement(MobileBy.XPath("//android.widget.Button[@content-desc='Log in']/android.view.ViewGroup"), "Login Button");
            loginButton.Click();
        }

        public void ClickContinueButton()
        {
            // Updated locator using the provided XPath
            var loginButton = GetElement(MobileBy.XPath("//android.view.View[@content-desc=\"Continue\"]"), "Continue");
            loginButton.Click();
        }

        // Method to perform the full login action
        public void Login(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLoginButton();
            ClickContinueButton();
        }

        // Method to check if the error message is displayed
        public bool IsErrorMessageDisplayed()
        {
            try
            {
                var errorMessage = GetElement(MobileBy.Id("com.facebook.katana:id/login_error_message"), "Error Message");
                return errorMessage.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}