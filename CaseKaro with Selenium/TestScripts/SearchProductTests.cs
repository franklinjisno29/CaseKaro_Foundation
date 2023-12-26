using CaseKaro_with_Selenium.PageObjects;
using CaseKaro_with_Selenium.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaseKaro_with_Selenium.Helpers;

namespace CaseKaro_with_Selenium.TestScripts
{
    internal class SearchProductTests : CoreCodes     //inheritance
    {
        [Test, Category("end-to-end Test")]
        [TestCase("1")]     //parameterization
        public void SearchProductTest(string pId)
        {
            var fluentWait = Waits(driver);
            string? currDir = Directory.GetParent(@"../../../").FullName;
            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";     //log file creation
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            test = extent.CreateTest("Search Product Test");     //extent report creation
            CaseKaroHomePage casekarohp = new CaseKaroHomePage(driver);
            Log.Information("Search Product Test Started");
            test.Info("Search Product Test Started");
            IWebElement pageLoadedElement = fluentWait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[contains(@class,'search-toggle')]")));     //fluent waits
            string? excelFilePath = currDir + "/TestData/InputData.xlsx";       //Excel Implementation
            string? sheetName = "SearchTerm";
            List<SearchData> searchDataList = ExcelUtils.ReadSearchData(excelFilePath, sheetName);
            foreach (var searchData in searchDataList)
            {
                try
                {
                    string? searchtext = searchData?.SearchText;
                    var searchresultpage = fluentWait.Until(d => casekarohp.ClickSearchButton(searchtext));
                    Log.Information("Searched for Product");
                    test.Info("Searched for Product");
                    var productpage = fluentWait.Until(d => searchresultpage.ClickProduct(pId));
                    Log.Information("Product Clicked");
                    test.Info("Product Clicked");
                    productpage.ClickAddToCartBtn();
                    Log.Information("Add To Cart Clicked");
                    test.Info("Add To Cart Clicked");
                    Assert.That(driver.Url, Does.Contain("cart"));     //NUnit Assertion
                    TakeScreenshot();     //taking screenshot
                    LogTestResult("Search Product Test", "Search Product Test Success");
                }
                catch (AssertionException ex)     //exception
                {
                    TakeScreenshot();
                    LogTestResult("Search Product Test", "Search Product Test Failed", ex.Message);
                }
            }
        }
    }
}
