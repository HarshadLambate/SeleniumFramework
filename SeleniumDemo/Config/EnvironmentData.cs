using System.Collections.Generic;

namespace SeleniumDemo.Config
{
    public class EnvironmentData
    {
        public string name { get; set; }
        public string url { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Root
    {
        public List<EnvironmentData> environments { get; set; }
    }
}
