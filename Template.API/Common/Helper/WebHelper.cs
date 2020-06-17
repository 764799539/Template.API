using System;
using System.Text;

namespace Template.API
{
    /// <summary>
    /// 网站帮助类
    /// </summary>
    public static class WebHelper
    {
        static object locker = new object();
        static Random _random = new Random(); //随机数对象

        /// <summary>
        /// 获取唯一数字编码
        /// </summary>
        /// <returns>The unique code.</returns>
        /// <param name="preStr">编码前缀</param>
        public static string GetUniqueCode(string preStr = "")
        {
            lock (locker)
            {
                return preStr + DateTime.Now.ToString("yyMMddHHmmssfff") + GetRandomInt(100000, 999999);
            }
        }

        /// <summary>
        /// 生成一个指定范围的随机整数，该随机数范围包括最小值，但不包括最大值
        /// </summary>
        /// <param name="minNum">最小值</param>
        /// <param name="maxNum">最大值</param>
        public static int GetRandomInt(int minNum, int maxNum)
        {
            return _random.Next(minNum, maxNum);
        }

        /// <summary>
        /// 隐藏敏感信息
        /// </summary>
        /// <param name="input">信息实体</param>
        /// <param name="left">左边保留的字符数</param>
        /// <param name="right">右边保留的字符数</param>
        /// <param name="basedOnLeft">当长度异常时，是否显示左边;<code>true</code>显示左边，<code>false</code>显示右边</param>
        /// <returns></returns>
        public static string HideSensitiveInfo(string input, int left, int right, bool basedOnLeft = true)
        {
            if (String.IsNullOrEmpty(input))
            {
                return "";
            }
            StringBuilder sbText = new StringBuilder();
            int hiddenCharCount = input.Length - left - right;
            if (hiddenCharCount > 0)
            {
                string prefix = input.Substring(0, left), suffix = input.Substring(input.Length - right);
                sbText.Append(prefix);
                for (int i = 0; i < hiddenCharCount; i++)
                {
                    sbText.Append("*");
                }
                sbText.Append(suffix);
            }
            else
            {
                if (basedOnLeft)
                {
                    if (input.Length > left && left > 0)
                    {
                        sbText.Append(input.Substring(0, left) + "****");
                    }
                    else
                    {
                        sbText.Append(input.Substring(0, 1) + "****");
                    }
                }
                else
                {
                    if (input.Length > right && right > 0)
                    {
                        sbText.Append("****" + input.Substring(input.Length - right));
                    }
                    else
                    {
                        sbText.Append("****" + input.Substring(input.Length - 1));
                    }
                }
            }
            return sbText.ToString();
        }

		///// <summary>
  //      /// 获取系统编码
  //      /// </summary>
  //      /// <returns>The unique code.</returns>
  //      /// <param name="code">编码名称</param>
  //      public static string GetSystemCode(string code)
  //      {
  //          var strReturn = string.Empty;
  //          lock (locker)
  //          {
  //              var sysCode = ServiceHelper.GetService<ICommonService>().Get< >(p => p.OperationCode == code);
  //              strReturn += sysCode.PreStr;
  //              int BaseNum = sysCode.BaseNum;
  //              int AddNum = sysCode.AddNum;
  //              int Length = sysCode.Length;
  //              //如何基数超出长度限制，则长度自动改变
  //              BaseNum += AddNum;
  //              int baseNumLength = Convert.ToString(BaseNum).Length;
  //              if (baseNumLength > Length)
  //              {
  //                  Length = baseNumLength;
  //              }
  //              for (int i = 0; i < (Length - baseNumLength); i++)
  //              {
  //                  strReturn += "0";
  //              }
  //              strReturn += BaseNum.ToString();
  //              sysCode.BaseNum = BaseNum;
  //              sysCode.Length = Length;
  //              ServiceHelper.GetService<ICommonService>().Update(sysCode);

  //              return strReturn;
  //          }
  //      }
    }
}
