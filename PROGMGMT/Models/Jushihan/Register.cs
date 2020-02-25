using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;

namespace PROGMGMT.Models.Jushihan
{
    /// </summary>
    /// 登録情報クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  kawana
    /// 作成日    ：  2019/10/24
    /// </remarks>
    public class Register
    {
        #region プロパティ

        [DisplayName("予定日")]
        public string YoteiDate { get; set; }

        [DisplayName("作業日")]
        public string CommitDate { get; set; }

        [DisplayName("担当者")]
        public string EmployeeCd { get; set; }
        public string EmployeeName { get; set; }

        [DisplayName("完了時間")]
        public string WorkTimeFrom { get; set; }
        public string WorkTimeTo { get; set; }

        [DisplayName("作業メモ")]
        public string WorkMemo { get; set; }

        [DisplayName("製造ライン")]
        public string PRODUCT_LINE { get; set; }

        [DisplayName("現象ライン")]
        public string GENSHO_LINE { get; set; }

        [DisplayName("アウト版数")]
        public string OUT_HANSU { get; set; }

        [DisplayName("特記欄")]
        public string Memo { get; set; }

        [DisplayName("ケースNo")]
        public string CaseNo { get; set; }

        [DisplayName("色見本発送")]
        public bool ColorFlg { get; set; } // 色見本発送フラグ
        public string ColorFlg_Str { get; set; }
        public string Chk1 { get; set; }

        [DisplayName("資料発送")]
        public bool DocFlg { get; set; } // 資料発送フラグ
        public string DocFlg_Str { get; set; }
        public string Chk2 { get; set; }   

        public bool DisabledFlg { get; set; } // 登録対象フラグ

        #endregion


        #region コンストラクタ

        public Register()
        {
            DisabledFlg = false;
        }
        public Register(DataRow row, bool flg)
        {
            YoteiDate = row["YOTEI_DAY"].ToString();
            CommitDate = row["COMMIT_DATE"].ToString();
            EmployeeCd = row["EMPLOYEE_CD"].ToString();
            EmployeeName = row["EMPLOYEE_NM"].ToString();
            WorkTimeFrom = row["WORKTIME_FROM"].ToString();
            WorkTimeTo = row["WORKTIME_TO"].ToString();
            WorkMemo = row["WORK_MEMO"].ToString();
            PRODUCT_LINE = row["PRODUCT_LINE"].ToString();
            GENSHO_LINE = row["GENSHO_LINE"].ToString();
            OUT_HANSU = row["OUT_HANSU"].ToString();
            CaseNo = row["CASE_NO"].ToString();
            ColorFlg_Str = row["COLOR_FLG"].ToString();
            ColorFlg = Utilities.SetStrToFlg(row["COLOR_FLG"].ToString());
            DocFlg_Str = row["DOC_FLG"].ToString();
            DocFlg = Utilities.SetStrToFlg(row["DOC_FLG"].ToString());
            Chk1 = row["CHK1"].ToString();
            Chk2 = row["CHK2"].ToString(); 
            Memo = row["MEMO"].ToString();
            DisabledFlg = flg;
        }

        #endregion
    }
}