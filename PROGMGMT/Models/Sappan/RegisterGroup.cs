using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROGMGMT.Models.Sappan
{
    /// <summary>
    /// 登録情報グループクラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/09/24
    /// </remarks> 
    public class RegisterGroup
    {
        #region プロパティ

        [DisplayName("刷版G　製造")]
        public Register Sappans { get; set; }

        [DisplayName("刷版G　検版")]
        public Register Sappank { get; set; }

        [DisplayName("編集G　製造")]
        public Register Henshus { get; set; }

        [DisplayName("編集G　検査")]
        public Register Henshuk { get; set; }

        [DisplayName("検査G")]
        public Register Kensa { get; set; }

        [DisplayName("業務課出荷")]
        public Register Gyoumu { get; set; }

        

        public SelectList EmployeeList { get; set; }
        
        public string ErrorGetMgmtMessage { get; set; }
        #endregion

        #region コンストラクタ
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

        #region メソッド

        /// <summary>
        /// 担当者リスト取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/24
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
        /// 進捗データ取得
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="process">工程コード</param>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/24
        /// </remarks>
        private void GetMgmt(string dpyno, string process)
        {
            DataSet dtSet = null;
            DataBase dataBase = null;
            try
            {
                dataBase = new DataBase();
                List<object> paraList = new List<object>();
                string sqlStr = QueryBuild.GetSappanMgmt(dpyno, ref paraList);


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
                            Sappans = new Register(row, disabled_flg);
                            break;

                        case Constants.PROCESS_SPNKENSA:
                            Sappank = new Register(row, disabled_flg);
                            break;

                        case Constants.PROCESS_HENSHU:
                            switch (row["SUBPROCESS_CD"].ToString())
                            {
                                case Constants.PROCESS_SUB1:
                                    Henshus = new Register(row, disabled_flg);
                                    break;
                                case Constants.PROCESS_SUB2:
                                    Henshuk = new Register(row, disabled_flg);
                                    break;
                                default:
                                    Henshus = new Register(row, disabled_flg);
                                    Henshuk = new Register(row, disabled_flg);
                                    break;
                            }
                            break;                        

                        case Constants.PROCESS_KENSA:
                            Kensa = new Register(row, disabled_flg);
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
        /// 進捗データ登録
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="process">工程</param>
        /// <param name="uid">ユーザーID</param>
        /// <returns>処理成否</returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/24
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

                // サブ１登録
                register = GetTargetRegister(process, Constants.PROCESS_SUB1);
                sqlStr = QueryBuild.RegistSappanMgmt(register, dpyno, process, Constants.PROCESS_SUB1, uid, ref paraList);
                dataBase.CommitData(sqlStr, paraList.ToArray());

                // 編集はサブ２登録
                if (process.Equals(Constants.PROCESS_HENSHU))
                {
                    paraList.Clear();
                    register = GetTargetRegister(process, Constants.PROCESS_SUB2);
                    sqlStr = QueryBuild.RegistSappanMgmt(register, dpyno, process, Constants.PROCESS_SUB2, uid, ref paraList);
                    dataBase.CommitData(sqlStr, paraList.ToArray());
                }

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
        /// 登録対象取得
        /// </summary>
        /// <param name="process">工程コード</param>
        /// <param name="sub">サブ工程コード</param>
        /// <returns>対象のRegister</returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/24
        /// </remarks>
        private Register GetTargetRegister(string process, string sub)
        {
            Register register = null;
            switch (process)
            {
                case Constants.PROCESS_SPNSEIZO:
                    Sappans.WorkTimeFrom = Utilities.CheckWorkTime(Sappans.WorkTimeFrom, Sappans.CommitDate);
                    register = Sappans;
                    break;

                case Constants.PROCESS_SPNKENSA:
                    Sappank.WorkTimeFrom = Utilities.CheckWorkTime(Sappank.WorkTimeFrom, Sappank.CommitDate);
                    register = Sappank;
                    break;

                case Constants.PROCESS_HENSHU:
                    switch (sub)
                    {
                        case Constants.PROCESS_SUB1:
                            Henshus.WorkTimeFrom = Utilities.CheckWorkTime(Henshus.WorkTimeFrom, Henshus.CommitDate);
                            register = Henshus;
                            break;
                        case Constants.PROCESS_SUB2:
                            Henshuk.WorkTimeFrom = Utilities.CheckWorkTime(Henshuk.WorkTimeFrom, Henshuk.CommitDate);
                            register = Henshuk;
                            break;
                    }
                    break;

                case Constants.PROCESS_KENSA:
                    Kensa.WorkTimeFrom = Utilities.CheckWorkTime(Kensa.WorkTimeFrom, Kensa.CommitDate);
                    register = Kensa;
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