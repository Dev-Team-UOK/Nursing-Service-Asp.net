using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Nursing_Service.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString()).FirstOrDefault();
            if (memberInfo != null)
            {
                var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
                if (displayAttribute != null)
                {
                    return displayAttribute.Name!;
                }
            }
            return enumValue.ToString(); // Fallback to the enum name if no Display attribute is found
        }
    }
}
