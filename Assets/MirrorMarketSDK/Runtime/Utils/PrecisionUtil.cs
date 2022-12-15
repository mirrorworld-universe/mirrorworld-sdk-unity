using UnityEngine;
using System.Collections;

namespace MirrorworldSDK
{
    public class PrecisionUtil
    {
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
