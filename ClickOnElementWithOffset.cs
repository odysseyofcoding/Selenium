public static Random offsetRandom = new Random();
public static Actions moveToElement = new Actions(driver);

public static void ClickOnElement(string pathToElement, string messeage)
{

  moveToElement.Reset();
  
  IWebElement webElement = driver.FindElementByXPath(pathToElement);

  int elementWidth = webElement.Size.Width;
  int elementHeight = webElement.Size.Height;

  moveToElement.MoveToElement(webElement,
            offsetRandom.Next(1, elementWidth - 5),
            offsetRandom.Next(1, elementHeight - 5))
            .Click().Build().Perform();
            
}
