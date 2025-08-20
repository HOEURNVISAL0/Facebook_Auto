using NUnit.Framework;
using FacebookLoginTests.Base;
using FacebookLoginTests.Pages;
using OpenQA.Selenium;
using System;

namespace FacebookLoginTests
{
    // The test class inherits from the base class to use its setup and teardown methods.
    [TestFixture]
    public class LoginTests : AppiumDriverBase
    {
        private LoginPage loginPage;

        // This method runs before each test to initialize the page object.
        [SetUp]
        public void TestSetup()
        {
            loginPage = new LoginPage(Driver);
        }

        [Test]
        public void TestInvalidLogin()
        {
            // The try-catch block is handled at the method level to provide more context for the specific error.
            // Act and Assert are combined for simplicity.

            // Arrange
            string invalidUsername = "johndoe@fakemail.com";
            string invalidPassword = "thisisnotapassword";

            // Act
            loginPage.Login(invalidUsername, invalidPassword);

            // Assert
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed(), "The error message was not displayed after an invalid login attempt.");
        }

        [Test]
        [Ignore("This test requires a valid username and password, which is not recommended for public code.")]
        public void TestValidLogin()
        {
            // The try-catch block is handled at the method level to provide more context for the specific error.
            // Act and Assert are combined for simplicity.

            // Arrange
            // This test is for educational purposes only, as automating a valid login is against Facebook's terms of service.
            string validUsername = "your_email@example.com";
            string validPassword = "your_password";

            // Act
            loginPage.Login(validUsername, validPassword);

            // Assert
            // You would add an assertion here to verify a successful login.
            // For example, checking for the presence of a unique element on the user's home feed.
            // Example:
            // Assert.IsTrue(Driver.FindElementByAccessibilityId("news_feed").Displayed, "Login was not successful.");
        }
    }
}
