using System.Configuration;
using System.IO;
using System.Xml;

namespace Toggl.Tests
{
    public static class Constants
    {
        private static string _apiToken;

        static Constants()
        {
            _apiToken = ReadApiToken();
        }


        public static string ApiToken
        {
            get { return _apiToken; }
            set { WriteApiToken(value); }
        }

        public const int DefaultUserId = 387715;





        private static SettingElement GetSetting(Configuration config, string name)
        {
            var group = config.GetSectionGroup("applicationSettings");
            var section = group.Sections.Get("Toggl.Tests.Properties.Settings") as ClientSettingsSection;
            var settings = section.Settings;
            var setting = settings.Get(name);
            return setting;
        }

        private static string ReadApiToken()
        {
            var config = ConfigurationManager.OpenExeConfiguration(Path.Combine(Directory.GetCurrentDirectory(), "TogglAPI.NetStandard.Tests.dll"));
            var setting = GetSetting(config, "ApiToken");
            var result = setting.Value.ValueXml.InnerText;
            return result;
        }

        private static void WriteApiToken(string apiToken)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var setting = GetSetting(config, "ApiToken");
            
            var doc = new XmlDocument();
            var valueElem = doc.CreateElement("value");
            valueElem.AppendChild(doc.CreateTextNode(apiToken));
            setting.Value.ValueXml = valueElem;
            config.Save(ConfigurationSaveMode.Modified);
            _apiToken = apiToken;
        }

    }
}
