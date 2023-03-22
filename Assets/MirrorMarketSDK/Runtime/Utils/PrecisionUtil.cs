using UnityEngine;
using System.Collections;
using MirrorworldSDK.Wrapper;
using System;

namespace MirrorworldSDK
{
    public class PrecisionUtil
    {
        public static string GetApproveValue(double amount)
        {
            int decimals = 9;
            int digit = GetDigit(amount);
            int totalDigit = digit + decimals;
            double dec = Math.Pow(10, decimals);
            double v = amount / dec;
            string strNeed = string.Format("{0:F" + totalDigit + "}", v);

            return strNeed;
        }

        public static string GetApproveValue(double amount, int decimals)
        {
            int digit = GetDigit(amount);
            int totalDigit = digit + decimals;
            double dec = Math.Pow(10, decimals);
            double v = amount / dec;
            string strNeed = string.Format("{0:F" + totalDigit + "}", v);

            return strNeed;
        }

        private static int GetDigit(double number)
        {
            if (haveSmallDigit(number))
            {
                return 0;
            }
            for (int i = 1; i < 10; i++)
            {
                if (haveSmallDigit(number * Math.Pow(10, i)))
                {
                    return i;
                }
            }
            return 10;
        }
        private static bool haveSmallDigit(double number)
        {
            double pre = Math.Truncate(number);

            double after = number - pre;

            if (after == 0)
            {
                return true;
            }

            return false;
        }
        public static double StrToDouble(object FloatString)
        {
            double result;
            if (FloatString != null)
            {
                if (double.TryParse(FloatString.ToString(), out result))
                {
                    Debug.Log("parse:" + FloatString + " r:" + result);
                    return result;
                }
                else
                {
                    return (float)0.00;
                }
            }
            else
            {
                return (float)0.00;
            }
        }


        public static float StrToFloat(object FloatString)
        {
            float result;
            if(FloatString != null)
            {
                if (float.TryParse(FloatString.ToString(),out result))
                {
                    Debug.Log("parse:"+ FloatString+ " r:"+result);
                    return result;
                }
                else
                {
                    return (float)0.00;
                }
            }
            else
            {
                return (float)0.00;
            }
        }

        public static ulong StrToULong(object FloatString)
        {
            ulong result;
            if (FloatString != null)
            {
                if (ulong.TryParse(FloatString.ToString(), out result))
                {
                    Debug.Log("parse:" + FloatString + " r:" + result);
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return (ulong)0;
            }
        }

        public static int StrToInt(object FloatString)
        {
            int result;
            if (FloatString != null)
            {
                if (int.TryParse(FloatString.ToString(), out result))
                {
                    Debug.Log("parse:" + FloatString + " r:" + result);
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
