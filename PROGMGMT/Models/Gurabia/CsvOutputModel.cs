using PROGMGMT.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PROGMGMT.Models.Gurabia
{
    /// <summary>
    /// CSV出力画面モデル
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/11/28
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
        /// CSV出力データ取得
        /// </summary>
        /// <returns>取得処理成否</returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/28
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
                List<object> paraList = new List<object>();

                // 得意先：東罐興業
                if (GurabiaCustomer.IsTokan(Condition.Customer))
                {
                    string queryStr = QueryBuild.GetGurabiaCsvTokan(Condition, ref paraList);

                    dataBase = new DataBase();
                    dataBase.ConnectDB();

                    dtSet = dataBase.GetDataSet(queryStr, paraList.ToArray()); // クエリ実行
                    DataTable table = dtSet.Tables[0];

                    CsvData = Utilities.DataTableToCsv(table);

                    dataBase.DisconnectDB();

                    CsvName = Utilities.GetCsvFileName(Resources.TextResource.ProgressGurabiaTokan);

                    return true;
                }

                // 得意先：東洋製罐
                else
                {
                    string queryStr = QueryBuild.GetGurabiaCsvToyo(Condition, ref paraList);

                    dataBase = new DataBase();
                    dataBase.ConnectDB();

                    dtSet = dataBase.GetDataSet(queryStr, paraList.ToArray()); // クエリ実行
                    DataTable table = dtSet.Tables[0];

                    CsvData = Utilities.DataTableToCsv(table);

                    dataBase.DisconnectDB();

                    CsvName = Utilities.GetCsvFileName(Resources.TextResource.ProgressGurabiaToyo);

                    return true;
                }
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
