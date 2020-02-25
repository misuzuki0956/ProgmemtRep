using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PROGMGMT.Models.User
{
    /// <summary>
    /// ログイン情報クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/10/30
    /// </remarks>
    public class LoginUser
    {

        #region プロパティ
        public string EMPLOYEE_CD { get; set; }
        public string PROCESS_CD { get; set; }
        public string USER_PASS { get; set; }

        #endregion

        #region コンストラクタ
        public LoginUser()
        {
        }
        #endregion

        #region メソッド
        /// <summary>
        /// ログイン情報取得
        /// </summary>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/10/30
        /// </remarks> 
        public DataRow GetLoginUser(Condition condition)
        {
            DataTable dtTable;
            DataRow dtRow = null;

            List<object> paraList = new List<object>();
            string queryStr = QueryBuild.SystemLogin(condition, ref paraList);

            DataBase dataBase = new DataBase();
            dataBase.ConnectDB();

            DataSet dtSet = dataBase.GetDataSet(queryStr, paraList.ToArray());
            dtTable = dtSet.Tables[0];

            if (dtTable.Rows.Count != 0)
            {
                dtRow = dtTable.Rows[0];
            }

            if (dataBase != null)
            {
                dataBase.DisconnectDB();
            }
            if (dtSet != null)
            {
                dtSet.Dispose();
            }

            return dtRow;
        }
        #endregion
    }
}