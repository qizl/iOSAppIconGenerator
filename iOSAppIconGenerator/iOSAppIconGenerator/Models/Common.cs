using System;
using System.Collections.Generic;
using System.IO;

namespace Com.EnjoyCodes.iOSAppIconGenerator.Models
{
    public class Common
    {
        public static string ConfigPath = Path.Combine(Environment.CurrentDirectory, "Config.xml");

        public static Config Config { get; set; }
        public static Config DefaultConfig = new Config()
        {
            Icons = new List<Icon>() {
                new Icon() { Name = "icon40.png", Tag = "iOS", Width = 40, Height = 40 } ,
                new Icon() { Name = "icon120.png", Tag = "iOS", Width = 120, Height = 120 }
            },
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now
        };
    }
}
