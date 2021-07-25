using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace GitHub_Examples
{
    class Program
    {
        static void Main(string[] args)
        {
           FirefoxDriver driver = new FirefoxDriver();
           IJavaScriptExecutor JavaScript = (IJavaScriptExecutor)driver;
           
           IWebElement element = driver.FindElementByXPath("XPath");
           ScrollIntoView(element);          
        }
        public static void ScrollIntoView(IWebElement element)
        {
          JavaScript.ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'})", element);
        }
        public static void ScrollPageToBottom()
        {
          JavaScript.ExecuteScript("window.scrollBy({top: document.body.scrollHeight, behavior:'smooth'})");
        }
    }
}
