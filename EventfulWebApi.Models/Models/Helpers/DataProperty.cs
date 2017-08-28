namespace EventfulWebApi.Models.Models.Helpers
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class DataProperty : Attribute
    {
        public string ColumnName { get; protected set; }
        public bool IsObject { get; set; }

        public DataProperty(string columnName)
        {
            this.ColumnName = columnName;
        }

        public DataProperty(string columnName, bool isObject)
        {
            this.ColumnName = columnName;
            this.IsObject = isObject;
        }

    }
}
