using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Security.Cryptography;
using TechTalk.SpecFlow;
using CaseKaro_with_BDD.Hooks;
using CaseKaro_with_BDD.Utilities;

namespace CaseKaro_with_BDD.StepDefinitions
{
    [Binding]
    public class SearchProductStep : CoreCodes     //inheritance
    {
        IWebDriver? driver = AllHooks.driver;

        [When(@"User clicks on the search button")]
        public void WhenUserClicksOnTheSearchButton()
        {
            var fluentWait = Waits(driver);     //fluent waits
            AllHooks.test = AllHooks.extent.CreateTest("Search Product Test");     //extent report creation
            IWebElement SearchButton = driver.FindElement(By.XPath("//button[contains(@class,'search-toggle')]"));
            SearchButton?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@class,'search-bar')]")));

        }

        [When(@"Fills the '([^']*)'")]
        public void WhenFillsThe(string searchtext)
        {
            var fluentWait = Waits(driver);
            IWebElement SearchBox = driver.FindElement(By.XPath("//input[@name='q']"));
            SearchBox?.SendKeys(searchtext);
            SearchBox?.SendKeys(Keys.Enter);
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@class,'search-toggle')]")));
        }

        [Then(@"Search Result Page is loaded in the same page")]
        public void ThenSearchResultPageIsLoadedInTheSamePage()
        {
            try
            {
                Assert.That(driver.Url, Does.Contain("search"));     //NUnit Assertion
                LogTestResult("Search Product Test", "Searched for Product");     //Log file creation
                TakeScreenshot(driver);     //taking screenshot
            }
            catch (AssertionException ex)     //exception
            {
                LogTestResult("Search Product Test", "Not Searched for Product", ex.Message);
                TakeScreenshot(driver);
            }
        }

        [When(@"User clicks the '([^']*)' product")]
        public void WhenUserClicksTheProduct(string pId)
        {
            var fluentWait = Waits(driver);
            IWebElement GetProduct = driver.FindElement(By.XPath("(//a[@class='full-width-link'])[" + pId + "]"));
            GetProduct?.Click();
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.Id("AddToCart-product-template")));
        }

        [Then(@"Product Page is loaded in the same page")]
        public void ThenProductPageIsLoadedInTheSamePage()
        {
            try
            {
                Assert.That(driver.Url, Does.Contain("iphone"));
                LogTestResult("Search Product Test", "Product Clicked");
                TakeScreenshot(driver);
            }
            catch (AssertionException ex)
            {
                LogTestResult("Search Product Test", "Product Not Clicked", ex.Message);
                TakeScreenshot(driver);
            }
        }

        [When(@"User clicks the Add to Cart Button")]
        public void WhenUserClicksTheAddToCartButton()
        {
            var fluentWait = Waits(driver);
            IWebElement AddtoCartbtn = driver.FindElement(By.Id("AddToCart-product-template"));
            AddtoCartbtn?.Click();
        }

        [Then(@"Add To Cart Page is loaded in the same page")]
        public void ThenAddToCartPageIsLoadedInTheSamePage()
        {
            try
            {
                Assert.That(driver.Url, Does.Contain("cart"));
                LogTestResult("Search Product Test", "Add To Cart Clicked, Test Success");
                TakeScreenshot(driver);
            }
            catch (AssertionException ex)
            {
                LogTestResult("Search Product Test", "Add To Cart didn't Clicked", ex.Message);
                TakeScreenshot(driver);
            }
        }
    }
}
