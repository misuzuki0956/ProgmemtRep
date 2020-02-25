using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.Mvc;

namespace PROGMGMT.Models.User
{
    /// <summary>
    /// ログイン情報クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/09/18
    /// </remarks>
    public class Condition
    {
        [DisplayName("ユーザーID")]
        public string Id { get; set; }
        [DisplayName("パスワード")]
        public string Password { get; set; }

        public string PROCESS_CD { get; set; }
    }
}