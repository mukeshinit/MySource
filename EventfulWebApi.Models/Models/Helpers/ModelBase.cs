namespace EventfulWebApi.Models.Models.Helpers
{
    using System;
    public class ModelBase : ICloneable
    {
        public ModelBase() { }

        public object this[string property]
        {
            get
            {
                var prop = this.GetType().GetProperty(property);
                if (prop != null)
                {
                    return prop.GetValue(this, null);
                }

                return null;
            }
        }
        public object Clone()
        {
            ModelBase clonedObj = new ModelBase();
            return clonedObj;
        }
    }
}
