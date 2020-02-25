using Oracle.DataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace PROGMGMT.Common
{
    public class DataBase
    {
        private DbProviderFactory factory;
        private DbConnectionStringBuilder ocsb;
        private DbConnection conn;
        private DbTransaction sqlTran;

        log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataBase()
        {
            factory =
               DbProviderFactories.GetFactory("Oracle.DataAccess.Client");

            ocsb = this.factory.CreateConnectionStringBuilder();
        }
        //----------------------------------------------------------------------
        //Databaseへの接続
        //----------------------------------------------------------------------
        public void ConnectDB()
        {
            try
            {
                DBConnInfo aConnInfo = null;
                aConnInfo = new DBConnInfo();

                this.ocsb["Data Source"] = aConnInfo._DataSource;
                this.ocsb["User ID"] = aConnInfo._UserId;
                this.ocsb["Password"] = aConnInfo._Password;
                this.conn = this.factory.CreateConnection();
                this.conn.ConnectionString = this.ocsb.ConnectionString;
                //データベース接続
                this.conn.Open();
            }
            catch (Exception ex)
            {
                logger.Debug("ConnectDB :  " + ex.ToString());
                conn.Dispose();
                ex.ToString();
            }
        }
        //----------------------------------------------------------------------
        //Databaseへの接続解除
        //----------------------------------------------------------------------
        public void DisconnectDB()
        {
            try
            {
                if (this.conn != null)
                {
                    this.conn.Close();
                    this.conn.Dispose();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        //----------------------------------------------------------------------
        //ﾄﾗﾝｻﾞｸｼｮﾝの開始
        //----------------------------------------------------------------------
        public void BeginTransaction()
        {
            try
            {
                //_Tran = this.conn.BeginTransaction();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        //----------------------------------------------------------------------
        //ﾄﾗﾝｻﾞｸｼｮﾝのｺﾐｯﾄ
        //----------------------------------------------------------------------
        public void TransactionCommit()
        {
            try
            {
                //_Tran.Commit();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                //_Tran.Dispose();
            }
        }
        //----------------------------------------------------------------------
        //ﾄﾗﾝｻﾞｸｼｮﾝのﾛｰﾙﾊﾞｯｸ
        //----------------------------------------------------------------------
        public void TransactionRollback()
        {
            try
            {
                // _Tran.Rollback();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                //_Tran.Dispose();
            }
        }

        /// <summary>
        /// 行を返さないSQL文またはコマンドを実行
        /// </summary>
        /// <param name="aSqlString">SQL文</param>
        /// <param name="aArrPara">パラメータ</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string aSqlString, Object[] aArrPara)
        {
            int rowCnt = 0;
            OracleCommand objCmd = null;
            try
            {
                objCmd = new OracleCommand(aSqlString, null)
                {
                    BindByName = false
                };
                if (aArrPara != null)
                {
                    OracleParameter oraPara;
                    foreach (Object para in aArrPara)
                    {
                        oraPara = new OracleParameter();
                        oraPara.Value = para;
                        objCmd.Parameters.Add(oraPara);
                    }
                }
                //SQL実行
                rowCnt = objCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (objCmd != null)
                {
                    objCmd.Dispose();
                }
            }

            //行数を返す
            return rowCnt;
        }

        /// <summary>
        /// １項目の取得
        /// </summary>
        /// <param name="aSqlString">SQL文</param>
        /// <param name="aArrPara">パラメータ</param>
        /// <returns></returns>
        public Object ExecuteScalar(string aSqlString, Object[] aArrPara)
        {
            Object objRet = null;
            OracleCommand objCmd = null;
            try
            {
                objCmd = new OracleCommand(aSqlString, null)
                {
                    BindByName = false
                };
                if (aArrPara != null)
                {
                    OracleParameter oraPara;
                    foreach (Object para in aArrPara)
                    {
                        oraPara = new OracleParameter();
                        oraPara.Value = para;
                        objCmd.Parameters.Add(oraPara);
                    }
                }
                //SQL実行
                objRet = objCmd.ExecuteScalar();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                if (objCmd != null)
                {
                    objCmd.Dispose();
                }
            }

            //取得項目を返す
            return objRet;
        }

        /// <summary>
        /// SELECTの実行
        /// </summary>
        /// <param name="aSqlString">SQL文</param>
        /// <param name="aArrPara">パラメータ</param>
        /// <returns></returns>
        public DataSet GetDataSet(string aSqlString, Object[] aArrPara)
        {
            DataSet objDs = null;
            try
            {

                DbCommand cmd = this.conn.CreateCommand();
                if (aArrPara != null)
                {
                    DbParameter oraPara;
                    foreach (Object para in aArrPara)
                    {
                        oraPara = cmd.CreateParameter();
                        oraPara.Value = para;
                        cmd.Parameters.Add(oraPara);
                        logger.Debug("SQL: GetDataSet　パラメータ  " +  oraPara.Value); 

                    }
                }
                logger.Debug("SQL:GetDataSet  String:  " + aSqlString);
                cmd.CommandText = aSqlString;
                DbDataAdapter da = this.factory.CreateDataAdapter();
                da.SelectCommand = cmd;
                objDs = new DataSet();
                da.Fill(objDs);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                logger.Debug("GetDataSet :  " + ex.ToString());
                ex.ToString();
                throw ex;
            }
            finally
            {
                if (objDs != null)
                {
                    objDs.Dispose();
                }
            }
            //データセットを返す
            return objDs;
        }

        /// <summary>
        /// 行を返さないSQL文またはコマンドを実行（コミットまで）
        /// </summary>
        /// <param name="aSqlString">SQL文</param>
        /// <param name="aArrPara">パラメータ</param>
        /// <returns></returns>
        public int CommitData(string aSqlString, Object[] aArrPara)
        {

            int rowsUpdated = 0;
            try
            {

                this.sqlTran = this.conn.BeginTransaction();
                DbCommand cmd = this.conn.CreateCommand();

                if (aArrPara != null)
                {
                    DbParameter oraPara;
                    foreach (Object para in aArrPara)
                    {
                        oraPara = cmd.CreateParameter();
                        oraPara.Value = para;
                        cmd.Parameters.Add(oraPara);
                        logger.Debug("SQL: CommitData　パラメータ  " + oraPara.Value);
                    }
                }
                logger.Debug("SQL:CommitData  String:  " + aSqlString);
                cmd.CommandText = aSqlString;
                rowsUpdated = cmd.ExecuteNonQuery();
                this.sqlTran.Commit();
                this.sqlTran.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                logger.Debug("CommitData :  " + ex.ToString());
                ex.ToString();
                this.sqlTran.Rollback();
                throw ex;
            }
            finally
            {
                this.sqlTran.Dispose();
            }

            return rowsUpdated;
        }


        class DBConnInfo
        {
            public string _UserId = ConfigurationManager.ConnectionStrings["UserId"].ConnectionString;
            public string _Password = ConfigurationManager.ConnectionStrings["PassWord"].ConnectionString;
            public string _DataSource = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;
            public long _ConnTimeout = 15;

        }
    }
}