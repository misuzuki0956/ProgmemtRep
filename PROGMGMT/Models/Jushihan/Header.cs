using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace PROGMGMT.Models.Jushihan
{
    /// </summary>
    /// �w�b�_���N���X
    /// </summary>
    /// <remarks>
    /// �쐬��    �F  kawana
    /// �쐬��    �F  2019/10/24
    /// </remarks>
    public class Header
    {
        #region �v���p�e�B

        [DisplayName("�l�KNo")]
        public string SUBNEGA_NO { get; set; }

        [DisplayName("�ďo��No")]
        public string JSBDPY_NO { get; set; }

        [DisplayName("�������e")]
        public string CMNY { get; set; }

        [DisplayName("�u�����h")]
        public string BURANDO_NM { get; set; }

        [DisplayName("������")]
        public string CUSTMER_RNM { get; set; }

        [DisplayName("���b�g��")]
        public string LOTSU { get; set; }

        [DisplayName("�Ő�")]
        public string HANSU { get; set; }

        [DisplayName("�i��")]
        public string SYOHIN_NM { get; set; }

        [DisplayName("�e��^")]
        public string KAN_NM { get; set; }

        [DisplayName("�}��No")]
        public string ZUMEN_NO { get; set; }

        [DisplayName("�Ŏ�")]
        public string HAN_KBN { get; set; }

        [DisplayName("�F��")]
        public string COLOR_NM { get; set; }

        #endregion

        #region �R���X�g���N�^

        public Header() { }

        public Header(string dpyno)
        {
            JSBDPY_NO = dpyno;
            GetHeader();
        }

        #endregion

        #region ���\�b�h

        /// <summary>
        /// �w�b�_���擾
        /// </summary>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/24
        /// </remarks>
        private void GetHeader()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            try
            {
                dataBase = new DataBase();
                List<object> paraList = new List<object>();
                string sqlStr = QueryBuild.GetJushihanHeader(JSBDPY_NO, ref paraList);

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, paraList.ToArray());

                DataRow row = dtSet.Tables[0].Rows[0];

                SUBNEGA_NO = row["SUBNEGA_NO"].ToString();
                CMNY = row["CMNY_NM"].ToString();
                LOTSU = row["LOTSU"].ToString();
                HANSU = row["HANSU"].ToString();
                BURANDO_NM = row["BURANDO_NM"].ToString();
                CUSTMER_RNM = row["CUSTMER_RNM"].ToString();
                SYOHIN_NM = row["SYOHIN_NM"].ToString();
                KAN_NM = row["KAN_NM"].ToString();
                ZUMEN_NO = row["ZUMEN_NO"].ToString();
                HAN_KBN = row["HAN_KBN"].ToString();
                COLOR_NM = row["COLOR_NM"].ToString();

                dataBase.DisconnectDB();
            }
            catch (Exception ex)
            {

            }
            finally
            {
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

        #endregion
    }
}
