namespace EventfulWebApi.Config
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static System.Configuration.ConfigurationManager;
    using EventfulWebApi.Logging;
    using log4net.Core;
    public class ConfigProvider
    {
        private string _EventfulApiUrl;
        private string _EventFulClientKey;
        private string _GoogleGeoApiUrl;
        private string _GoogleApiClientKey;
        private string _OauthCosumerKey;
        private string _OauthCosumerSecret;
        private string _EventfulCatApiUrl;

        private int _NumberofThreads;
        private int _NumberofRetriesForFailures;
        private int _MillisecondsToWaitBewteenRetries;

        public static ConfigProvider Instance { get; protected set; }        

        [ConfigAttribute]
        public bool IsConfigLoaded { get; set; } = false;   
        //apply regular expression annotations to validate
        [ConfigAttribute]
        [Required]
        public string EventfulApiUrl
        {
            get => _EventfulApiUrl;
            protected set => _EventfulApiUrl = value;
        }
        [ConfigAttribute]
        [Required]
        public string EventFulClientKey
        {
            get => _EventFulClientKey;
            protected set
            {
                _EventFulClientKey = "JF8kWvMTtC7S4hjc";
            }
        }
        [ConfigAttribute]
        [Required]
        public string OauthCosumerKey
        {
            get => _OauthCosumerKey;
            protected set
            {
                _OauthCosumerKey = "d625f4af4b7e41801b66";
            }
        }
        [ConfigAttribute]
        [Required]
        public string OauthCosumerSecret
        {
            get => _OauthCosumerSecret;
            protected set
            {
                _OauthCosumerSecret = "398e822fcbd884fdd0ee";
            }
        }
        [ConfigAttribute]
        [Required]
        public string GoogleGeoApiUrl
        {
            get => _GoogleGeoApiUrl;
            protected set => _GoogleGeoApiUrl = value;
        }        
        [ConfigAttribute]
        [Required]
        public string GoogleApiClientKey
        {
            get => _GoogleApiClientKey;
            protected set
            {
                _GoogleApiClientKey = "AIzaSyCnxEFqeEFy0X6CjdC1b0AQ-YLk7aGuWno";
            }
        }
        [ConfigAttribute]
        [Required]
        public string EventfulCatApiUrl
        {
            get => _EventfulCatApiUrl;
            protected set => _EventfulCatApiUrl = value;
        }
        [ConfigAttribute]
        [Required]
        public int NumberofThreads
        {
            get => _NumberofThreads;
            protected set => _NumberofThreads = value;
        }

        [ConfigAttribute]
        [Required]
        public int NumberofRetriesForFailures
        {
            get => _NumberofRetriesForFailures;
            protected set => _NumberofRetriesForFailures = value;
        }

        [ConfigAttribute]
        [Required]
        public int MillisecondsToWaitBewteenRetries
        {
            get => _MillisecondsToWaitBewteenRetries;
            protected set => _MillisecondsToWaitBewteenRetries = value;
        }
        static ConfigProvider()
        {
            Logger.Log(Level.Info,"Loading config entries..");
            Instance = new ConfigProvider(); 

            Instance.LoadConfiguration();
            Instance.IsConfigLoaded = true;

            var context = new ValidationContext(Instance, null, null);
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(Instance, context, results))
            {
                Logger.Log(Level.Error, "Issue with one or more config entries. Validate config entries", null);
                foreach (var item in results)
                {   
                    Logger.Log(Level.Error, item.ErrorMessage, null);
                }

                Instance.IsConfigLoaded = false;
            }

            Logger.Log(Level.Info,"Config entries loaded");
        }

        private void LoadConfiguration()
        {
            var properties = this.GetType().GetProperties();
            foreach (var p in properties)
            {
                if (Attribute.GetCustomAttribute(p, typeof(ConfigAttribute)) is ConfigAttribute attribute)
                {
                    var value = AppSettings[p.Name];
                    object actValue;
                    if (p.PropertyType.Equals(typeof(long)))
                    {
                        actValue = Convert.ToInt64(value);
                    }
                    else if (p.PropertyType.Equals(typeof(int)))
                    {
                        actValue = Convert.ToInt32(value);
                    }
                    else if (p.PropertyType.Equals(typeof(bool)))
                    {
                        actValue = Convert.ToBoolean(value);
                    }
                    else
                    {
                        actValue = Convert.ToString(value);
                    }

                    p.SetValue(this, actValue, null);
                }
            }
        }
    }
}
