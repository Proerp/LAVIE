using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

using TotalBase.Enums;

namespace TotalBase
{
    public class CommonExpressions
    {
        public static string PropertyName<T>(Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        public static string AlphaNumericString(string normalString)
        {
            return Regex.Replace(normalString, @"[^0-9a-zA-Z]+", "");
            //return Regex.Replace(normalString, @"[^0-9a-zA-Z\*\+\(\)]+", "");
        }

        public static string AlphaNumericStringLOTNUMBER(string normalString)
        {
            return Regex.Replace(normalString, @"[^0-9A-Z]+", "");
        }

        public static string ComposeCommodityCode(string code, int commodityTypeID)
        {
            code = TotalBase.CommonExpressions.AlphaNumericString(code);

            if (commodityTypeID != (int)GlobalEnums.CommodityTypeID.Vehicles && code.Length >= 9)
                return code.Substring(0, 5) + "-" + code.Substring(5, 3) + "-" + code.Substring(8, code.Length - 8);
            else
                return code;
        }

        public static bool IsFV(string commodityCode) { return commodityCode != null && commodityCode.Length > 3 && commodityCode.Substring(0, 2) == "FV"; }
    }
}
