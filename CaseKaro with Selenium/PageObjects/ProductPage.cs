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
    internal class ProductPage
    {
        IWebDriver driver;
        public ProductPage(IWebDriver? driver)
        {
            this.driver = driver ?? throw new ArgumentException(nameof(driver)); ;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "AddToCart-product-template")]
        private IWebElement? AddToCartButton { get; set; }

        public void ClickAddToCartBtn()
        {
            AddToCartButton?.Click();
        }
    }
}
