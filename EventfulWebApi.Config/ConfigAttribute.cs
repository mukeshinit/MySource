namespace EventfulWebApi.Config
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class ConfigAttribute : Attribute
    {
        public ConfigAttribute() { }
    }
}
    