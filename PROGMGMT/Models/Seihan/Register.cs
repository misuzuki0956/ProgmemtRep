using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Seihan
{
    /// <summary>
    /// 登録情報クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/09/01
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

        [DisplayName("作業時間")]
        public string WorkTimeFrom { get; set; }

        public string WorkTimeTo { get; set; }

        [DisplayName("作業メモ")]
        public string WorkMemo { get; set; }

        [DisplayName("校正ライン")]
        public string KoseiLine { get; set; }

        [DisplayName("特記欄")]
        public string Memo { get; set; }

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
            KoseiLine = row["KOSEI_LINE"].ToString();
            Memo = row["MEMO"].ToString();
            DisabledFlg = flg;
        }
        #endregion

    }
}