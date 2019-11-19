using OpenQA.Selenium;
//using OpenQA.Selenium.Support.UI;
using PTCheckInTests.CommonTools;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using System.Threading;
using System.Net.Http;
using System.Threading.Tasks;

namespace PTCheckInTests.Steps
{
    [Binding]
    public class EmployeeSideSteps
    {
        static readonly HttpClient _httpClient = new HttpClient();

        private readonly WebDriver _webDriver;

        public EmployeeSideSteps()
        {
            _webDriver = new WebDriver();
        }

        [Given(@"我前往登入畫面")]
        public void Given我前往登入畫面()
        {
            IWebDriver webDriver = _webDriver.Current;
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(webDriver.Url + "login.aspx");
        }
        
        [Given(@"輸入帳號 (.*)")]
        public void Given輸入帳號(string userName)
        {
            var element = _webDriver.Wait.Until(d => d.FindElement(By.Id("username")));
            element.SendKeys(userName);
        }
        
        [Given(@"輸入密碼 (.*)")]
        public void Given輸入密碼(string password)
        {
            var element = _webDriver.Wait.Until(d => d.FindElement(By.Id("password")));
            element.SendKeys(password);
        }
        
        [When(@"我按下登入按鈕")]
        public void When我按下登入按鈕()
        {
            var element = _webDriver.Wait.Until(d => d.FindElement(By.XPath("//button[contains(.,'登入')]")));
            element.Click();
            
        }
        
        [Then(@"首頁的用戶名稱應該為(.*)")]
        public void Then首頁的用戶名稱應該為(string username)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("userName")));
            Assert.AreEqual(username, element.Text);
        }

        [Given(@"我按下登入按鈕")]
        public void Given我按下登入按鈕()
        {
            var element = _webDriver.Wait.Until(d => d.FindElement(By.XPath("//button[contains(.,'登入')]")));
            element.Click();
        }

        [Given(@"我點選快速簽到按鈕")]
        public void Given我點選快速簽到按鈕()
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("fastCheckIn")));
            element.Click();
        }

        [Given(@"我點選快速簽退按鈕")]
        public void Given我點選快速簽退按鈕()
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("fastCheckOut")));
            element.Click();
        }


        [Then(@"應該出現(.*)視窗")]
        public void Then應該出現視窗(string windowText)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(string.Format("//h2[contains(.,'{0}')]", windowText))));
            Assert.AreEqual(windowText, element.Text);
        }

        [Then(@"首頁時鐘的簽到時間應該為現在的伺服器時間")]
        public async Task Then首頁時鐘的簽到時間應該為現在的伺服器時間()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://assistant.gss.com.tw/PTCheckinFront/login.aspx");
            //Server時間
            DateTime now = DateTime.Parse(response.Headers.GetValues("Date").First());
            string hour = now.Hour < 10 ? "0" + now.Hour.ToString() : now.Hour.ToString();
            string minute = now.Minute < 10 ? "0" + now.Minute.ToString() : now.Minute.ToString();
            string assertion = string.Format("{0}:{1}", hour, minute);
            var element = _webDriver.Wait.Until(d => d.FindElement(By.Id("CheckInRecord")));
            string text = element.Text;
            string startTime = text.Substring(0, text.IndexOf('~'));
            Assert.AreEqual(assertion, startTime);
        }

        [Then(@"首頁時鐘的簽退時間應該為現在的伺服器時間")]
        public async Task Then首頁時鐘的簽退時間應該為現在的伺服器時間()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://assistant.gss.com.tw/PTCheckinFront/login.aspx");
            //Server時間
            DateTime now = DateTime.Parse(response.Headers.GetValues("Date").First());
            string hour = now.Hour < 10 ? "0" + now.Hour.ToString() : now.Hour.ToString();
            string minute = now.Minute < 10 ? "0" + now.Minute.ToString() : now.Minute.ToString();
            string assertion = string.Format("{0}:{1}", hour, minute);
            var element = _webDriver.Wait.Until(d => d.FindElement(By.Id("CheckInRecord")));
            string text = element.Text;
            string endTime = text.Substring(text.IndexOf("~") + 1);
            Assert.AreEqual(assertion, endTime);
        }



        [Then(@"簽到時間應該為現在的伺服器時間")]
        public async Task Then簽到時間應該為現在的伺服器時間()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://assistant.gss.com.tw/PTCheckinFront/login.aspx");
            //Server時間
            DateTime now = DateTime.Parse(response.Headers.GetValues("Date").First());
            string hour = now.Hour < 10 ? "0" + now.Hour.ToString() : now.Hour.ToString();
            string minute = now.Minute < 10 ? "0" + now.Minute.ToString() : now.Minute.ToString();
            string assertion = string.Format("{0}:{1}",hour, minute);
            DateTime today = DateTime.Today;
            string id = string.Format("{0}{1}{2}-start-time", today.Year, today.Month, today.Day);
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            Assert.AreEqual(assertion, element.GetAttribute("value"));
        }

        [Then(@"簽退時間應該為現在的伺服器時間")]
        public async Task Then簽退時間應該為現在的伺服器時間()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://assistant.gss.com.tw/PTCheckinFront/login.aspx");
            //Server時間
            DateTime now = DateTime.Parse(response.Headers.GetValues("Date").First());
            string hour = now.Hour < 10 ? "0" + now.Hour.ToString() : now.Hour.ToString();
            string minute = now.Minute < 10 ? "0" + now.Minute.ToString() : now.Minute.ToString();
            string assertion = string.Format("{0}:{1}", hour, minute);
            DateTime today = DateTime.Today;
            string id = string.Format("{0}{1}{2}-end-time", today.Year, today.Month, today.Day);
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            Assert.AreEqual(assertion, element.GetAttribute("value"));
        }

        [Given(@"我按下名為(.*)的連結")]
        public void Given我按下名為的連結(string linkText)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(linkText)));
            _webDriver.Current.Navigate().Refresh();
            _webDriver.Wait.Until(ExpectedConditions.StalenessOf(element));
            element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(linkText)));
            element.Click();
        }

        [When(@"我按下名為(.*)的連結")]
        public void When我按下名為的連結(string linkText)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(linkText)));
            _webDriver.Current.Navigate().Refresh();
            _webDriver.Wait.Until(ExpectedConditions.StalenessOf(element));
            element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText(linkText)));
            element.Click();
        }

        [Then(@"中文姓名應該為 (.*)")]
        public void Then中文姓名應該為(string cName)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("cName")));
            //Assert.AreEqual(cName, element.GetAttribute("value"));
            Assert.Fail();
        }

        [Then(@"員工編號應該為 (.*)")]
        public void Then員工編號應該為(string empId)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("empId")));
            //Assert.AreEqual(empId, element.GetAttribute("value"));
            Assert.Fail();
        }

        [Then(@"英文姓名應該為 (.*)")]
        public void Then英文姓名應該為(string eName)
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("eName")));
            //Assert.AreEqual(eName, element.GetAttribute("value"));
            Assert.Fail();
        }


        [Given(@"我按下馬上簽到GO按鈕")]
        public void Given我按下馬上簽到GO按鈕()
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("manual_checkIn")));
            element.Click();
        }


        [Given(@"我點選這個月的簽到行事曆")]
        public void Given我點選這個月的簽到行事曆()
        {
            Actions action = new Actions(_webDriver.Current);
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//div[@id='ckeck-in-range']/div/div")));
            action.MoveToElement(element).Click().Build().Perform();
            DateTime today = DateTime.Today;
            int currentMonth = DateTime.Today.Day > 20 ? DateTime.Now.Month : DateTime.Now.Month - 1;
            int currentYear = DateTime.Today.Year;
            string xpath = string.Format("//ul[@id='countSalaryRange_listbox']/li[contains(.,'{0}/{1}/21~{2}/{3}/20')]", currentYear, currentMonth, 
                currentMonth == 12 ? currentYear + 1 : currentYear, currentMonth == 12 ? 1 : currentMonth + 1);
            element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(xpath)));
            action.MoveToElement(element).Click().Build().Perform();
        }

        [Given(@"我填入今天的簽到時間 (.*)")]
        public void Given我填入今天的簽到時間(string StartTime)
        {
            DateTime today = DateTime.Today;
            string id = string.Format("{0}{1}{2}-start-time", today.Year, today.Month, today.Day);
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            element.Clear();
            element.SendKeys(StartTime);
        }

        [Given(@"我填入今天的簽退時間 (.*)")]
        public void Given我填入今天的簽退時間(string endTime)
        {
            DateTime today = DateTime.Today;
            string id = string.Format("{0}{1}{2}-end-time", today.Year, today.Month, today.Day);
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            element.Clear();
            element.SendKeys(endTime);
            element.SendKeys("\t");
        }

        [Given(@"我按下今日的送出按鈕")]
        public void When我按下今日的送出按鈕()
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(
                By.XPath("//td[@class='today-date weekday']/div/div[3]/button")));
            element.Click();
        }

        [Given(@"我按下OK按鈕")]
        public void Given我按下OK按鈕()
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[contains(.,'OK')]")));
            element.Click();
        }

        [Then(@"應該出現簽到失敗視窗並顯示錯誤訊息 (.*)")]
        public void Then應該出現簽到失敗視窗並顯示錯誤訊息(string errorMessage)
        {
            var alertWindowTitleElement = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//h2[contains(.,'簽到失敗')]")));
            var errorMessageElement = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath(string.Format("//p[contains(.,'{0}')]", errorMessage))));
            Assert.AreEqual("簽到失敗", alertWindowTitleElement.Text);
            Assert.AreEqual(errorMessage, errorMessageElement.Text);
        }



        [Given(@"我填寫員工資料")]
        public void Given我填寫員工資料(Table table)
        {
            TableRow row = table.Rows.Single();
            //輸入
            var input = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//div[@id='detail-form']/div[4]/div/div/div/input")));
            input.SendKeys(row["支援BU"]);
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//ul[@id='depSerilNoAct_listbox']/li")));
            element.Click();
            input = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//div[@id='buLeader-div']/div/div/input")));
            input.SendKeys(row["支援專案Leader"]);
            element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//li[@id='buLeader_option_selected']")));
            element.Click();
            input = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id("supProjectName")));
            input.Clear();
            input.SendKeys(row["支援專案名稱"]);
        }
        [Then(@"關於我內的資料應該為")]
        public void Then關於我內的資料應該為(Table table)
        {
            TableRow row = table.Rows.Single();
            var BUElement = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//ul[@id='depSerilNoAct_taglist']/li/span")));
            var leaderElement = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(
                By.XPath("//ul[@id='buLeader_taglist']/li/span")));
            var projectElement = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(
                By.Id("supProjectName")));

            Assert.AreEqual(row["支援BU"], BUElement.Text);
            Assert.AreEqual(row["支援專案Leader"], leaderElement.Text);
            Assert.AreEqual(row["支援專案名稱"], projectElement.GetAttribute("value"));
        }



        [When(@"我按下修改按鈕")]
        public void When我按下修改按鈕()
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("update-btn")));
            element.Click();
        }

        [Given(@"我按下修改按鈕")]
        public void Given我按下修改按鈕()
        {
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("update-btn")));
            element.Click();
        }

        [Then(@"今天的簽到時間應該為 (.*)")]
        public void Then今天的簽到時間應該為(string startTime)
        {
            DateTime today = DateTime.Today;
            string id = string.Format("{0}{1}{2}-start-time", today.Year, today.Month, today.Day);
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            Assert.AreEqual(startTime, element.GetAttribute("value"));
        }

        [Then(@"今天的簽退時間應該為 (.*)")]
        public void Then今天的簽退時間應該為(string EndTime)
        {
            DateTime today = DateTime.Today;
            string id = string.Format("{0}{1}{2}-end-time", today.Year, today.Month, today.Day);
            var element = _webDriver.Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(id)));
            Assert.AreEqual(EndTime, element.GetAttribute("value"));
        }

        [AfterScenario]
        private void CloseBrowser()
        {
            _webDriver.Quit();
        }
    }
}
