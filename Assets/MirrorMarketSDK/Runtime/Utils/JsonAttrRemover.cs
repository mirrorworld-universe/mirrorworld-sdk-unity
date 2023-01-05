using UnityEngine;
using System.Collections;

namespace MirrorworldSDK
{
    public class JsonAttrRemover
    {
        public static string RemoveAttr(string originJson, string attrName)
        {
            Debug.Log("ori:"+ originJson);

            string startStr = "\"" + attrName + "\":\"";
            int startIndex = originJson.IndexOf(startStr);
            Debug.Log("startindex:" + startIndex+"   str:"+ startStr);

            string endStr = "\"";
            int endIndex = originJson.IndexOf(endStr, startIndex+ startStr.Length+ 1);
            Debug.Log("endIndex:" + endIndex + "   str:" + endStr);

            string followChar = originJson.Substring(endIndex+1, 1);
            Debug.Log("followChar:"+ followChar);

            if (followChar.Equals(","))
            {
                endIndex++;
            }

            string preChar = originJson.Substring(startIndex - 1, 1);
            Debug.Log("preChar:" + preChar);

            if (preChar.Equals(","))
            {
                startIndex--;
            }

            Debug.Log(startIndex+" "+endIndex);
            string targetStr = originJson.Substring(startIndex, endIndex - startIndex + 1);
            Debug.Log("targetStr " + targetStr);
            return originJson.Replace(targetStr, "");
        }

        public static string RemoveEmptyAttr(string originJson)
        {
            string result = RemoveOneEmptyAttr(originJson);

            if(result.Equals(originJson))
            {
                return originJson;
            }
            else
            {
                return RemoveEmptyAttr(result);
            }
        }

        private static string RemoveOneEmptyAttr(string originJson)
        {
            Debug.Log("ori:" + originJson);

            string emptyTag = ":\"\"";
            int startIndex = originJson.IndexOf(emptyTag);
            Debug.Log("startindex:" + startIndex + "   str:" + emptyTag);

            if(startIndex == -1)
            {
                return originJson;
            }

            int endIdx = -1;
            int count = emptyTag.Length + 1;
            for(int i= startIndex-1; i > -1; i--)
            {
                count++;
                string letter = originJson.Substring(i-1,1);
                if (letter.Equals("\""))
                {
                    endIdx = i;
                    break;
                }
            }
            Debug.Log("endIdx:" + endIdx + "   count:" + count);

            string followChar = originJson.Substring(endIdx-2, 1);
            Debug.Log("followChar:"+ followChar);

            endIdx -= 1;
            if (followChar.Equals(","))
            {
                endIdx -= 1;
                count++;
            }
            else if (followChar.Equals("{"))
            {
                string after = originJson.Substring(startIndex+emptyTag.Length, 1);
                Debug.Log("after:" + after);
                if (after.Equals(","))
                {
                    count++;
                }
            }
            Debug.Log(startIndex + " " + count);
            string target = originJson.Substring(endIdx, count);
            return originJson.Replace(target, "");
        }
    }
}
    
