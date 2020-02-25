using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROGMGMT.Models.Jushihan
{
    /// </summary>
    /// RegisterGroup�N���X
    /// </summary>
    /// <remarks>
    /// �쐬��    �F  kawana
    /// �쐬��    �F  2019/10/24
    /// </remarks>
    public class RegisterGroup
    {
        #region �v���p�e�B

        [DisplayName("����G�@����")]
        public Register Spnseizo { get; set; }

        [DisplayName("����G�@����")]
        public Register Spnkensa { get; set; }

        [DisplayName("�Ɩ��ۏo��")]
        public Register Gyoumu { get; set; }

        public SelectList EmployeeList { get; set; }

        public string ErrorGetMgmtMessage { get; set; }

        #endregion

        #region �R���X�g���N�^
        public RegisterGroup()
        {
            GetEmployeeList();
        }

        public RegisterGroup(string dpyno, string process)
        {
            GetEmployeeList();
            GetMgmt(dpyno, process);
        }

        #endregion

        #region ���\�b�h

        /// <summary>
        /// �S���҃��X�g�擾
        /// </summary>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/24
        /// </remarks> 
        private void GetEmployeeList()
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            List<SelectListItem> list = new List<SelectListItem>();

            try
            {
                dataBase = new DataBase();
                string sqlStr = QueryBuild.GetEmployee();

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, null);

                foreach (DataRow row in dtSet.Tables[0].Rows)
                {
                    list.Add(new SelectListItem
                    {
                        Text = row["EMPLOYEE_NM"].ToString(),
                        Value = row["EMPLOYEE_CD"].ToString()
                    });
                }

                dataBase.DisconnectDB();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                EmployeeList = new SelectList(list, "Value", "Text");
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
        /// �i���f�[�^�擾
        /// </summary>
        /// <param name="dpyno">�`�[No</param>
        /// <param name="process">�H��</param>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/24
        /// </remarks>
        private void GetMgmt(string dpyno, string process)
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            try
            {
                dataBase = new DataBase();
                List<object> paraList = new List<object>();
                string sqlStr = QueryBuild.GetJushihanMgmt(dpyno, ref paraList);

                dataBase.ConnectDB();
                dtSet = dataBase.GetDataSet(sqlStr, paraList.ToArray());

                dataBase.DisconnectDB();

                foreach (DataRow row in dtSet.Tables[0].Rows)
                {
                    string cd = row["PROCESS_CD"].ToString();
                    bool disabled_flg = (cd != process);
                    switch (cd)
                    {
                        case Constants.PROCESS_SPNSEIZO:
                            Spnseizo = new Register(row, disabled_flg);
                            break;
                        case Constants.PROCESS_SPNKENSA:
                            Spnkensa = new Register(row, disabled_flg);
                            break;
                        case Constants.PROCESS_GYOUMU:
                            Gyoumu = new Register(row, disabled_flg);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorGetMgmtMessage = Resources.TextResource.ErrorGetMgmt;
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

        /// <summary>
        /// �i���f�[�^�o�^
        /// </summary>
        /// <param name="dpyno">�`�[No</param>
        /// <param name="process">�H��</param>
        /// <param name="uid">���[�U�[ID</param>
        /// <returns>��������</returns>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/24
        /// </remarks>
        public bool RegistMgmt(string dpyno, string process, string uid)
        {

            DataSet dtSet = null;
            DataBase dataBase = null;

            try
            {
                string sqlStr;
                Register register;
                List<object> paraList = new List<object>();

                dataBase = new DataBase();
                dataBase.ConnectDB();

                register = GetTargetRegister(process);
                sqlStr = QueryBuild.RegistJushihanMgmt(register, dpyno, process, Constants.PROCESS_SUB1, uid, ref paraList);
                dataBase.CommitData(sqlStr, paraList.ToArray());

                

                dataBase.DisconnectDB();

            }
            catch
            {
                return false;
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

            return true;
        }

        /// <summary>
        /// �o�^�Ώێ擾
        /// </summary>
        /// <param name="process">�H���R�[�h</param>
        /// <param name="sub">�T�u�H���R�[�h</param>
        /// <returns>�Ώۂ�Register</returns>
        /// <remarks>
        /// �쐬��    �F  kawana
        /// �쐬��    �F  2019/10/24
        /// </remarks>

        private Register GetTargetRegister(string process)
        {
            Register register = null;
            switch (process)
            {
                case Constants.PROCESS_SPNSEIZO:
                    Spnseizo.WorkTimeFrom = Utilities.CheckWorkTime(Spnseizo.WorkTimeFrom, Spnseizo.CommitDate);
                    register = Spnseizo;
                    break;
                case Constants.PROCESS_SPNKENSA:
                    Spnkensa.WorkTimeFrom = Utilities.CheckWorkTime(Spnkensa.WorkTimeFrom, Spnkensa.CommitDate);
                    register = Spnkensa;
                    break;
                case Constants.PROCESS_GYOUMU:
                    Gyoumu.WorkTimeFrom = Utilities.CheckWorkTime(Gyoumu.WorkTimeFrom, Gyoumu.CommitDate);
                    Gyoumu.ColorFlg_Str = Utilities.SetFlgToStr(Gyoumu.ColorFlg);
                    Gyoumu.DocFlg_Str = Utilities.SetFlgToStr(Gyoumu.DocFlg);
                    register = Gyoumu;
                    break;
            }

            return register;
        }

        #endregion

    }
}