using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

    class Gecko
    {
        public FirefoxDriver InitiateDriver() 
        {
            //To get users session Id before starting new Gecko
            IEnumerable<int> pidsBefore = Process.GetProcessesByName("firefox").Select(p => p.Id);
            try
            {
                //To Store the Processes to kill
                List<string> myPidsToKill = new List<string>();
                //Start New Processes with timeout if something is incorrect
                FirefoxDriver driver = new FirefoxDriver(Service(), Options(), TimeSpan.FromSeconds(25));
                //To get a list to compare the Process ID's before and after
                IEnumerable<int> pidsAfter = Process.GetProcessesByName("firefox").Select(p => p.Id);
                //Return a list of Process ID's only used by Gecko to kill if necessary
                IEnumerable<int> newFirefoxPids = pidsAfter.Except(pidsBefore);
                //Add and convert the ID's to a List to use it in the next step
                foreach (var item in newFirefoxPids)
                {
                    myPidsToKill.Add(item.ToString());
                }
                //Store the Process ID's wich is Gecko using in a File
                File.WriteAllLines("WriteText.txt", myPidsToKill);
                return driver;
            }
            catch (WebDriverException) 
            {
                //Kill every Process from last session, Gecko should start as wished
                foreach (var item in File.ReadAllLines("WriteText.txt"))
                {
                    Process.GetProcessById(Convert.ToInt32(item)).Kill();
                }
            }
            return null;
        }
        private static FirefoxOptions Options()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArguments("-private", "-profile", @"The Profile is stored at ..\AppData\Roaming");
            options.PageLoadStrategy = PageLoadStrategy.Normal;
            return options;
        }
        private static FirefoxDriverService Service()
        {
            FirefoxDriverService myService = FirefoxDriverService.CreateDefaultService(@"Path to GeckoDriver.exe");
            myService.HideCommandPromptWindow = true;
            //if not using this port Gecko could loose control because desiring to use own profile instead the generated. Could be fixed in future builds.
            myService.BrowserCommunicationPort = 2828;
            return myService;
        }
    }
