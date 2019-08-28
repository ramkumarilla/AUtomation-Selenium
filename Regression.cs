using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace automation_nunit
{
    public class Regression
    {
        private IWebDriver driver { get; set; }

        [Test]
        public void DoSearch()
        {
            String driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string driverFileExtensionName = "";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                driverPath = driverPath.Substring(0, driverPath.IndexOf("\\bin\\"));
                driverPath += "\\drivers\\";
                driverFileExtensionName = ".exe";
            }
            else
            {
                driverPath = driverPath.Substring(0, driverPath.IndexOf("/bin/"));
                driverPath += "/drivers/";
            }
            String driverExecutableFileName = "";
            if (getBrowser() == "Firefox")
            {
                driverExecutableFileName = "geckodriver";
                FirefoxOptions options = new FirefoxOptions();
                if (runHeadless())
                {
                    options.AddArgument("--headless");
                }
                FirefoxDriverService fxservice = FirefoxDriverService.CreateDefaultService(driverPath, driverExecutableFileName + driverFileExtensionName);
                driver = new FirefoxDriver(fxservice, options);
            }
            else
            {
                driverExecutableFileName = "chromedriver";
                ChromeOptions options = new ChromeOptions();
                if (runHeadless())
                {
                    options.AddArgument("--headless");
                    options.AddArgument("--window-size=1920x1080");
                    options.AddArgument("--disable-dev-shm-usage");
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--disable-gpu");
                }
                ChromeDriverService service = ChromeDriverService.CreateDefaultService(driverPath, driverExecutableFileName + driverFileExtensionName);
                driver = new ChromeDriver(service, options, TimeSpan.FromSeconds(60));
            }
            // open bing page with 'github' as search word
            driver.Url = "https://www.bing.com/search?q=github";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            // Find 'cite' element in result page which must have url for github.com
            Assert.AreEqual("https://github.com", driver.FindElement(By.TagName("cite")).Text);
            driver.Quit();
        }
        public bool runHeadless()
        {
            bool result = false;
            result = Convert.ToBoolean(TestContext.Parameters.Get("Headless", false));
            return result;
        }
        public string getBrowser()
        {
            string result = "Firefox";
            result = Convert.ToString(TestContext.Parameters.Get("Browser", ""));
            return result;
        }

    }
}
