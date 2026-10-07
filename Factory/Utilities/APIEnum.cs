using System.ComponentModel;

namespace DotNet8.WebApi.Factory.Utilities
{
    public class APIEnum
    {
        public enum Enum
        {
            [Description("Feature not available.")]
            ERR_CODE1=1,
            [Description("Failed to test.")]
            ERR_CODE2=2,
        }
    
        public static string getDescription(APIEnum.Enum err)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])err.GetType().GetField(err.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
