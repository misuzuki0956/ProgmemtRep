using System.Collections.Generic;
using System.Web.Mvc;

namespace PROGMGMT.Common
{
    /// <summary>
    /// グラビア用得意先クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/11/14
    /// </remarks>
    public class GurabiaCustomer
    {
        /// <summary>
        /// 得意先リスト作成
        /// </summary>
        /// <returns>得意先リスト</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/11/14
        /// </remarks>
        public static SelectList MakeList()
        {
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem { Text = "東洋製罐", Value = "0" },
                new SelectListItem { Text = "東罐興業", Value = "1" }
            };

            return new SelectList(list, "Value", "Text");
        }

        /// <summary>
        /// 東罐興業チェック
        /// </summary>
        /// <param name="code"></param>
        /// <returns>True=東罐興業、False=東罐興業以外</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/11/14
        /// </remarks>
        public static bool IsTokan(string code)
        {
            return code.Equals("1");
        }
    }
}