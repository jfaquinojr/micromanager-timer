using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroManager.Timer.Core.Presenters
{
    public class PomodoroConfiguration : IPomodoroConfiguration
    {
        private readonly string _configFilename;
        private readonly Dictionary<string, object> _settings;
        private readonly ILogger _logger;

        public PomodoroConfiguration()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _configFilename = typeof(PomodoroConfiguration).Assembly.GetName().Name + ".json";
            var json = "";
            if(!File.Exists(_configFilename))
            {
                _logger.Info("Config file does not exist. Creating '{configfileName}'", _configFilename);
                CreateDefaultSettings(_configFilename);
            }
            json = File.ReadAllText(_configFilename);
            _settings = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            
        }

        private void CreateDefaultSettings(string configFilename)
        {
            var defaultSettings = new Dictionary<string, object>();
            // default values:
            // 4  pomodoros before long break
            // 15 minutes long break
            // 5  minutes short break
            // 25 minutes pomodoro duration
            defaultSettings.Add(ConfigurationConstants.PomodoroBeforeLongBreak, 4);
            defaultSettings.Add(ConfigurationConstants.LongBreakInMinutes, 15);
            defaultSettings.Add(ConfigurationConstants.ShortBreakInMinutes, 5);
            defaultSettings.Add(ConfigurationConstants.PomodoroDuration, 25);
            WriteJson(defaultSettings);
        }

        public T GetConfig<T>(string name, T defaultValue)
        {
            if (!_settings.ContainsKey(name))
            {
                return defaultValue;
            }

            var ret = (T)Convert.ChangeType(_settings[name], typeof(T));

            switch (_settings[name])
            {
                case long l: 
                    return l == 0 ? defaultValue : ret;
                case int n:
                    return n == 0 ? defaultValue : ret;
                case string s:
                    return s == "" ? defaultValue : ret;
                case null:
                    return defaultValue;
                default:
                    return ret;
            }
        }

        public void WriteConfig<T>(string name, T value)
        {
            _settings[name] = value.ToString();
            WriteJson(_settings);
        }

        private void WriteJson(Dictionary<string, object> settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(_configFilename, json);
        }
    }
}
