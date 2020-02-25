using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace PROGMGMT.Models.Jushihan
{
    /// <summary>
    /// CSV出力画面モデル
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/11/06
    /// </remarks> 
    public class CsvOutputModel
    {
        #region プロパティ
        public Condition Condition { get; set; }

        public string CsvName { get; set; }
        public byte[] CsvData { get; set; }

        public string CsvErrorMessage { get; set; }
        #endregion

        #region コンストラクタ
        public CsvOutputModel()
        {
            Condition = new Condition();
        }
        public CsvOutputModel(Condition con)
        {
            Condition = con;
        }
        #endregion

        #region メソッド
        /// <summary>
        /// CSVデータ取得
        /// </summary>
        /// <returns>取得処理成否</returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/06
        /// </remarks> 
        public bool GetCsvData()
        {
            // 出力条件が正しければCSVデータ取得
            if (Condition.ValidateOutput() == false)
            {
                return false;
            }

            DataSet dtSet = null;
            DataBase dataBase = null;
            try
            {
                dataBase = new DataBase();
                List<object> paraList = new List<object>();
                string queryStr = QueryBuild.GetJushihanCsv(Condition, ref paraList);

                dataBase.ConnectDB();

                dtSet = dataBase.GetDataSet(queryStr, paraList.ToArray()); // クエリ実行
                DataTable table = dtSet.Tables[0];

                CsvData = Utilities.DataTableToCsv(table);

                dataBase.DisconnectDB();

                CsvName = Utilities.GetCsvFileName(Resources.TextResource.ProgressJushihan);

                return true;
            }
            catch (Exception ex)
            {
                CsvErrorMessage = Resources.TextResource.ErrorOutputCsv;
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
        }
        #endregion
    }
}
