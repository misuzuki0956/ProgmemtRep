using PROGMGMT.Common;
using System;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Jushihan
{
    public class SearchResult
    {
        /// <summary>
        /// �������ʃN���X
        /// </summary>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/04
        /// </remarks>
        
        #region �v���p�e�B

        [DisplayName("  ")]
        public string MARK { get; set; }

        [DisplayName("�\���")]
        public string YOTEI_DAY { get; set; }

         [DisplayName("�������e")]
        public string CMNY_NM { get; set; }

        [DisplayName("�ďo��No")]
        public string JSBDPY_NO { get; set; }

        [DisplayName("������")]
        public string CUSTMER_RNM { get; set; }

        [DisplayName("�T�u�l�KNo")]
        public string SUBNEGA_NO { get; set; }

        [DisplayName("�u�����h��")]
        public string BURANDO_NM { get; set; }

        [DisplayName("���i��")]
        public string SYOHIN_NM { get; set; }

        [DisplayName("�e��^")]
        public string KAN_NM { get; set; }

        [DisplayName("�}��No")]
        public string ZUMEN_NO { get; set; }

        [DisplayName("�Ŏ�")]
        public string HAN_KBN { get; set; }

        [DisplayName("���b�g��")]
        public string LOTSU { get; set; }

        [DisplayName("�Ő�")]
        public string HANSU { get; set; }

        [DisplayName("��Ǝw��")]
        public string WORK_SHIJI { get; set; }

        [DisplayName("����")]
        public string DOC_FLG { get; set; }

        [DisplayName("�F���{")]
        public string CLRMH_FLG { get; set; }

        [DisplayName("������")]
        public string COMMIT_DATE_SPNSEIZO { get; set; }

        [DisplayName("������")]
        public string COMMIT_DATE_SPNKENSA { get; set; }

        [DisplayName("����")]
        public string SPN_CHK1 { get; set; }

        [DisplayName("����")]
        public string SPN_CHK2 { get; set; }

        [DisplayName("������")]
        public string COMMIT_DATE_GYOUMU { get; set; }

        #endregion

        #region �R���X�g���N�^
        public SearchResult(DataRow row)
        {
            MARK = row["MARK"].ToString();
            YOTEI_DAY = row["YOTEI_DAY"].ToString();
            CMNY_NM = row["CMNY_NM"].ToString();
            JSBDPY_NO = row["JSBDPY_NO"].ToString();
            CUSTMER_RNM = row["CUSTMER_RNM"].ToString();
            SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
            BURANDO_NM = row["BURANDO_NM"].ToString();
            SYOHIN_NM = row["SYOHIN_NM"].ToString();
            KAN_NM = row["KAN_NM"].ToString();
            ZUMEN_NO = row["ZUMEN_NO"].ToString();
            HAN_KBN = row["HAN_KBN"].ToString();
            LOTSU = row["LOTSU"].ToString();
            HANSU = row["HANSU"].ToString();
            WORK_SHIJI = row["WORK_SHIJI"].ToString();
            CLRMH_FLG = row["GYM_CHK1"].ToString();
            DOC_FLG = row["GYM_CHK2"].ToString();
            COMMIT_DATE_SPNSEIZO = row["COMMIT_DATE_SPNSEIZO"].ToString();
            COMMIT_DATE_SPNKENSA = row["COMMIT_DATE_SPNKENSA"].ToString();
            SPN_CHK1 = row["SPN_CHK1"].ToString();
            SPN_CHK2 = row["SPN_CHK2"].ToString();
            COMMIT_DATE_GYOUMU = row["COMMIT_DATE_GYOUMU"].ToString();
        }
        #endregion

        #region ���\�b�h
        /// <summary>
        /// �o�͔���
        /// </summary>
        /// <param name="proc">�H��</param>
        /// <returns>True=�\���AFalse=��\��</returns>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/04
        /// </remarks>

        public bool CheckOutPut(string proc)
        {
            bool flag = false;

            switch (proc)
            {
                case Constants.PROCESS_SPNSEIZO:  // ����G�@����
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_SPNSEIZO))    //�u�����N���Atrue
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_SPNKENSA:  // ����G�@����
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_SPNKENSA))   //�u�����N���Atrue
                    {
                        flag = true;
                    }
                    break;

                case Constants.PROCESS_GYOUMU:  //�@�Ɩ�
                    if (string.IsNullOrWhiteSpace(COMMIT_DATE_GYOUMU) ||
                        string.IsNullOrWhiteSpace(COMMIT_DATE_SPNSEIZO) ||
                        string.IsNullOrWhiteSpace(COMMIT_DATE_SPNKENSA))
                    {
                        flag = true;
                    }
                    break;
            }
            return flag;
        }
        #endregion
    }
}