using System.Collections.Generic;

namespace SingletonPractice
{
    public class ConfigManager
    {
        private static ConfigManager _instance;
        private static readonly object _lock = new object();
        private Dictionary<string, string> _config;

        private ConfigManager()
        {
            _config = new Dictionary<string, string>();
            LoadDefaultConfig();
            Console.WriteLine("ConfigManager: Завантажено конфігурацію");
        }

        private void LoadDefaultConfig()
        {
            _config["AppName"] = "MyApplication";
            _config["Version"] = "1.0.0";
            _config["Language"] = "uk-UA";
            _config["Theme"] = "Dark";
        }

        public static ConfigManager GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfigManager();
                    }
                }
            }
            return _instance;
        }

        public string GetConfig(string key)
        {
            if (_config.ContainsKey(key))
            {
                return _config[key];
            }
            return null;
        }

        public void SetConfig(string key, string value)
        {
            _config[key] = value;
            Console.WriteLine("ConfigManager: Оновлено конфігурацію - " + key + " = " + value);
        }

        public void PrintAllConfig()
        {
            Console.WriteLine("ConfigManager: Вся конфігурація:");
            foreach (var item in _config)
            {
                Console.WriteLine("  " + item.Key + " = " + item.Value);
            }
        }
    }
}
