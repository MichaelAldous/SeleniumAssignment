using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Threading;
using System.Linq;

namespace UnitTestAssignment2
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void CalcCosine()
        {
            // Testing 10Cos45 = 7.071
            // Note, in this calculator, it's entered as 45cos * 10 = 7.071
            double expected = 7.071;
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.calculator.net");
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[2]/span[1]")).Click();// 4
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[2]/span[2]")).Click();// 5
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[1]/div/div[1]/span[2]")).Click();// Cos
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[3]/span[4]")).Click();// x
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[3]/span[1]")).Click();// 1
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[4]/span[1]")).Click();// 0
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[5]/span[4]")).Click();// =
            double result = double.Parse(driver.FindElement(By.XPath("//*[@id='sciOutPut']")).Text);
            Assert.AreEqual(result, expected, 0.001, "Incorrect Result when performing 45Cos * 10");
            return;
        }// End of CalcCosine
        
        
        [TestMethod]
        public void CalcLog()
        {
            // Testing Log10 = 1
            // Note, in this calculator, it's entered as 10 log
            double expected = 1;
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.calculator.net");
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[3]/span[1]")).Click();// 1
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[4]/span[1]")).Click();// 0
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[1]/div/div[4]/span[5]")).Click();// log
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[5]/span[4]")).Click();// =
            double result = double.Parse(driver.FindElement(By.XPath("//*[@id='sciOutPut']")).Text);
            Assert.AreEqual(result, expected, 0.001, "Incorrect Result when performing log10");
        }//End of CalcLog
        
        
        [TestMethod]
        public void CalcXY()
        {
            // Testing 10^5 = 100,000
            double expected = 100000;
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.calculator.net");
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[3]/span[1]")).Click();// 1
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[4]/span[1]")).Click();// 0
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[1]/div/div[3]/span[1]")).Click();// xy
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[2]/span[2]")).Click();// 5
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[5]/span[4]")).Click();// =
            double result = double.Parse(driver.FindElement(By.XPath("//*[@id='sciOutPut']")).Text);
            Assert.AreEqual(result, expected, 0, "Incorrect Result when performing 10^5");
        }//End of CalcXY
        
        
        [TestMethod]
        public void CalcNFactorial()
        {
            // Testing 5! = 120
            double expected = 120;
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://www.calculator.net");
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[2]/div/div[2]/span[2]")).Click();// 5
            driver.FindElement(By.XPath("//*[@id='sciout']/tbody/tr[2]/td[1]/div/div[5]/span[5]")).Click();// !
            double result = double.Parse(driver.FindElement(By.XPath("//*[@id='sciOutPut']")).Text);
            Assert.AreEqual(result, expected, 0, "Incorrect Result when performing 5!");
        }//End of CalcNFactorial
        

        [TestMethod]
        public void TestPartB() // Validates order price
        {
            // 16.51 + 50.99 + 16.40 + $2 shipping
            double expected = 16.51 + 50.99 + 16.40 + 2; // Totals = 85.9

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://automationpractice.com/index.php");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            // Orders 3 items
            int counter = 1;
            for (int i=0; i < 3; i++)
            {
                //Selects invisible box to bring up add to cart button
                driver.FindElement(By.XPath("//*[@id='homefeatured']/li["+ counter +"]/div/div[2]")).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='homefeatured']/li[" + counter + "]/ div/div[2]/div[2]/a[1]")));
                //Clicks add to cart
                driver.FindElement(By.XPath("//*[@id='homefeatured']/li[" + counter + "]/ div/div[2]/div[2]/a[1]")).Click();
                //Waits for cross to be found
                wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("cross")));
                //Closes pop up window
                driver.FindElement(By.XPath("//*[@id='layer_cart']/div[1]/div[2]/div[4]/span")).Click();
                counter = counter + 3;
            }

            // Goes to checkout page and gets total value and compares it with value that it should be
            driver.FindElement(By.XPath("//*[@id='layer_cart']/div[1]/div[2]/div[4]/a")).Click();
            string resultTotal = driver.FindElement(By.XPath("//*[@id='total_price']")).Text;
            double result = double.Parse(resultTotal.Remove(0, 1));
            Assert.AreEqual(result, expected, 0, "Incorrect Order Total");

        }//End of TestPartB
        

        [TestMethod]
        public void TestPartC() //Validates avaliablity of links
        {
            using (TextWriter tw = new StreamWriter("E:/Wintec/Software Testing/Selenium Assignment/linkOfLinks.txt"))
            {
                int totalLinks = 0;
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("http://automationpractice.com/index.php");

                tw.WriteLine("Format is Link Text : Link Title: Link URL");
                foreach (IWebElement link in driver.FindElements(By.TagName("a")))
                {
                    totalLinks++;
                    tw.WriteLine(link.Text + ": " + link.GetAttribute("title") + ": " + link.GetAttribute("href"));
                    Console.WriteLine(link.Text + ": " + link.GetAttribute("title") + ": " + link.GetAttribute("href"));
                }
                tw.WriteLine("---------- Total Links ----------");
                tw.WriteLine("Total number of links: " + totalLinks);
            }
        }//End of TestPartC
        

        [TestMethod]
        public void TestPartD() //Verifies all region and district values
        {
            using (TextReader tr = new StreamReader("E:/Wintec/Software Testing/Selenium Assignment/part4_BaseResults.txt"))
            {
                bool isCorrrect = true;

                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://www.realestate.co.nz/");
                
                //Navigate to classic version of website
                driver.FindElement(By.XPath("//*[@id='ember287']/header/div/div[2]/div/nav/div[4]/div[2]")).Click();
                driver.FindElement(By.XPath("//*[@id='ember287']/header/div/div[2]/div/nav/div[4]/div[2]/ul/li[3]/a")).Click();

                IWebElement regionElement = driver.FindElement(By.Id("search_filters_regions"));
                SelectElement region = new SelectElement(regionElement);

                //Cycle through regions
                foreach (IWebElement selectionRegion in region.Options)
                {
                    region.SelectByText("All regions");
                    if (selectionRegion.Text.Equals("All Regions")) continue;
                    region.SelectByText(selectionRegion.Text);

                    //If region does not match base list then returns false value and Assert fails
                    if (tr.ReadLine() != selectionRegion.Text)
                    {
                        isCorrrect = false;
                    }

                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
                    wait.Until(ExpectedConditions.ElementToBeClickable(driver.FindElement(By.Id("search_filters_districts"))));

                    IWebElement districtElement = driver.FindElement(By.Id("search_filters_districts"));
                    SelectElement district = new SelectElement(districtElement);
                    //Cycle through district 
                    foreach (IWebElement selectionDistrict in district.Options)
                    {
                        if (selectionDistrict.Text.Equals("All Districts")) continue;

                        //If district does not match base list then returns false value and Assert fails
                        if (tr.ReadLine() != ("-- " + selectionDistrict.Text))
                        {
                            isCorrrect = false;
                        }
                    }//End of scanning districts
                }//End of scanning regions
                Assert.IsTrue(isCorrrect, "Lists do no match base results");
            }
        }//End of TestPartD



    }
}
