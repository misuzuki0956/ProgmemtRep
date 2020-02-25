using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Web.Mvc;

namespace PROGMGMT.Models.Jushihan
{
    /// <summary>
    /// ���������N���X
    /// </summary>
    /// <remarks>
    /// �쐬��    �F  kawana
    /// �쐬��    �F  2019/10/24
    /// </remarks>
    public class Condition
    {
        #region �v���p�e�B

        [DisplayName("�\���")]
        public string ScheDateFrom { get; set; }

        public string ScheDateTo { get; set; }

        [DisplayName("�H��")]
        public string Process { get; set; }

        [DisplayName("��Ɗ����f�[�^���o�͂���")]
        public Boolean OutPut_Chk { get; set; }

        [DisplayName("�Ŏ�")]
        public string[] Han_Type { get; set; }

        [DisplayName("�l�KNo")]
        public string Nega_No { get; set; }

        [DisplayName("�ďo��No")]
        public string Dpy_No { get; set; }

        [DisplayName("�o�͊���")]
        public string OutputDateFrom { get; set; }          // CSV�o�͉�ʗp
        public string OutputDateTo { get; set; }            // CSV�o�͉�ʗp

        [DisplayName("�H��")]
        public string[] OutputProcess { get; set; }         // CSV�o�͉�ʗp
        public string ConditionErrorMessage { get; set; }   // �����擾�G���[
        public string InputErrorMessage { get; set; }       // ���̓G���[
        public SelectList ProcessList { get; set; }         // �H�����X�g
        public SelectList HanTypeList { get; set; }         // �Ŏ����X�g

        #endregion

        #region �R���X�g���N�^

        public Condition()
        {
            ScheDateFrom = DateTime.Now.ToString("yyyy-MM-dd");    // �\��� (From)�F����
            OutputDateFrom = DateTime.Now.ToString("yyyy-MM-dd");  // �\��� (To)  �F����
            GetProcessList();
            GetHanTypeList();
        }

        #endregion

        #region ���\�b�h

        /// <summary>
        /// �H�����X�g�擾
        /// </summary>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/24
        /// </remarks>
        private void GetProcessList()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                dataBase = new DataBase();
                string sqlStr = QueryBuild.GetProcess(Constants.PRODUCT_JUSHIHAN);

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, null);


                foreach (DataRow row in dtSet.Tables[0].Rows)
                {
                    list.Add(new SelectListItem
                    {
                        Text = row["PROCESS_NM"].ToString(),
                        Value = row["PROCESS_CD"].ToString()
                    });
                }

                dataBase.DisconnectDB();
            }
            catch (Exception ex)
            {
                ConditionErrorMessage = Resources.TextResource.ErrorGetCondition;
            }
            finally
            {
                ProcessList = new SelectList(list, "Value", "Text");
                if (dataBase != null)
                {
                    dataBase.DisconnectDB();
                }
                if (dtSet != null)
                {
                    dtSet.Dispose();
                }
            }

        }

        /// <summary>
        /// �Ŏ����X�g(�����ŗp)�擾
        /// </summary>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/24
        /// </remarks>
        private void GetHanTypeList()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {

                dataBase = new DataBase();
                string sqlStr = QueryBuild.GetHanTypeJushi();

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, null);

                foreach (DataRow row in dtSet.Tables[0].Rows)
                {
                    list.Add(new SelectListItem
                    {
                        Text = row["HANSKI_NM"].ToString(),
                        Value = row["HANSKI_CD"].ToString()
                    });
                }

                dataBase.DisconnectDB();
            }
            catch (Exception ex)
            {
                ConditionErrorMessage = Resources.TextResource.ErrorGetCondition;
            }
            finally
            {
                HanTypeList = new SelectList(list, "Value", "Text");
                if (dataBase != null)
                {
                    dataBase.DisconnectDB();
                }
                if (dtSet != null)
                {
                    dtSet.Dispose();
                }
            }
        }

        /// <summary>
        /// ���� ���̓`�F�b�N
        /// </summary>
        /// <returns>True=����OK�AFalse=���̓G���[</returns>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/24
        /// </remarks>
        public bool ValidateSearch()
        {
            InputErrorMessage = Utilities.CheckDateFromTo(ScheDateFrom, ScheDateTo, "�\���");
            return string.IsNullOrEmpty(InputErrorMessage);
        }

        /// <summary>
        /// CSV�o�� ���̓`�F�b�N
        /// </summary>
        /// <returns>True=����OK�AFalse=���̓G���[</returns>
        /// �쐬��    �F  sesaki
        /// �쐬��    �F  2019/11/06
        /// </remarks>
        public bool ValidateOutput()
        {
            InputErrorMessage = Utilities.CheckDateFromTo(OutputDateFrom, OutputDateTo, "�o�͊���");
            return string.IsNullOrEmpty(InputErrorMessage);
        }

        #endregion
    }
}

