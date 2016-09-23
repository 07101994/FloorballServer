using System;

namespace Bll
{
    public enum CountriesEnum
    {
        HU, SE, FL, SW, CZ
    }

    public static class CountriesEnumExtensions
    {
        public static string ToFriendlyString(this CountriesEnum country)
        {
            switch (country)
            {
                case CountriesEnum.HU:
                    return "HU";
                case CountriesEnum.SE:
                    return "SE";
                case CountriesEnum.FL:
                    return "FL";
                case CountriesEnum.SW:
                    return "SW";
                case CountriesEnum.CZ:
                    return "CZ";
                default:
                    return "";
            }
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
