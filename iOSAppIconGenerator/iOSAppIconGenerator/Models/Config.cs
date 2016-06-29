using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.EnjoyCodes.iOSAppIconGenerator.Models
{
    public class Config
    {
        private static string configPath = string.Empty;

        public List<Icon> Icons { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public static Config Load(string path)
        {
            configPath = path;

            Config config = null;
            try
            {
                SharpSerializer.SharpSerializer serializer = new SharpSerializer.SharpSerializer();
                config = serializer.Deserialize(path) as Config;
            }
            catch { }
            return config;
        }

        public bool Save()
        { return this.Save(Config.configPath); }

        public bool Save(string path)
        {
            try
            {
                SharpSerializer.SharpSerializer serializer = new SharpSerializer.SharpSerializer();
                serializer.Serialize(this, path);
            }
            catch { return false; }
            return true;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
