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
    /// �o�^���N���X
    /// </summary>
    /// <remarks>
    /// �쐬��    �F  kawana
    /// �쐬��    �F  2019/10/24
    /// </remarks>
    public class Register
    {
        #region �v���p�e�B

        [DisplayName("�\���")]
        public string YoteiDate { get; set; }

        [DisplayName("��Ɠ�")]
        public string CommitDate { get; set; }

        [DisplayName("�S����")]
        public string EmployeeCd { get; set; }
        public string EmployeeName { get; set; }

        [DisplayName("��������")]
        public string WorkTimeFrom { get; set; }
        public string WorkTimeTo { get; set; }

        [DisplayName("��ƃ���")]
        public string WorkMemo { get; set; }

        [DisplayName("�������C��")]
        public string PRODUCT_LINE { get; set; }

        [DisplayName("���ۃ��C��")]
        public string GENSHO_LINE { get; set; }

        [DisplayName("�A�E�g�Ő�")]
        public string OUT_HANSU { get; set; }

        [DisplayName("���L��")]
        public string Memo { get; set; }

        [DisplayName("�P�[�XNo")]
        public string CaseNo { get; set; }

        [DisplayName("�F���{����")]
        public bool ColorFlg { get; set; } // �F���{�����t���O
        public string ColorFlg_Str { get; set; }
        public string Chk1 { get; set; }

        [DisplayName("��������")]
        public bool DocFlg { get; set; } // ���������t���O
        public string DocFlg_Str { get; set; }
        public string Chk2 { get; set; }   

        public bool DisabledFlg { get; set; } // �o�^�Ώۃt���O

        #endregion


        #region �R���X�g���N�^

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