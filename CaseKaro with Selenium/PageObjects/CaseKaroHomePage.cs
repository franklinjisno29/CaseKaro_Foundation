using CaseKaro_with_Selenium.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V117.DOM;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseKaro_with_Selenium.PageObjects     //POM
{
    internal class CaseKaroHomePage
    {
        IWebDriver driver;

        public CaseKaroHomePage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));//if the driver is null exception thrown
            PageFactory.InitElements(driver, this);//for optimizing the code we write this inside the constructor
        }

        //Arrange
        [FindsBy(How = How.XPath, Using = "//button[contains(@class,'search-toggle')]")]
        private IWebElement? SearchButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='q']")]
        private IWebElement? SearchBox { get; set; }

        //Act
        public SearchResultsPage ClickSearchButton(string searchtext)
        {
            if (searchtext == null)
                throw new NoSuchElementException(nameof(searchtext));
            SearchButton?.Click();
            IWebElement pageLoadedElement = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@class,'search-bar')]")));
            SearchBox?.SendKeys(searchtext);
            SearchBox?.SendKeys(Keys.Enter);
            IWebElement pageLoadedElement1 = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@class,'search-toggle')]")));
            return new SearchResultsPage(driver);
        }
    }
}
