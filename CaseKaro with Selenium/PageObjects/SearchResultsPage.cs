using CaseKaro_with_Selenium.PageObjects;
using CaseKaro_with_Selenium.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaseKaro_with_Selenium.PageObjects
{ 
    internal class SearchResultsPage
    {
        IWebDriver driver;
        public SearchResultsPage(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver));
            PageFactory.InitElements(driver, this);
        }

        //Act
        public IWebElement GetProductSelect(string pId)
        {
            return driver.FindElement(By.XPath("(//a[@class='full-width-link'])[" + pId + "]"));
        }

        public ProductPage ClickProduct(string pId)
        {
            GetProductSelect(pId)?.Click();
            IWebElement pageLoadedElement1 = CoreCodes.Waits(driver).Until(ExpectedConditions.ElementIsVisible(By.Id("AddToCart-product-template")));
            return new ProductPage(driver);
        }
    }
}
