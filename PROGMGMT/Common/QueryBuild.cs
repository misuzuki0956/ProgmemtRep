using System.Collections.Generic;
using System.Text;

namespace PROGMGMT.Common
{
    /// <summary>
    /// クエリ作成クラス
    /// </summary>
    public class QueryBuild
    {
        #region 選択リスト用

        /// <summary>
        /// 条件 工程取得クエリ
        /// </summary>
        /// <returns>取得クエリ</returns>
        /// <param name="product">工程コード</param>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string GetProcess(string product)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("PR.PROCESS_CD,");
            query.AppendLine("PR.PROCESS_NM");
            query.AppendLine("FROM PROGPROCESS_MST PR");
            query.AppendLine("INNER JOIN PROGPRODUCT_BELONG_MST BE ON PR.PROCESS_CD = BE.PROCESS_CD");
            query.Append("WHERE BE.PRODUCT_CD = '").Append(product).AppendLine("'");
            query.AppendLine("ORDER BY PR.PROCESS_CD");

            return query.ToString();
        }

        /// <summary>
        /// 条件 校正区分取得クエリ
        /// </summary>
        /// <returns>取得クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string GetKoseiKubun()
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("KS.KSKBN_CD,");
            query.AppendLine("KS.KSKBN_NM");
            query.AppendLine("FROM KSKBN_MST KS");
            query.AppendLine("WHERE KS.DISPLAY_FLG = '0'");
            query.AppendLine("order by KS.KSKBN_CD");

            return query.ToString();
        }

        /// <summary>
        /// 条件 東罐興業校正区分取得クエリ
        /// </summary>
        /// <returns>取得クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/12/06
        /// </remarks>
        public static string GetTokanKoseiKubun()
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("KS.KSKBN_CD,");
            query.AppendLine("KS.KSKBN_NM");
            query.AppendLine("FROM TOKAN_KSKBN_MST KS");
            query.AppendLine("WHERE KS.DISPLAY_FLG = '0'");
            query.AppendLine("order by KS.KSKBN_CD");

            return query.ToString();
        }

        /// <summary>
        /// 条件 版式取得クエリ
        /// </summary>
        /// <returns>取得クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string GetHanType()
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("HS.HAN_KBN,");
            query.AppendLine("HS.HAN_KBN || ': ' || HS.HANSKI_NM AS HANSKI_NM");
            query.AppendLine("FROM HANSKI_MST HS");
            query.AppendLine("WHERE HS.DISPLAY_FLG = '0'");
            query.AppendLine("ORDER BY HS.HAN_KBN");

            return query.ToString();
        }

        /// <summary>
        /// 条件 版式取得クエリ(樹脂版用)
        /// </summary>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/11
        /// </remarks>
        public static string GetHanTypeJushi()
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("JHS.HANSKI_CD,");
            query.AppendLine("JHS.HANSKI_CD || ': ' || JHS.HANSKI_NM AS HANSKI_NM");
            query.AppendLine("FROM JSBHANSKI_MST JHS");
            query.AppendLine("order by JHS.HANSKI_CD");

            return query.ToString();
        }

        /// <summary>
        /// 登録画面 担当者取得クエリ
        /// </summary>
        /// <returns>取得クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string GetEmployee()
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("EMPLOYEE_CD,");
            query.AppendLine("EMPLOYEE_NM");
            query.AppendLine("FROM PROGEMPLOYEE_MST");
            query.AppendLine("ORDER BY EMPLOYEE_CD");

            return query.ToString();
        }

        /// <summary>
        /// 出力工場取得クエリ
        /// </summary>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/19
        /// </remarks>
        public static string GetFactory()
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("FACTORY_CD,");
            query.AppendLine("FACTORY_NAME");
            query.AppendLine("FROM FACTORY_MST");
            query.AppendLine("ORDER BY FACTORY_CD");

            return query.ToString();
        }

        /// <summary>
        /// 注文内容取得クエリ（刷版用）
        /// </summary>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/19
        /// </remarks>
        public static string GetCmny()
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("CMNY_CD,");
            query.AppendLine("CMNY_NM");
            query.AppendLine("FROM CMNY_MST");
            query.AppendLine("WHERE SAPPAN_FLG = 0");
            query.AppendLine("ORDER BY CMNY_CD");

            return query.ToString();
        }

        #endregion

        #region 製版

        /// <summary>
        /// 製版 検索画面 検索クエリ
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <returns>検索クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string GetSeihanSearch(Models.Seihan.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");
            query.AppendLine("NVL2(PLAN.WORK_SHIJI,'＊','') AS MARK,");      //　指示マーク
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY,'YYYY/MM/DD') AS YOTEI_DAY,");     //　予定日
            query.AppendLine("CMNY.CMNY_NM,");      // 注文内容
            query.AppendLine("CMKBN.CMKBN_NM,");    // 注文区分
            query.AppendLine("SHODR.DPY_NO,");      // 呼出し№
            query.AppendLine("SHODR.SUBNEGA_NO,");  // サブネガNo
            query.AppendLine("THNG.BURANDO_NM,");   // ブランド
            query.AppendLine("THNG.SYOHIN_NM,");    // 品名
            query.AppendLine("THNG.KAN_NM,");       // 容器型
            query.AppendLine("THNG.ZUMEN_NO,");     // 図面No
            query.AppendLine("THNG.HAN_KBN,");      // 版式
            query.AppendLine("PLAN.WORK_SHIJI,");   // 作業指示
            query.AppendLine("KSKBN.KSKBN_NM,");    // 校正区分
            query.AppendLine("THNG.KOSEIZAI,");     // 校正材コード
            query.AppendLine("THNG.CLRKZ,");        // 色数
            query.AppendLine("SHODR.GOKEI_CT,");    // 部数

            query.AppendLine("TO_CHAR(MGMT_HAN.COMMIT_DATE,'YYYY/MM/DD')  AS COMMIT_DATE_HAN,");        // 版下完了日

            query.AppendLine("TO_CHAR(MGMT_HEN1.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_HEN1,");       // 編集編集完了日
            query.AppendLine("TO_CHAR(MGMT_HEN2.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_HEN2,");       // 編集検査完了日
            query.AppendLine("NVL2(MGMT_HEN1.COMMIT_DATE,'レ','') AS HEN_CHK1,");                       // 編集編集完了チェック
            query.AppendLine("NVL2(MGMT_HEN2.COMMIT_DATE,'レ','') AS HEN_CHK2,");                       // 編集検査完了チェック

            query.AppendLine("TO_CHAR(MGMT_KEN1.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_KEN1,");       // 検査完了日1
            query.AppendLine("TO_CHAR(MGMT_KEN2.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_KEN2,");       // 検査完了日2
            query.AppendLine("NVL2(MGMT_KEN1.COMMIT_DATE,'レ','') AS KEN_CHK1,");                       // 検査完了日チェック1
            query.AppendLine("NVL2(MGMT_KEN2.COMMIT_DATE,'レ','') AS KEN_CHK2,");                       // 検査完了日チェック2

            query.AppendLine("TO_CHAR(MGMT_HIRA.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_HIRA,");     // 平板完了日

            query.AppendLine("TO_CHAR(MGMT_KYOK.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_KYOK,");     // 曲面完了日

            query.AppendLine("TO_CHAR(MGMT_KOUSEI.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_KOUSEI,");   // 校正検査完了日

            query.AppendLine("TO_CHAR(MGMT_GYOUM.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_GYOUM");     // 業務完了日

            query.AppendLine("FROM SHODR_TBL SHODR");
            query.AppendLine("INNER JOIN PROGPLAN_SEI_TBL PLAN ON PLAN.DPY_NO = SHODR.DPY_NO");
            query.AppendLine("LEFT OUTER JOIN THNG_MST THNG ON THNG.NGSH_KEY = SHODR.NGSH_KEY");
            query.AppendLine("LEFT OUTER JOIN CMNY_MST CMNY ON CMNY.CMNY_CD = SHODR.CMNY_CD");
            query.AppendLine("LEFT OUTER JOIN CMKBN_MST CMKBN ON CMKBN.CMKBN_CD = SHODR.CMKBN_CD");
            query.AppendLine("LEFT OUTER JOIN KSKBN_MST KSKBN ON KSKBN.KSKBN_CD = SHODR.KSKBN_CD");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL MGMT_HAN    ON    MGMT_HAN.DPY_NO = SHODR.DPY_NO AND    MGMT_HAN.PROCESS_CD = '01' AND    MGMT_HAN.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL MGMT_HEN1   ON   MGMT_HEN1.DPY_NO = SHODR.DPY_NO AND   MGMT_HEN1.PROCESS_CD = '02' AND   MGMT_HEN1.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL MGMT_HEN2   ON   MGMT_HEN2.DPY_NO = SHODR.DPY_NO AND   MGMT_HEN2.PROCESS_CD = '02' AND   MGMT_HEN2.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL MGMT_KEN1   ON   MGMT_KEN1.DPY_NO = SHODR.DPY_NO AND   MGMT_KEN1.PROCESS_CD = '03' AND   MGMT_KEN1.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL MGMT_KEN2   ON   MGMT_KEN2.DPY_NO = SHODR.DPY_NO AND   MGMT_KEN2.PROCESS_CD = '03' AND   MGMT_KEN2.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL MGMT_HIRA   ON   MGMT_HIRA.DPY_NO = SHODR.DPY_NO AND   MGMT_HIRA.PROCESS_CD = '04' AND   MGMT_HIRA.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL MGMT_KYOK   ON   MGMT_KYOK.DPY_NO = SHODR.DPY_NO AND   MGMT_KYOK.PROCESS_CD = '05' AND   MGMT_KYOK.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL MGMT_KOUSEI ON MGMT_KOUSEI.DPY_NO = SHODR.DPY_NO AND MGMT_KOUSEI.PROCESS_CD = '06' AND MGMT_KOUSEI.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL MGMT_GYOUM  ON  MGMT_GYOUM.DPY_NO = SHODR.DPY_NO AND  MGMT_GYOUM.PROCESS_CD = '09' AND  MGMT_GYOUM.SUBPROCESS_CD = '01'");

            // 工程
            query.AppendLine("WHERE PLAN.PROCESS_CD = :0");
            paraList.Add(con.Process);

            // 予定日
            query.AppendLine("AND PLAN.YOTEI_DAY BETWEEN TO_DATE(:1,'RR-MM-DD') AND TO_DATE(:2,'RR-MM-DD')");
            paraList.Add(con.ScheDateFrom);
            if (!string.IsNullOrWhiteSpace(con.ScheDateTo))
            {
                paraList.Add(con.ScheDateTo);
            }
            else
            {
                paraList.Add(con.ScheDateFrom);
            }

            // 校正区分
            if (con.Kosei_Kbn != null)
            {
                query.Append("AND SHODR.KSKBN_CD IN ( ");

                for (int i = 0; i< con.Kosei_Kbn.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Kosei_Kbn[i]);
                }

                query.AppendLine(" ) ");
            }
            // 版式
            if (con.Han_Type != null)
            {
                query.Append("AND THNG.HAN_KBN IN ( ");

                for (int i = 0; i < con.Han_Type.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Han_Type[i]);
                }

                query.AppendLine(" ) ");
            }

            // ｻﾌﾞﾈｶﾞNo
            if (!string.IsNullOrWhiteSpace(con.Nega_No))
            {
                query.Append("AND SHODR.SUBNEGA_NO LIKE '%' || :").Append(paraList.Count).AppendLine(" || '%'");
                paraList.Add(con.Nega_No);
            }

            // 伝票No
            if (!string.IsNullOrWhiteSpace(con.Dpy_No))
            {
                query.Append("AND SHODR.DPY_NO = :").Append(paraList.Count).AppendLine("");
                paraList.Add(con.Dpy_No);
            }

            // 品名
            if (!string.IsNullOrWhiteSpace(con.Shohin_Name))
            {
                query.Append("AND THNG.SYOHIN_NM LIKE '%' || :").Append(paraList.Count).AppendLine(" || '%'");
                paraList.Add(con.Shohin_Name);
            }

            query.AppendLine("ORDER BY PLAN.YOTEI_DAY,SHODR.SUBNEGA_NO");

            return query.ToString();
        }

        /// <summary>
        /// 製版 登録画面 ヘッダ取得クエリ
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="paraList">パラメータリスト</param>
        /// <returns>取得クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string GetSeihanHeader(string dpyno, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");
            query.AppendLine("SHODR.SUBNEGA_NO,");  // サブネガNo
            query.AppendLine("CMNY.CMNY_NM,");      // 注文内容
            query.AppendLine("CMKBN.CMKBN_NM,");    // 注文区分
            query.AppendLine("THNG.BURANDO_NM,");   // ブランド
            query.AppendLine("THNG.SYOHIN_NM,");    // 品名
            query.AppendLine("THNG.KAN_NM,");      // 容器型
            query.AppendLine("THNG.ZUMEN_NO,");     // 図面No
            query.AppendLine("THNG.HAN_KBN,");      // 版式
            query.AppendLine("( SELECT LISTAGG( CLR.COLOR_KJ, ' ') WITHIN GROUP (ORDER BY SHCLR.EDA_NO)");
            query.AppendLine("  FROM SHCLR_TBL SHCLR INNER JOIN COLOR_MST CLR ON CLR.COLOR_CD = SHCLR.COLOR_CD");
            query.AppendLine("  WHERE SHCLR.DPY_NO = SHODR.DPY_NO AND SHCLR.DEL_FLG = '0'");
            query.AppendLine(") AS COLOR_NM,");     // 色名漢字
            query.AppendLine("KSKBN.KSKBN_NM,");    // 校正区分
            query.AppendLine("THNG.KOSEIZAI,");     // 校正材
            query.AppendLine("SHODR.GOKEI_CT");     // 校正刷部数
            query.AppendLine("FROM SHODR_TBL SHODR");
            query.AppendLine("LEFT OUTER JOIN THNG_MST THNG ON THNG.NGSH_KEY = SHODR.NGSH_KEY");
            query.AppendLine("LEFT OUTER JOIN CMNY_MST CMNY ON CMNY.CMNY_CD = SHODR.CMNY_CD");
            query.AppendLine("LEFT OUTER JOIN CMKBN_MST CMKBN ON CMKBN.CMKBN_CD = SHODR.CMKBN_CD");
            query.AppendLine("LEFT OUTER JOIN KSKBN_MST KSKBN ON KSKBN.KSKBN_CD = SHODR.KSKBN_CD");
            query.AppendLine("WHERE SHODR.DPY_NO = :0");
            paraList.Add(dpyno);

            return query.ToString();
        }

        /// <summary>
        /// 製版 登録画面 進捗データ取得クエリ
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="paraList">パラメータリスト</param>
        /// <returns>取得クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string GetSeihanMgmt(string dpyno, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("PLAN.DPY_NO,");           //  呼出し№
            query.AppendLine("PLAN.PROCESS_CD,");       //  工程コード
            query.AppendLine("MGMT.SUBPROCESS_CD,");    //  サブ工程コード
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY, 'YYYY-MM-DD') AS YOTEI_DAY,");        //  予定日
            query.AppendLine("TO_CHAR(MGMT.COMMIT_DATE, 'YYYY-MM-DD') AS COMMIT_DATE,");    //  作業日
            query.AppendLine("MGMT.EMPLOYEE_CD,");      //  担当者コード
            query.AppendLine("EMP.EMPLOYEE_NM,");       //  担当者名
            query.AppendLine("MGMT.WORKTIME_FROM,");    //  作業時間From
            query.AppendLine("MGMT.WORKTIME_TO,");      //  作業時間To
            query.AppendLine("MGMT.WORK_MEMO,");        //  作業メモ
            query.AppendLine("MGMT.KOSEI_LINE,");       //  校正ライン
            query.AppendLine("MGMT.MEMO");              //  メモ
            query.AppendLine("FROM PROGPLAN_SEI_TBL PLAN");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL MGMT ON MGMT.DPY_NO = PLAN.DPY_NO AND MGMT.PROCESS_CD = PLAN.PROCESS_CD");
            query.AppendLine("LEFT OUTER JOIN PROGEMPLOYEE_MST EMP ON EMP.EMPLOYEE_CD = MGMT.EMPLOYEE_CD");
            query.AppendLine("WHERE PLAN.DPY_NO = :0");
            paraList.Add(dpyno);

            query.AppendLine("ORDER BY PLAN.PROCESS_CD, MGMT.SUBPROCESS_CD");

            return query.ToString();

        }

        /// <summary>
        /// 製版 登録画面 製版進捗管理テーブル登録クエリ
        /// </summary>
        /// <param name="register">登録情報</param>
        /// <param name="dpyno">伝票No</param>
        /// <param name="process">工程コード</param>
        /// <param name="sub">サブ工程コード</param>
        /// <param name="uid">ユーザーID</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <returns>登録クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string RegistSeihanMgmt(Models.Seihan.Register register, string dpyno, string process, string sub, string uid, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            // MERGE
            query.AppendLine("MERGE INTO PROGMGMT_SEI_TBL MGMT");
            query.AppendLine("USING (");
            query.AppendLine("  SELECT");

            query.AppendLine("  :0 AS DPY_NO,");
            paraList.Add(dpyno);

            query.AppendLine("  :1 AS PROCESS_CD,");
            paraList.Add(process);

            query.AppendLine("  :2 AS SUBPROCESS_CD,");
            paraList.Add(sub);

            query.AppendLine("  TO_DATE(:3,'RR-MM-DD') AS COMMIT_DATE,");
            paraList.Add(register.CommitDate);

            query.AppendLine("  :4 AS EMPLOYEE_CD,");
            paraList.Add(register.EmployeeCd);

            query.AppendLine("  :5 AS WORKTIME_FROM,");
            paraList.Add(register.WorkTimeFrom);

            query.AppendLine("  :6 AS WORKTIME_TO,");
            paraList.Add(register.WorkTimeTo);

            query.AppendLine("  :7 AS WORK_MEMO,");
            paraList.Add(register.WorkMemo);

            query.AppendLine("  :8 AS KOSEI_LINE,");
            paraList.Add(register.KoseiLine);

            query.AppendLine("  :9 AS MEMO,");
            paraList.Add(register.Memo);

            query.AppendLine("  :10 AS ID,");
            paraList.Add(uid);

            query.AppendLine("  'SEIHAN' AS PG,");
            query.AppendLine("  TO_DATE(SYSDATE,'RR-MM-DD') AS DT");
            query.AppendLine("  FROM DUAL");
            query.AppendLine(") TMP");

            // 条件
            query.AppendLine("ON (");
            query.AppendLine("  MGMT.DPY_NO = TMP.DPY_NO");
            query.AppendLine("  AND MGMT.PROCESS_CD = TMP.PROCESS_CD");
            query.AppendLine("  AND MGMT.SUBPROCESS_CD = TMP.SUBPROCESS_CD");
            query.AppendLine(")");

            // 条件に一致するレコードがあれば更新
            query.AppendLine("WHEN MATCHED THEN");
            query.AppendLine("UPDATE SET");
            query.AppendLine("  MGMT.COMMIT_DATE   = TMP.COMMIT_DATE,");
            query.AppendLine("  MGMT.EMPLOYEE_CD   = TMP.EMPLOYEE_CD,");
            query.AppendLine("  MGMT.WORKTIME_FROM = TMP.WORKTIME_FROM,");
            query.AppendLine("  MGMT.WORKTIME_TO   = TMP.WORKTIME_TO,");
            query.AppendLine("  MGMT.WORK_MEMO     = TMP.WORK_MEMO,");
            query.AppendLine("  MGMT.KOSEI_LINE    = TMP.KOSEI_LINE,");
            query.AppendLine("  MGMT.MEMO          = TMP.MEMO,");
            query.AppendLine("  MGMT.UPDT_DT       = TMP.DT,");
            query.AppendLine("  MGMT.UPDT_USERID   = TMP.ID,");
            query.AppendLine("  MGMT.UPDT_PG       = TMP.PG");

            // 条件に一致するレコードがなければ追加
            query.AppendLine("WHEN NOT MATCHED THEN");
            query.AppendLine("INSERT VALUES (");
            query.AppendLine("  TMP.DPY_NO,");
            query.AppendLine("  TMP.PROCESS_CD,");
            query.AppendLine("  TMP.SUBPROCESS_CD,");
            query.AppendLine("  TMP.COMMIT_DATE,");
            query.AppendLine("  TMP.EMPLOYEE_CD,");
            query.AppendLine("  TMP.WORKTIME_FROM,");
            query.AppendLine("  TMP.WORKTIME_TO,");
            query.AppendLine("  TMP.WORK_MEMO,");
            query.AppendLine("  TMP.KOSEI_LINE,");
            query.AppendLine("  TMP.MEMO,");
            query.AppendLine("  TMP.DT, TMP.ID, TMP.PG,");
            query.AppendLine("  TMP.DT, TMP.ID, TMP.PG");
            query.AppendLine(")");

            return query.ToString();
        }

        /// <summary>
        /// 製版 CSV出力画面 CSVデータ取得クエリ
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <returns>取得クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string GetSeihanCsv(Models.Seihan.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("SHODR.SUBNEGA_NO AS \"サブネガNo\",");
            query.AppendLine("SHODR.DPY_NO AS \"呼出し№\",");
            query.AppendLine("CMNY.CMNY_NM AS \"注文内容\",");
            query.AppendLine("CMKBN.CMKBN_NM AS \"注文区分\",");
            query.AppendLine("THNG.BURANDO_NM AS \"ブランド\",");
            query.AppendLine("THNG.SYOHIN_NM AS \"品名\",");
            query.AppendLine("THNG.KAN_NM AS \"容器型\",");
            query.AppendLine("THNG.ZUMEN_NO AS \"図面No\",");
            query.AppendLine("THNG.HAN_KBN AS \"版式\",");
            query.AppendLine("KSKBN.KSKBN_NM AS \"校正区分\",");
            query.AppendLine("THNG.KOSEIZAI AS \"校正材料コード\",");
            query.AppendLine("THNG.GOKEI_CT AS \"校正刷部数\",");
            query.AppendLine("( SELECT LISTAGG( CLR.COLOR_KJ, ' ') WITHIN GROUP (ORDER BY SHCLR.EDA_NO)");
            query.AppendLine("  FROM SHCLR_TBL SHCLR INNER JOIN COLOR_MST CLR ON CLR.COLOR_CD = SHCLR.COLOR_CD");
            query.AppendLine("  WHERE SHCLR.DPY_NO = SHODR.DPY_NO AND SHCLR.DEL_FLG = '0'");
            query.AppendLine(") AS \"色名\",");
            query.AppendLine("PROC.PROCESS_NM AS \"工程\",");
            query.AppendLine("CASE MGMT.PROCESS_CD || MGMT.SUBPROCESS_CD");
            query.AppendLine("    WHEN '0201' THEN '編集'");
            query.AppendLine("    WHEN '0202' THEN '検査'");
            query.AppendLine("    WHEN '0301' THEN '工程1'");
            query.AppendLine("    WHEN '0302' THEN '工程2'");
            query.AppendLine("    ELSE ''");
            query.AppendLine("END AS \"サブ工程\",");
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY,'YYYY/MM/DD') AS \"予定日\",");
            query.AppendLine("TO_CHAR(MGMT.COMMIT_DATE,'YYYY/MM/DD') AS \"作業日\",");
            query.AppendLine("EMP.EMPLOYEE_NM AS \"担当者名\",");
            query.AppendLine("MGMT.WORKTIME_FROM AS \"作業時間(From)\",");
            query.AppendLine("MGMT.WORKTIME_TO AS \"作業時間(To)\",");
            query.AppendLine("MGMT.WORK_MEMO AS \"作業メモ\",");
            query.AppendLine("MGMT.MEMO AS \"メモ\",");
            query.AppendLine("MGMT.KOSEI_LINE AS \"校正ライン\"");

            query.AppendLine("FROM SHODR_TBL SHODR");                                                   // 製版注文テーブル
            query.AppendLine("INNER JOIN PROGPLAN_SEI_TBL PLAN ON PLAN.DPY_NO = SHODR.DPY_NO");         // 製版工程予定テーブル
            query.AppendLine("INNER JOIN PROGMGMT_SEI_TBL MGMT ON MGMT.DPY_NO = SHODR.DPY_NO AND MGMT.PROCESS_CD = PLAN.PROCESS_CD");   // 製版進捗管理テーブル
            query.AppendLine("LEFT OUTER JOIN THNG_MST THNG ON THNG.NGSH_KEY = SHODR.NGSH_KEY");             // 東版ネガマスタ
            query.AppendLine("LEFT OUTER JOIN CMNY_MST CMNY ON CMNY.CMNY_CD = SHODR.CMNY_CD");               // 注文内容マスタ
            query.AppendLine("LEFT OUTER JOIN CMKBN_MST CMKBN ON CMKBN.CMKBN_CD = SHODR.CMKBN_CD");          // 注文区分マスタ
            query.AppendLine("LEFT OUTER JOIN KSKBN_MST KSKBN ON KSKBN.KSKBN_CD = SHODR.KSKBN_CD");          // 校正区分マスタ
            query.AppendLine("LEFT OUTER JOIN PROGPROCESS_MST PROC ON PROC.PROCESS_CD = MGMT.PROCESS_CD");   // 進捗管理工程マスタ
            query.AppendLine("LEFT OUTER JOIN PROGEMPLOYEE_MST EMP ON EMP.EMPLOYEE_CD = MGMT.EMPLOYEE_CD");  // 進捗管理担当者マスタ

            // 作業日
            query.AppendLine("WHERE MGMT.COMMIT_DATE BETWEEN TO_DATE(:0,'RR-MM-DD') AND TO_DATE(:1,'RR-MM-DD')");
            paraList.Add(con.OutputDateFrom);
            if (!string.IsNullOrWhiteSpace(con.OutputDateTo))
            {
                paraList.Add(con.OutputDateTo);
            }
            else
            {
                paraList.Add(con.OutputDateFrom);
            }

            // 工程
            if (con.OutputProcess != null)
            {
                query.AppendLine("AND PLAN.PROCESS_CD IN ( ");
                for (int i = 0; i < con.OutputProcess.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.OutputProcess[i]);
                }
                query.AppendLine(" ) ");
            }

            // 校正区分
            if (con.Kosei_Kbn != null)
            {
                query.Append("AND SHODR.KSKBN_CD IN ( ");

                for (int i = 0; i < con.Kosei_Kbn.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Kosei_Kbn[i]);
                }

                query.AppendLine(" ) ");
            }
            // 版式
            if (con.Han_Type != null)
            {
                query.Append("AND THNG.HAN_KBN IN ( ");

                for (int i = 0; i < con.Han_Type.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Han_Type[i]);
                }

                query.AppendLine(" ) ");
            }

            query.AppendLine("ORDER BY SHODR.DPY_NO, SHODR.SUBNEGA_NO, MGMT.PROCESS_CD, MGMT.SUBPROCESS_CD, MGMT.COMMIT_DATE");

            return query.ToString();

        }

        #endregion

        #region グラビア

        /// <summary>
        /// グラビア 検索画面 検索クエリ 東洋製罐
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/13
        /// </remarks>
        public static string GetGurabiaSearchToyo(Models.Gurabia.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");
            query.AppendLine("NVL2(PLAN.WORK_SHIJI,'＊','') AS MARK,");                 // 作業指示マーク
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY,'YYYY/MM/DD') AS YOTEI_DAY,");     // 予定日
            query.AppendLine("CMNY.CMNY_NM,");           // 注文内容
            query.AppendLine("CMKBN.CMKBN_NM,");         // 注文区分
            query.AppendLine("SHODR.DPY_NO,");           // 呼出しNo
            query.AppendLine("SHODR.SUBNEGA_NO,");       // ｻﾌﾞﾈｶﾞNo
            query.AppendLine("THGNG.BURANDO_NM,");       // ブランド
            query.AppendLine("THGNG.SYOHIN_NM,");        // 品名
            query.AppendLine("THGNG.KAN_NM,");           // 容器型（規格）
            query.AppendLine("KSKBN.KSKBN_NM,");         // 校正区分
            query.AppendLine("PLAN.WORK_SHIJI,");        // 作業指示（発送指示）

            query.AppendLine("TO_CHAR(MGMT_HAN.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_HAN,");           // 版下完了日

            query.AppendLine("TO_CHAR(MGMT_HEN1.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_HEN1,");         // 編集編集完了日
            query.AppendLine("TO_CHAR(MGMT_HEN2.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_HEN2,");         // 編集検査完了日
            query.AppendLine("NVL2(MGMT_HEN1.COMMIT_DATE,'レ','') AS HEN_CHK1,");                         // 編集編集完了チェック
            query.AppendLine("NVL2(MGMT_HEN2.COMMIT_DATE,'レ','') AS HEN_CHK2,");                         // 編集検査完了チェック
            query.AppendLine("TO_CHAR(SHODR.DENSOU_DAY,'YYYY/MM/DD') AS DENSOU_DAY,");                    // 電送予定日
            query.AppendLine("CASE WHEN (SHODR.DENSOU_DAY IS NULL) THEN ('―')");
            query.AppendLine("     WHEN (SHODR.DENSOU_DAY IS NOT NULL) THEN (DECODE(MGMT_HEN1.DENSO_FLG,1,'レ',''))");
            query.AppendLine("  END AS DENSO_CHK,");                                                       // 電送

            query.AppendLine("TO_CHAR(MGMT_KEN1.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_KEN1,");         // 検査工程1完了日
            query.AppendLine("TO_CHAR(MGMT_KEN2.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_KEN2,");         // 検査工程2完了日
            query.AppendLine("NVL2(MGMT_KEN1.COMMIT_DATE,'レ','') AS KEN_CHK1,");                         // 検査工程1完了チェック
            query.AppendLine("NVL2(MGMT_KEN2.COMMIT_DATE,'レ','') AS KEN_CHK2,");                         // 検査工程2完了

            query.AppendLine("TO_CHAR(MGMT_GYM.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_GYM");            // 業務課出荷完了日

            query.AppendLine("FROM SHODR_TBL SHODR");
            query.AppendLine("INNER JOIN PROGPLAN_GRA_TBL PLAN ON PLAN.DPY_NO = SHODR.DPY_NO");
            query.AppendLine("LEFT OUTER JOIN CMNY_MST CMNY ON CMNY.CMNY_CD = SHODR.CMNY_CD");
            query.AppendLine("LEFT OUTER JOIN CMKBN_MST CMKBN ON CMKBN.CMKBN_CD = SHODR.CMKBN_CD");
            query.AppendLine("LEFT OUTER JOIN THGNG_MST THGNG ON THGNG.NGSH_KEY = SHODR.NGSH_KEY");
            query.AppendLine("LEFT OUTER JOIN KSKBN_MST KSKBN ON KSKBN.KSKBN_CD = SHODR.KSKBN_CD");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_HAN ON MGMT_HAN.DPY_NO = SHODR.DPY_NO AND MGMT_HAN.PROCESS_CD = '01' AND   MGMT_HAN.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_HEN1 ON MGMT_HEN1.DPY_NO = SHODR.DPY_NO AND MGMT_HEN1.PROCESS_CD = '02' AND MGMT_HEN1.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_HEN2 ON MGMT_HEN2.DPY_NO = SHODR.DPY_NO AND MGMT_HEN2.PROCESS_CD = '02' AND MGMT_HEN2.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_KEN1 ON MGMT_KEN1.DPY_NO = SHODR.DPY_NO AND MGMT_KEN1.PROCESS_CD = '03' AND MGMT_KEN1.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_KEN2 ON MGMT_KEN2.DPY_NO = SHODR.DPY_NO AND MGMT_KEN2.PROCESS_CD = '03' AND MGMT_KEN2.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_GYM ON MGMT_GYM.DPY_NO = SHODR.DPY_NO AND MGMT_GYM.PROCESS_CD = '09' AND MGMT_GYM.SUBPROCESS_CD = '01'");

            // グラビア注文
            query.AppendLine("WHERE SHODR.SHGRVSH_KBN = '2'");

            // 得意先
            query.AppendLine("AND SHODR.CUSTMER_CD <> '9904'");

            // 工程
            query.AppendLine("AND PLAN.PROCESS_CD = :0");
            paraList.Add(con.Process);

            // 予定日
            query.AppendLine("AND PLAN.YOTEI_DAY BETWEEN TO_DATE(:1,'RR-MM-DD') AND TO_DATE(:2,'RR-MM-DD')");
            paraList.Add(con.ScheDateFrom);
            if (!string.IsNullOrWhiteSpace(con.ScheDateTo))
            {
                paraList.Add(con.ScheDateTo);
            }
            else
            {
                paraList.Add(con.ScheDateFrom);
            }

            // 校正区分
            if (con.Kosei_Kbn != null)
            {
                query.Append("AND SHODR.KSKBN_CD IN ( ");

                for (int i = 0; i < con.Kosei_Kbn.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Kosei_Kbn[i]);
                }

                query.AppendLine(" ) ");
            }

            // ｻﾌﾞﾈｶﾞNo
            if (!string.IsNullOrWhiteSpace(con.Nega_No))
            {
                query.Append("AND SHODR.SUBNEGA_NO LIKE '%' || :").Append(paraList.Count).AppendLine(" || '%'");
                paraList.Add(con.Nega_No);
            }

            // 呼出しNo
            if (!string.IsNullOrWhiteSpace(con.Dpy_No))
            {
                query.Append("AND SHODR.DPY_NO = :").Append(paraList.Count).AppendLine("");
                paraList.Add(con.Dpy_No);
            }

            query.AppendLine("ORDER BY PLAN.YOTEI_DAY,SHODR.SUBNEGA_NO");

            return query.ToString();
        }

        /// <summary>
        /// グラビア 検索画面 検索クエリ 東罐興業
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/13
        /// </remarks>
        public static string GetGurabiaSearchTokan(Models.Gurabia.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("NVL2(PLAN.WORK_SHIJI,'＊','') AS MARK,");                 // 作業指示マーク
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY,'YYYY/MM/DD') AS YOTEI_DAY,");     // 予定日
            query.AppendLine("CMNY.CMNY_NM,");           // 注文内容
            query.AppendLine("CMKBN.CMKBN_NM,");         // 注文区分
            query.AppendLine("ODR.DPY_NO,");           // 呼出しNo
            query.AppendLine("ODR.ORDER_NO || '/' || ODR.PRODUCT_CD AS SUBNEGA_NO,");       // ｻﾌﾞﾈｶﾞNo
            query.AppendLine("ODR.BURANDO_NM,");       // ブランド
            query.AppendLine("ODR.SYOHIN_NM,");        // 品名
            query.AppendLine("ODR.KIKAKU AS KAN_NM,"); // 容器型（規格）
            query.AppendLine("KSKBN.KSKBN_NM,");         // 校正区分
            query.AppendLine("PLAN.WORK_SHIJI,");        // 作業指示（発送指示）

            query.AppendLine("TO_CHAR(MGMT_HAN.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_HAN,");           // 版下完了日

            query.AppendLine("TO_CHAR(MGMT_HEN1.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_HEN1,");         // 編集編集完了日
            query.AppendLine("TO_CHAR(MGMT_HEN2.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_HEN2,");         // 編集検査完了日
            query.AppendLine("NVL2(MGMT_HEN1.COMMIT_DATE,'レ','') AS HEN_CHK1,");                         // 編集編集完了チェック
            query.AppendLine("NVL2(MGMT_HEN2.COMMIT_DATE,'レ','') AS HEN_CHK2,");                         // 編集検査完了チェック
            query.AppendLine("TO_CHAR(ODR.DENSOU_DAY,'YYYY/MM/DD') AS DENSOU_DAY,");                    // 電送予定日
            query.AppendLine("CASE WHEN (ODR.DENSOU_DAY IS NULL) THEN ('―')");
            query.AppendLine("     WHEN (ODR.DENSOU_DAY IS NOT NULL) THEN (DECODE(MGMT_HEN1.DENSO_FLG,1,'レ',''))");
            query.AppendLine("  END AS DENSO_CHK,");                                                       // 電送

            query.AppendLine("TO_CHAR(MGMT_KEN1.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_KEN1,");         // 検査工程1完了日
            query.AppendLine("TO_CHAR(MGMT_KEN2.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_KEN2,");         // 検査工程2完了日
            query.AppendLine("NVL2(MGMT_KEN1.COMMIT_DATE,'レ','') AS KEN_CHK1,");                         // 検査工程1完了チェック
            query.AppendLine("NVL2(MGMT_KEN2.COMMIT_DATE,'レ','') AS KEN_CHK2,");                         // 検査工程2完了

            query.AppendLine("TO_CHAR(MGMT_GYM.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_GYM");            // 業務課出荷完了日

            query.AppendLine("FROM TOKANODR_TBL ODR");
            query.AppendLine("INNER JOIN PROGPLAN_GRA_TBL PLAN ON PLAN.DPY_NO = ODR.DPY_NO");
            query.AppendLine("LEFT OUTER JOIN TOKAN_CMNY_MST CMNY ON CMNY.CMNY_CD = ODR.CMNY_CD");
            query.AppendLine("LEFT OUTER JOIN TOKAN_CMKBN_MST CMKBN ON CMKBN.CMKBN_CD = ODR.CMKBN_CD");
            query.AppendLine("LEFT OUTER JOIN TOKAN_KSKBN_MST KSKBN ON KSKBN.KSKBN_CD = ODR.KSKBN_CD");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_HAN ON MGMT_HAN.DPY_NO = ODR.DPY_NO AND MGMT_HAN.PROCESS_CD = '01' AND   MGMT_HAN.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_HEN1 ON MGMT_HEN1.DPY_NO = ODR.DPY_NO AND MGMT_HEN1.PROCESS_CD = '02' AND MGMT_HEN1.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_HEN2 ON MGMT_HEN2.DPY_NO = ODR.DPY_NO AND MGMT_HEN2.PROCESS_CD = '02' AND MGMT_HEN2.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_KEN1 ON MGMT_KEN1.DPY_NO = ODR.DPY_NO AND MGMT_KEN1.PROCESS_CD = '03' AND MGMT_KEN1.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_KEN2 ON MGMT_KEN2.DPY_NO = ODR.DPY_NO AND MGMT_KEN2.PROCESS_CD = '03' AND MGMT_KEN2.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT_GYM ON MGMT_GYM.DPY_NO = ODR.DPY_NO AND MGMT_GYM.PROCESS_CD = '09' AND MGMT_GYM.SUBPROCESS_CD = '01'");

            // 工程
            query.AppendLine("WHERE PLAN.PROCESS_CD = :0");
            paraList.Add(con.Process);

            // 予定日
            query.AppendLine("AND PLAN.YOTEI_DAY BETWEEN TO_DATE(:1,'RR-MM-DD') AND TO_DATE(:2,'RR-MM-DD')");
            paraList.Add(con.ScheDateFrom);
            if (!string.IsNullOrWhiteSpace(con.ScheDateTo))
            {
                paraList.Add(con.ScheDateTo);
            }
            else
            {
                paraList.Add(con.ScheDateFrom);
            }

            // 校正区分
            if (con.Tokan_Kosei_Kbn != null)
            {
                query.Append("AND ODR.KSKBN_CD IN ( ");

                for (int i = 0; i < con.Tokan_Kosei_Kbn.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Tokan_Kosei_Kbn[i]);
                }

                query.AppendLine(" ) ");
            }

            // ｻﾌﾞﾈｶﾞNo
            if (!string.IsNullOrWhiteSpace(con.Nega_No))
            {
                query.Append("AND ODR.ORDER_NO || '/' || ODR.PRODUCT_CD LIKE '%' || :").Append(paraList.Count).AppendLine(" || '%'");
                paraList.Add(con.Nega_No);
            }

            // 呼出しNo
            if (!string.IsNullOrWhiteSpace(con.Dpy_No))
            {
                query.Append("AND ODR.DPY_NO = :").Append(paraList.Count).AppendLine("");
                paraList.Add(con.Dpy_No);
            }

            query.AppendLine("ORDER BY PLAN.YOTEI_DAY,SUBNEGA_NO");

            return query.ToString();
        }

        /// <summary>
        /// グラビア 登録画面 ヘッダ取得クエリ 東洋製罐
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="paraList">パラメータリスト</param>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/21
        /// </remarks>
        public static string GetGurabiaHeaderToyo(string dpyno, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");
            query.AppendLine("CUS.CUSTMER_RNM,");      // 得意先
            query.AppendLine("SHODR.SUBNEGA_NO,");     // ｻﾌﾞﾈｶﾞNo
            query.AppendLine("THGNG.BURANDO_NM,");     // ブランド
            query.AppendLine("THGNG.SYOHIN_NM,");      // 品名
            query.AppendLine("THGNG.KAN_NM,");         // 規格
            query.AppendLine("KSKBN.KSKBN_NM,");       // 校正区分
            query.AppendLine("( SELECT LISTAGG( CLR.COLOR_KJ, ' ') WITHIN GROUP (ORDER BY GRACLR.EDA_NO)");
            query.AppendLine("  FROM GRACLR_TBL GRACLR INNER JOIN COLOR_MST CLR ON CLR.COLOR_CD = GRACLR.COLOR_CD");
            query.AppendLine("  WHERE GRACLR.DPY_NO = SHODR.DPY_NO AND GRACLR.DEL_FLG = '0'");
            query.AppendLine(") AS COLOR_NM");        // 色名漢字
            query.AppendLine("FROM SHODR_TBL SHODR");
            query.AppendLine("LEFT OUTER JOIN THGNG_MST THGNG ON THGNG.NGSH_KEY = SHODR.NGSH_KEY");
            query.AppendLine("LEFT OUTER JOIN KSKBN_MST KSKBN ON KSKBN.KSKBN_CD = SHODR.KSKBN_CD");
            query.AppendLine("LEFT OUTER JOIN CUSTMER_MST CUS ON CUS.CUSTMER_CD = SHODR.CUSTMER_CD");
            query.AppendLine("WHERE SHODR.DPY_NO = :0");
            query.AppendLine("AND SHODR.SHGRVSH_KBN = '2'");

            paraList.Add(dpyno);

            return query.ToString();
        }

        /// <summary>
        /// グラビア 登録画面 ヘッダ取得クエリ 東罐興業
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="paraList">パラメータリスト</param>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/12/06
        /// </remarks>
        public static string GetGurabiaHeaderTokan(string dpyno, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");
            query.AppendLine("CUS.CUSTMER_RNM,");       // 得意先
            query.AppendLine("ODR.ORDER_NO || '/' || ODR.PRODUCT_CD AS SUBNEGA_NO,");     // ｻﾌﾞﾈｶﾞNo
            query.AppendLine("ODR.BURANDO_NM,");        // ブランド
            query.AppendLine("ODR.SYOHIN_NM,");         // 品名
            query.AppendLine("ODR.KIKAKU AS KAN_NM,");  // 規格
            query.AppendLine("KSKBN.KSKBN_NM,");        // 校正区分
            query.AppendLine("( SELECT LISTAGG( CLRM.COLOR_KJ, ' ') WITHIN GROUP (ORDER BY CLR.EDA_NO)");
            query.AppendLine("  FROM TOKANCLR_TBL CLR INNER JOIN COLOR_MST CLRM ON CLRM.COLOR_CD = CLR.COLOR_CD");
            query.AppendLine("  WHERE CLR.DPY_NO = ODR.DPY_NO AND CLR.DEL_FLG = '0'");
            query.AppendLine(") AS COLOR_NM");          // 色名漢字
            query.AppendLine("FROM TOKANODR_TBL ODR");
            query.AppendLine("LEFT OUTER JOIN TOKAN_KSKBN_MST KSKBN ON KSKBN.KSKBN_CD = ODR.KSKBN_CD");
            query.AppendLine("LEFT OUTER JOIN CUSTMER_MST CUS ON CUS.CUSTMER_CD = ODR.CUSTMER_CD");
            query.AppendLine("WHERE ODR.DPY_NO = :0");

            paraList.Add(dpyno);

            return query.ToString();
        }

        /// <summary>
        /// グラビア 登録画面 進捗データ取得クエリ 東洋製罐
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="paraList">パラメータリスト</param>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/21
        /// </remarks>
        public static string GetGurabiaMgmtToyo(string dpyno, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("PLAN.DPY_NO,");           //  呼出しNo
            query.AppendLine("PLAN.PROCESS_CD,");       //  工程コード
            query.AppendLine("MGMT.SUBPROCESS_CD,");    //  Sub工程コード
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY, 'YYYY-MM-DD') AS YOTEI_DAY,");        //  予定日
            query.AppendLine("TO_CHAR(MGMT.COMMIT_DATE, 'YYYY-MM-DD') AS COMMIT_DATE,");    //  作業完了日
            query.AppendLine("MGMT.EMPLOYEE_CD,");      //  担当者コード
            query.AppendLine("EMP.EMPLOYEE_NM,");       //  担当者名
            query.AppendLine("MGMT.WORKTIME_FROM,");    //  作業時間(FROM)
            query.AppendLine("MGMT.WORKTIME_TO,");      //  作業時間(TO)
            query.AppendLine("MGMT.WORK_MEMO,");        //  作業メモ
            query.AppendLine("MGMT.MEMO,");             //  メモ
            query.AppendLine("CASE WHEN SHODR.DENSOU_DAY IS NOT NULL THEN 1");
            query.AppendLine("      ELSE NULL");
            query.AppendLine(" END AS DENDAYCHK,");     //  電送予定日チェック
            query.AppendLine("MGMT.DENSO_FLG");         //  電送フラグ
            query.AppendLine("FROM PROGPLAN_GRA_TBL PLAN");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT ON MGMT.DPY_NO = PLAN.DPY_NO AND MGMT.PROCESS_CD = PLAN.PROCESS_CD");
            query.AppendLine("LEFT OUTER JOIN PROGEMPLOYEE_MST EMP ON EMP.EMPLOYEE_CD = MGMT.EMPLOYEE_CD");
            query.AppendLine("LEFT OUTER JOIN SHODR_TBL SHODR ON SHODR.DPY_NO = PLAN.DPY_NO");
            query.AppendLine("WHERE PLAN.DPY_NO = :0");
            query.AppendLine("AND SHODR.SHGRVSH_KBN = '2'");
            paraList.Add(dpyno);
            query.AppendLine("ORDER BY PLAN.PROCESS_CD, MGMT.SUBPROCESS_CD");
            return query.ToString();

        }

        /// <summary>
        /// グラビア 登録画面 進捗データ取得クエリ 東罐興業
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="paraList">パラメータリスト</param>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/12/10
        /// </remarks>
        public static string GetGurabiaMgmtTokan(string dpyno, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("PLAN.DPY_NO,");           //  呼出しNo
            query.AppendLine("PLAN.PROCESS_CD,");       //  工程コード
            query.AppendLine("MGMT.SUBPROCESS_CD,");    //  Sub工程コード
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY, 'YYYY-MM-DD') AS YOTEI_DAY,");        //  予定日
            query.AppendLine("TO_CHAR(MGMT.COMMIT_DATE, 'YYYY-MM-DD') AS COMMIT_DATE,");    //  作業完了日
            query.AppendLine("MGMT.EMPLOYEE_CD,");      //  担当者コード
            query.AppendLine("EMP.EMPLOYEE_NM,");       //  担当者名
            query.AppendLine("MGMT.WORKTIME_FROM,");    //  作業時間(FROM)
            query.AppendLine("MGMT.WORKTIME_TO,");      //  作業時間(TO)
            query.AppendLine("MGMT.WORK_MEMO,");        //  作業メモ
            query.AppendLine("MGMT.MEMO,");             //  メモ
            query.AppendLine("CASE WHEN ODR.DENSOU_DAY IS NOT NULL THEN 1");
            query.AppendLine("      ELSE NULL");
            query.AppendLine(" END AS DENDAYCHK,");     //  電送予定日チェック
            query.AppendLine("MGMT.DENSO_FLG");         //  電送フラグ
            query.AppendLine("FROM PROGPLAN_GRA_TBL PLAN");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL MGMT ON MGMT.DPY_NO = PLAN.DPY_NO AND MGMT.PROCESS_CD = PLAN.PROCESS_CD");
            query.AppendLine("LEFT OUTER JOIN PROGEMPLOYEE_MST EMP ON EMP.EMPLOYEE_CD = MGMT.EMPLOYEE_CD");
            query.AppendLine("LEFT OUTER JOIN TOKANODR_TBL ODR ON ODR.DPY_NO = PLAN.DPY_NO");
            query.AppendLine("WHERE PLAN.DPY_NO = :0");
            paraList.Add(dpyno);
            query.AppendLine("ORDER BY PLAN.PROCESS_CD, MGMT.SUBPROCESS_CD");
            return query.ToString();

        }

        /// <summary>
        /// グラビア 登録画面 グラビア進捗管理テーブル登録クエリ
        /// </summary>
        /// <param name="register">登録情報</param>
        /// <param name="dpyno">伝票No</param>
        /// <param name="process">工程コード</param>
        /// <param name="sub">サブ工程コード</param>
        /// <param name="uid">ユーザーID</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/21
        /// </remarks>
        public static string RegistGurabiaMgmt(Models.Gurabia.Register register, string dpyno, string process, string sub, string uid, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            //MERGE
            query.AppendLine("MERGE INTO PROGMGMT_GRA_TBL MGMT");
            query.AppendLine("USING(");
            query.AppendLine(" SELECT");

            query.AppendLine(" :0 AS DPY_NO,");
            paraList.Add(dpyno);

            query.AppendLine(" :1 AS PROCESS_CD,");
            paraList.Add(process);

            query.AppendLine(" :2 AS SUBPROCESS_CD,");
            paraList.Add(sub);

            query.AppendLine(" TO_DATE(:3,'RR-MM-DD') AS COMMIT_DATE,");
            paraList.Add(register.CommitDate);

            query.AppendLine(" :4 AS EMPLOYEE_CD,");
            paraList.Add(register.EmployeeCd);

            query.AppendLine(" :5 AS WORKTIME_FROM,");
            paraList.Add(register.WorkTimeFrom);

            query.AppendLine(" :6 AS WORKTIME_TO,");
            paraList.Add(register.WorkTimeTo);

            query.AppendLine(" :7 AS WORK_MEMO,");
            paraList.Add(register.WorkMemo);

            query.AppendLine(" :8 AS DENSO_FLG,");
            paraList.Add(register.DenFlg_Str);

            query.AppendLine(" :9 AS MEMO,");
            paraList.Add(register.Memo);

            query.AppendLine(" :10 AS ID,");
            paraList.Add(uid);

            query.AppendLine(" 'GURABIA' AS PG,");
            query.AppendLine(" TO_DATE(SYSDATE,'RR-MM-DD') AS DT");
            query.AppendLine(" FROM DUAL");
            query.AppendLine(") TMP");

            // 条件
            query.AppendLine("ON (");
            query.AppendLine(" MGMT.DPY_NO = TMP.DPY_NO");
            query.AppendLine(" AND MGMT.PROCESS_CD = TMP.PROCESS_CD");
            query.AppendLine(" AND MGMT.SUBPROCESS_CD = TMP.SUBPROCESS_CD");
            query.AppendLine(")");

            // 条件に一致するレコードがあれば更新
            query.AppendLine("WHEN MATCHED THEN");
            query.AppendLine("UPDATE SET");
            query.AppendLine(" MGMT.COMMIT_DATE   = TMP.COMMIT_DATE,");
            query.AppendLine(" MGMT.EMPLOYEE_CD   = TMP.EMPLOYEE_CD,");
            query.AppendLine(" MGMT.WORKTIME_FROM = TMP.WORKTIME_FROM,");
            query.AppendLine(" MGMT.WORKTIME_TO = TMP.WORKTIME_TO,");
            query.AppendLine(" MGMT.WORK_MEMO     = TMP.WORK_MEMO,");
            query.AppendLine(" MGMT.DENSO_FLG     = TMP.DENSO_FLG,");
            query.AppendLine(" MGMT.MEMO          = TMP.MEMO,");
            query.AppendLine(" MGMT.UPDT_DT       = TMP.DT,");
            query.AppendLine(" MGMT.UPDT_USERID   = TMP.ID,");
            query.AppendLine(" MGMT.UPDT_PG       = TMP.PG");

            // 条件に一致するレコードがなければ追加
            query.AppendLine("WHEN NOT MATCHED THEN");
            query.AppendLine("INSERT VALUES (");
            query.AppendLine(" TMP.DPY_NO,");
            query.AppendLine(" TMP.PROCESS_CD,");
            query.AppendLine(" TMP.SUBPROCESS_CD,");
            query.AppendLine(" TMP.COMMIT_DATE,");
            query.AppendLine(" TMP.EMPLOYEE_CD,");
            query.AppendLine(" TMP.WORKTIME_FROM,");
            query.AppendLine(" TMP.WORKTIME_TO,");
            query.AppendLine(" TMP.WORK_MEMO,");
            query.AppendLine(" TMP.DENSO_FLG,");
            query.AppendLine(" TMP.MEMO,");
            query.AppendLine(" TMP.DT, TMP.ID, TMP.PG,");
            query.AppendLine(" TMP.DT, TMP.ID, TMP.PG");
            query.AppendLine(")");

            return query.ToString();
        }

        /// <summary>
        /// グラビア　CSV出力画面　CSVデータ取得クエリ 東洋製罐
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/28
        /// </remarks>
        public static string GetGurabiaCsvToyo(Models.Gurabia.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("CUS.CUSTMER_RNM AS \"得意先\",");
            query.AppendLine("SHODR.SUBNEGA_NO AS \"サブネガNo\",");
            query.AppendLine("SHODR.DPY_NO AS \"呼出しNo\",");
            query.AppendLine("THGNG.BURANDO_NM AS \"ブランド\",");
            query.AppendLine("THGNG.SYOHIN_NM AS \"品名\",");
            query.AppendLine("THGNG.KAN_NM AS \"規格\",");
            query.AppendLine("KSKBN.KSKBN_NM AS \"校正区分\",");
            query.AppendLine("( SELECT LISTAGG( CLR.COLOR_KJ, ' ') WITHIN GROUP (ORDER BY GRACLR.EDA_NO)");
            query.AppendLine("  FROM GRACLR_TBL GRACLR INNER JOIN COLOR_MST CLR ON CLR.COLOR_CD = GRACLR.COLOR_CD");
            query.AppendLine("  WHERE GRACLR.DPY_NO = SHODR.DPY_NO AND GRACLR.DEL_FLG = '0'");
            query.AppendLine(") AS \"色名\",");
            query.AppendLine("PROC.PROCESS_NM AS \"工程\",");
            query.AppendLine("CASE MGMT.PROCESS_CD || MGMT.SUBPROCESS_CD");
            query.AppendLine("      WHEN '0201' THEN '編集'");
            query.AppendLine("      WHEN '0202' THEN '検査'");
            query.AppendLine("      WHEN '0301' THEN '工程1'");
            query.AppendLine("      WHEN '0302' THEN '工程2'");
            query.AppendLine("      ELSE ''");
            query.AppendLine("END AS \"サブ工程\",");
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY,'YYYY/MM/DD') AS \"予定日\",");
            query.AppendLine("TO_CHAR(MGMT.COMMIT_DATE,'YYYY/MM/DD') AS \"作業日\",");
            query.AppendLine("EMP.EMPLOYEE_NM AS \"担当者名\",");
            query.AppendLine("MGMT.WORKTIME_FROM AS \"作業時間(From)\",");
            query.AppendLine("MGMT.WORKTIME_To AS \"作業時間(To)\",");
            query.AppendLine("MGMT.WORK_MEMO AS \"作業メモ\",");
            query.AppendLine("MGMT.MEMO AS \"メモ\",");
            query.AppendLine("CASE MGMT_HEN1.DENSO_FLG");
            query.AppendLine("      WHEN '1' THEN '済'");
            query.AppendLine("      ELSE ''");
            query.AppendLine("END AS \"電送\"");

            query.AppendLine("FROM SHODR_TBL SHODR");                                                       // 製版注文テーブル
            query.AppendLine("INNER JOIN PROGPLAN_GRA_TBL PLAN ON PLAN.DPY_NO = SHODR.DPY_NO");             // グラビア工程予定テーブル
            query.AppendLine("INNER JOIN PROGMGMT_GRA_TBL MGMT ON  MGMT.DPY_NO = SHODR.DPY_NO AND MGMT.PROCESS_CD = PLAN.PROCESS_CD");   // グラビア進捗管理テーブル
            query.AppendLine("INNER JOIN PROGMGMT_GRA_TBL MGMT_HEN1 ON MGMT_HEN1.DPY_NO = SHODR.DPY_NO AND MGMT_HEN1.PROCESS_CD = '02' AND MGMT_HEN1.SUBPROCESS_CD = '01'");   // グラビア進捗管理テーブル(編集編集)
            query.AppendLine("LEFT OUTER JOIN THGNG_MST THGNG ON THGNG.NGSH_KEY = SHODR.NGSH_KEY");         // 東版Gネガマスタ
            query.AppendLine("LEFT OUTER JOIN CUSTMER_MST CUS ON CUS.CUSTMER_CD = SHODR.CUSTMER_CD");       // 得意先マスタ
            query.AppendLine("LEFT OUTER JOIN KSKBN_MST KSKBN ON KSKBN.KSKBN_CD = SHODR.KSKBN_CD");         // 校正区分マスタ
            query.AppendLine("LEFT OUTER JOIN PROGPROCESS_MST PROC ON PROC.PROCESS_CD = MGMT.PROCESS_CD");  // 進捗管理工程マスタ
            query.AppendLine("LEFT OUTER JOIN PROGEMPLOYEE_MST EMP ON EMP.EMPLOYEE_CD = MGMT.EMPLOYEE_CD"); // 進捗管理担当者マスタ

            // 出力期間
            query.AppendLine("WHERE MGMT.COMMIT_DATE BETWEEN TO_DATE(:0,'RR-MM-DD') AND TO_DATE(:1,'RR-MM-DD')");
            paraList.Add(con.OutputDateFrom);
            if (!string.IsNullOrWhiteSpace(con.OutputDateTo))
            {
                paraList.Add(con.OutputDateTo);
            }
            else
            {
                paraList.Add(con.OutputDateFrom);
            }

            // 工程
            if (con.OutputProcess != null)
            {
                query.AppendLine("AND MGMT.PROCESS_CD IN ( ");
                for (int i = 0; i < con.OutputProcess.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.OutputProcess[i]);
                }
                query.AppendLine(" ) ");
            }

            // 得意先
            query.AppendLine("AND SHODR.CUSTMER_CD <> '9904'");

            query.Append("ORDER BY SHODR.DPY_NO, SHODR.SUBNEGA_NO, MGMT.PROCESS_CD, MGMT.SUBPROCESS_CD, MGMT.COMMIT_DATE");

            return query.ToString();

        }

        /// <summary>
        /// グラビア　CSV出力画面　CSVデータ取得クエリ 東罐興業
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/28
        /// </remarks>
        public static string GetGurabiaCsvTokan(Models.Gurabia.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("CUS.CUSTMER_RNM AS \"得意先\",");
            query.AppendLine("ODR.ORDER_NO || ' ' || ODR.PRODUCT_CD AS \"サブネガNo\",");
            query.AppendLine("ODR.DPY_NO AS \"呼出しNo\",");
            query.AppendLine("ODR.BURANDO_NM AS \"ブランド\",");
            query.AppendLine("ODR.SYOHIN_NM AS \"品名\",");
            query.AppendLine("ODR.KIKAKU AS \"規格\",");
            query.AppendLine("KSKBN.KSKBN_NM AS \"校正区分\",");
            query.AppendLine("( SELECT LISTAGG( CLRM.COLOR_KJ, ' ') WITHIN GROUP (ORDER BY CLR.EDA_NO)");
            query.AppendLine("  FROM TOKANCLR_TBL CLR INNER JOIN COLOR_MST CLRM ON CLRM.COLOR_CD = CLR.COLOR_CD");
            query.AppendLine("  WHERE CLR.DPY_NO = ODR.DPY_NO AND CLR.DEL_FLG = '0'");
            query.AppendLine(") AS \"色名\",");
            query.AppendLine("PROC.PROCESS_NM AS \"工程\",");
            query.AppendLine("CASE MGMT.PROCESS_CD || MGMT.SUBPROCESS_CD");
            query.AppendLine("      WHEN '0201' THEN '編集'");
            query.AppendLine("      WHEN '0202' THEN '検査'");
            query.AppendLine("      WHEN '0301' THEN '工程1'");
            query.AppendLine("      WHEN '0302' THEN '工程2'");
            query.AppendLine("      ELSE ''");
            query.AppendLine("END AS \"サブ工程\",");
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY,'YYYY/MM/DD') AS \"予定日\",");
            query.AppendLine("TO_CHAR(MGMT.COMMIT_DATE,'YYYY/MM/DD') AS \"作業日\",");
            query.AppendLine("EMP.EMPLOYEE_NM AS \"担当者名\",");
            query.AppendLine("MGMT.WORKTIME_FROM AS \"作業時間(From)\",");
            query.AppendLine("MGMT.WORKTIME_To AS \"作業時間(To)\",");
            query.AppendLine("MGMT.WORK_MEMO AS \"作業メモ\",");
            query.AppendLine("MGMT.MEMO AS \"メモ\",");
            query.AppendLine("CASE MGMT_HEN1.DENSO_FLG");
            query.AppendLine("      WHEN '1' THEN '済'");
            query.AppendLine("      ELSE ''");
            query.AppendLine("END AS \"電送\"");

            query.AppendLine("FROM TOKANODR_TBL ODR");                                                      // 東罐興業注文テーブル
            query.AppendLine("INNER JOIN PROGPLAN_GRA_TBL PLAN ON PLAN.DPY_NO = ODR.DPY_NO");               // グラビア工程予定テーブル
            query.AppendLine("INNER JOIN PROGMGMT_GRA_TBL MGMT ON  MGMT.DPY_NO = ODR.DPY_NO AND MGMT.PROCESS_CD = PLAN.PROCESS_CD");   // グラビア進捗管理テーブル
            query.AppendLine("INNER JOIN PROGMGMT_GRA_TBL MGMT_HEN1 ON MGMT_HEN1.DPY_NO = ODR.DPY_NO AND MGMT_HEN1.PROCESS_CD = '02' AND MGMT_HEN1.SUBPROCESS_CD = '01'");   // グラビア進捗管理テーブル(編集編集)
            query.AppendLine("LEFT OUTER JOIN CUSTMER_MST CUS ON CUS.CUSTMER_CD = ODR.CUSTMER_CD");         // 得意先マスタ
            query.AppendLine("LEFT OUTER JOIN TOKAN_KSKBN_MST KSKBN ON KSKBN.KSKBN_CD = ODR.KSKBN_CD");     // 校正区分マスタ
            query.AppendLine("LEFT OUTER JOIN PROGPROCESS_MST PROC ON PROC.PROCESS_CD = MGMT.PROCESS_CD");  // 進捗管理工程マスタ
            query.AppendLine("LEFT OUTER JOIN PROGEMPLOYEE_MST EMP ON EMP.EMPLOYEE_CD = MGMT.EMPLOYEE_CD"); // 進捗管理担当者マスタ

            // 出力期間
            query.AppendLine("WHERE MGMT.COMMIT_DATE BETWEEN TO_DATE(:0,'RR-MM-DD') AND TO_DATE(:1,'RR-MM-DD')");
            paraList.Add(con.OutputDateFrom);
            if (!string.IsNullOrWhiteSpace(con.OutputDateTo))
            {
                paraList.Add(con.OutputDateTo);
            }
            else
            {
                paraList.Add(con.OutputDateFrom);
            }

            // 工程
            if (con.OutputProcess != null)
            {
                query.AppendLine("AND MGMT.PROCESS_CD IN ( ");
                for (int i = 0; i < con.OutputProcess.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.OutputProcess[i]);
                }
                query.AppendLine(" ) ");
            }


            query.Append("ORDER BY ODR.DPY_NO, \"サブネガNo\", MGMT.PROCESS_CD, MGMT.SUBPROCESS_CD, MGMT.COMMIT_DATE");

            return query.ToString();

        }

        #endregion

        #region 刷版

        /// <summary>
        /// 刷版 検索画面 検索クエリ
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <returns></returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/19
        /// </remarks>
        public static string GetSappanSearch(Models.Sappan.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");
            query.AppendLine("NVL2(PLAN.WORK_SHIJI,'＊','') AS MARK,");                  // 指示マーク
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY,'YYYY/MM/DD') AS YOTEI_DAY,");     // 予定日
            query.AppendLine("CMNY.CMNY_NM,");           // 注文内容
            query.AppendLine("SPODR.SPDPY_NO,");         // 呼び出しNo
            query.AppendLine("CUS.CUSTMER_RNM,");        // 発送先
            query.AppendLine("SPODR.SUBNEGA_NO,");       // サブネガNo
            query.AppendLine("SPODR.BURANDO_NM,");       // ブランド
            query.AppendLine("SPODR.SYOHIN_NM,");        // 品名
            query.AppendLine("SPODR.KAN_NM,");           // 容器型
            query.AppendLine("SPODR.ZUMEN_NO,");         // 図面No
            query.AppendLine("SPODR.LOTSU,");            // ロット数
            query.AppendLine("SPODR.HANSU,");            // 版数
            query.AppendLine("PLAN.WORK_SHIJI,");        // 作業指示
            query.AppendLine("DECODE(SPODR.DOC_FLG,1,'有','') AS GYM_CHK1,");         // 資料（資料有無）チェック
            query.AppendLine("DECODE(SPODR.CLRMH_FLG,1,'有','') AS GYM_CHK2,");       // 色見（色見本有無）チェック

            query.AppendLine("TO_CHAR(MGMT_SSEI.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_SSEI,");        // 刷版製造完了日

            query.AppendLine("TO_CHAR(MGMT_SKEN.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_SKEN,");        // 刷版検査完了日

            query.AppendLine("TO_CHAR(MGMT_HEN1.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_HEN1,");           // 編集編集完了日
            query.AppendLine("TO_CHAR(MGMT_HEN2.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_HEN2,");           // 編集検査完了日
            query.AppendLine("NVL2(MGMT_HEN1.COMMIT_DATE,'レ','') AS HEN_CHK1,");                           // 編集編集完了チェック
            query.AppendLine("NVL2(MGMT_HEN2.COMMIT_DATE,'レ','') AS HEN_CHK2,");                           // 編集検査完了チェック

            query.AppendLine("TO_CHAR(MGMT_KEN.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_KEN,");             // 検査完了日

            query.AppendLine("TO_CHAR(MGMT_GYM3.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_GYM3,");           // 業務課出荷 刷版製造完了日
            query.AppendLine("TO_CHAR(MGMT_GYM4.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_GYM4,");           // 業務課出荷 刷版検板完了日
            query.AppendLine("NVL2(MGMT_GYM3.COMMIT_DATE,'レ','') AS GYM_CHK3,");                           // 業務課出荷 刷版製造完了チェック
            query.AppendLine("NVL2(MGMT_GYM4.COMMIT_DATE,'レ','') AS GYM_CHK4,");                           // 業務課出荷 刷版検板完了チェック
            query.AppendLine("TO_CHAR(MGMT_GYM5.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_MGMT_GYM5");            // 業務課出荷 出荷日

            query.AppendLine("FROM SPODR_TBL SPODR");
            query.AppendLine("INNER JOIN PROGPLAN_SPN_TBL PLAN ON PLAN.DPY_NO = SPODR.SPDPY_NO");
            query.AppendLine("LEFT OUTER JOIN CMNY_MST CMNY ON CMNY.CMNY_CD = SPODR.CMNY_CD");
            query.AppendLine("LEFT OUTER JOIN CUSTMER_MST CUS ON CUS.CUSTMER_CD = SPODR.CUSTMER_CD");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SPN_TBL MGMT_SSEI    ON   MGMT_SSEI.DPY_NO = SPODR.SPDPY_NO AND    MGMT_SSEI.PROCESS_CD = '07' AND   MGMT_SSEI.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SPN_TBL MGMT_SKEN    ON   MGMT_SKEN.DPY_NO = SPODR.SPDPY_NO AND    MGMT_SKEN.PROCESS_CD = '08' AND   MGMT_SKEN.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SPN_TBL MGMT_HEN1       ON   MGMT_HEN1.DPY_NO = SPODR.SPDPY_NO    AND    MGMT_HEN1.PROCESS_CD = '02'    AND   MGMT_HEN1.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SPN_TBL MGMT_HEN2       ON   MGMT_HEN2.DPY_NO = SPODR.SPDPY_NO    AND    MGMT_HEN2.PROCESS_CD = '02'    AND   MGMT_HEN2.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SPN_TBL MGMT_KEN        ON   MGMT_KEN.DPY_NO = SPODR.SPDPY_NO     AND    MGMT_KEN.PROCESS_CD = '03'     AND   MGMT_KEN.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SPN_TBL MGMT_GYM3      ON   MGMT_GYM3.DPY_NO = SPODR.SPDPY_NO   AND    MGMT_GYM3.PROCESS_CD = '07'   AND   MGMT_GYM3.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SPN_TBL MGMT_GYM4      ON   MGMT_GYM4.DPY_NO = SPODR.SPDPY_NO   AND    MGMT_GYM4.PROCESS_CD = '08'   AND   MGMT_GYM4.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SPN_TBL MGMT_GYM5      ON   MGMT_GYM5.DPY_NO = SPODR.SPDPY_NO   AND    MGMT_GYM5.PROCESS_CD = '09'   AND   MGMT_GYM5.SUBPROCESS_CD = '01'");

            // 工程
            query.AppendLine("WHERE PLAN.PROCESS_CD = :0");
            paraList.Add(con.Process);

            // 予定日
            query.AppendLine("AND PLAN.YOTEI_DAY BETWEEN TO_DATE(:1,'RR-MM-DD') AND TO_DATE(:2,'RR-MM-DD')");
            paraList.Add(con.ScheDateFrom);
            if (!string.IsNullOrWhiteSpace(con.ScheDateTo))
            {
                paraList.Add(con.ScheDateTo);
            }
            else
            {
                paraList.Add(con.ScheDateFrom);
            }

            // 出力工場
            query.AppendLine("AND SPODR.CONPO_NO = :3");
            paraList.Add(con.Factory_Nm);

            // 注文内容
            if (con.Cmny_Nm != null)
            {
                query.Append("AND SPODR.CMNY_CD IN ( ");

                for (int i = 0; i < con.Cmny_Nm.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Cmny_Nm[i]);
                }

                query.AppendLine(" ) ");
            }

            // ｻﾌﾞﾈｶﾞNo
            if (!string.IsNullOrWhiteSpace(con.Nega_No))
            {
                query.Append("AND SPODR.SUBNEGA_NO LIKE '%' || :").Append(paraList.Count).AppendLine(" || '%'");
                paraList.Add(con.Nega_No);
            }

            // 伝票No
            if (!string.IsNullOrWhiteSpace(con.Spdpy_No))
            {
                query.Append("AND SPODR.SPDPY_NO = :").Append(paraList.Count).AppendLine("");
                paraList.Add(con.Spdpy_No);
            }

            query.AppendLine("ORDER BY PLAN.YOTEI_DAY,SPODR.CUSTMER_CD,SPODR.SUBNEGA_NO");

            return query.ToString();
        }

        /// <summary>
        /// 刷版 登録画面 ヘッダ取得クエリ
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="paraList">パラメータリスト</param>
        /// <returns></returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/24
        /// </remarks>
        public static string GetSappanHeader(string dpyno, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");
            query.AppendLine("SPODR.SUBNEGA_NO,");     // サブネガNo
            query.AppendLine("CMNY.CMNY_NM,");         // 注文内容
            query.AppendLine("SPODR.LOTSU,");          // ロット数
            query.AppendLine("SPODR.HANSU,");          // 版数
            query.AppendLine("SPODR.BURANDO_NM,");     // ブランド名
            query.AppendLine("SPODR.SYOHIN_NM,");      // 品名
            query.AppendLine("SPODR.KAN_NM,");         // 容器型
            query.AppendLine("SPODR.ZUMEN_NO,");       // 図面No
            query.AppendLine("( SELECT LISTAGG( CLR.COLOR_KJ, ' ') WITHIN GROUP (ORDER BY SPCLR.EDA_NO)");
            query.AppendLine("  FROM SPCLR_TBL SPCLR INNER JOIN COLOR_MST CLR ON CLR.COLOR_CD = SPCLR.COLOR_CD");
            query.AppendLine("  WHERE SPCLR.DPY_NO = SPODR.SPDPY_NO AND SPCLR.SPJSB_KBN = '1'");
            query.AppendLine(") AS COLOR_NM,");        // 色名漢字
            query.AppendLine("CUS.CUSTMER_RNM");       // 発送先
            query.AppendLine("FROM SPODR_TBL SPODR");
            query.AppendLine("LEFT OUTER JOIN CMNY_MST CMNY ON CMNY.CMNY_CD = SPODR.CMNY_CD");
            query.AppendLine("LEFT OUTER JOIN CUSTMER_MST CUS ON CUS.CUSTMER_CD = SPODR.CUSTMER_CD");
            query.AppendLine("WHERE SPODR.SPDPY_NO = :0");
            paraList.Add(dpyno);

            return query.ToString();
        }

        /// <summary>
        /// 刷版 登録画面 進捗データ取得クエリ
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="paraList">パラメータリスト</param>
        /// <returns></returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/24
        /// </remarks>
        public static string GetSappanMgmt(string dpyno, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("PLAN.DPY_NO,");           //  呼出しNo
            query.AppendLine("PLAN.PROCESS_CD,");       //  工程コード
            query.AppendLine("MGMT.SUBPROCESS_CD,");    //  サブ工程コード
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY, 'YYYY-MM-DD') AS YOTEI_DAY,");        //  予定日
            query.AppendLine("TO_CHAR(MGMT.COMMIT_DATE, 'YYYY-MM-DD') AS COMMIT_DATE,");    //  作業完了日
            query.AppendLine("MGMT.EMPLOYEE_CD,");      //  担当者コード
            query.AppendLine("EMP.EMPLOYEE_NM,");       //  担当者名
            query.AppendLine("MGMT.WORKTIME_FROM,");    //  完了時間（作業時間FROM)
            query.AppendLine("MGMT.WORK_MEMO,");        //  作業メモ
            query.AppendLine("MGMT.MEMO,");             //  メモ
            query.AppendLine("MGMT.OUT_HANSU,");        //  アウト版数
            query.AppendLine("MGMT.CASE_NO,");          //  ケースNo
            query.AppendLine("CASE WHEN SPODR.CLRMH_FLG = 1 THEN 1");
            query.AppendLine("      ELSE NULL");
            query.AppendLine(" END AS CHK1,");
            query.AppendLine("MGMT.COLOR_FLG,");        //  色見本発送（色見本発送フラグ）
            query.AppendLine("CASE WHEN SPODR.DOC_FLG = 1 THEN 1");
            query.AppendLine("      ELSE NULL");
            query.AppendLine(" END AS CHK2,");
            query.AppendLine("MGMT.DOC_FLG");           //資料発送（資料発送フラグ）
            query.AppendLine("FROM PROGPLAN_SPN_TBL PLAN");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SPN_TBL MGMT ON MGMT.DPY_NO = PLAN.DPY_NO AND MGMT.PROCESS_CD = PLAN.PROCESS_CD");
            query.AppendLine("LEFT OUTER JOIN PROGEMPLOYEE_MST EMP ON EMP.EMPLOYEE_CD = MGMT.EMPLOYEE_CD");
            query.AppendLine("LEFT OUTER JOIN SPODR_TBL SPODR ON SPODR.SPDPY_NO = PLAN.DPY_NO");
            query.AppendLine("WHERE PLAN.DPY_NO = :0");
            paraList.Add(dpyno);
            query.AppendLine("ORDER BY PLAN.PROCESS_CD, MGMT.SUBPROCESS_CD");
            return query.ToString();

        }

        /// <summary>
        /// 刷版 登録画面 刷版進捗管理テーブル登録クエリ
        /// </summary>
        /// <param name="register">登録情報</param>
        /// <param name="dpyno">伝票No</param>
        /// <param name="process">工程コード</param>
        /// <param name="sub">サブ工程コード</param>
        /// <param name="uid">ユーザーID</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <returns></returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/09/24
        /// </remarks>
        public static string RegistSappanMgmt(Models.Sappan.Register register, string dpyno, string process, string sub, string uid, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            //MERGE
            query.AppendLine("MERGE INTO PROGMGMT_SPN_TBL MGMT");
            query.AppendLine("USING(");
            query.AppendLine(" SELECT");

            query.AppendLine(" :0 AS DPY_NO,");
            paraList.Add(dpyno);

            query.AppendLine(" :1 AS PROCESS_CD,");
            paraList.Add(process);

            query.AppendLine(" :2 AS SUBPROCESS_CD,");
            paraList.Add(sub);

            query.AppendLine(" TO_DATE(:3,'RR-MM-DD') AS COMMIT_DATE,");
            paraList.Add(register.CommitDate);

            query.AppendLine(" :4 AS EMPLOYEE_CD,");
            paraList.Add(register.EmployeeCd);

            query.AppendLine(" :5 AS WORKTIME_FROM,");
            paraList.Add(register.WorkTimeFrom);

            query.AppendLine(" :6 AS WORKTIME_TO,");
            paraList.Add(register.WorkTimeTo);

            query.AppendLine(" :7 AS WORK_MEMO,");
            paraList.Add(register.WorkMemo);

            query.AppendLine(" :8 AS OUT_HANSU,");
            paraList.Add(register.OutHan);

            query.AppendLine(" :9 AS CASE_NO,");
            paraList.Add(register.CaseNo);

            query.AppendLine(" :10 AS COLOR_FLG,");

            paraList.Add(register.ColorFlg_Str);

            query.AppendLine(" :11 AS DOC_FLG,");
            paraList.Add(register.DocFlg_Str);

            query.AppendLine(" :12 AS MEMO,");
            paraList.Add(register.Memo);

            query.AppendLine(" :13 AS ID,");
            paraList.Add(uid);

            query.AppendLine(" 'SAPPAN' AS PG,");
            query.AppendLine(" TO_DATE(SYSDATE,'RR-MM-DD') AS DT");
            query.AppendLine(" FROM DUAL");
            query.AppendLine(") TMP");

            // 条件
            query.AppendLine("ON (");
            query.AppendLine(" MGMT.DPY_NO = TMP.DPY_NO");
            query.AppendLine(" AND MGMT.PROCESS_CD = TMP.PROCESS_CD");
            query.AppendLine(" AND MGMT.SUBPROCESS_CD = TMP.SUBPROCESS_CD");
            query.AppendLine(")");

            // 条件に一致するレコードがあれば更新
            query.AppendLine("WHEN MATCHED THEN");
            query.AppendLine("UPDATE SET");
            query.AppendLine(" MGMT.COMMIT_DATE   = TMP.COMMIT_DATE,");
            query.AppendLine(" MGMT.EMPLOYEE_CD   = TMP.EMPLOYEE_CD,");
            query.AppendLine(" MGMT.WORKTIME_FROM = TMP.WORKTIME_FROM,");
            query.AppendLine(" MGMT.WORKTIME_TO = TMP.WORKTIME_TO,");
            query.AppendLine(" MGMT.WORK_MEMO     = TMP.WORK_MEMO,");
            query.AppendLine(" MGMT.OUT_HANSU     = TMP.OUT_HANSU,");
            query.AppendLine(" MGMT.CASE_NO       = TMP.CASE_NO,");
            query.AppendLine(" MGMT.COLOR_FLG     = TMP.COLOR_FLG,");
            query.AppendLine(" MGMT.DOC_FLG       = TMP.DOC_FLG,");
            query.AppendLine(" MGMT.MEMO          = TMP.MEMO,");
            query.AppendLine(" MGMT.UPDT_DT       = TMP.DT,");
            query.AppendLine(" MGMT.UPDT_USERID   = TMP.ID,");
            query.AppendLine(" MGMT.UPDT_PG       = TMP.PG");

            // 条件に一致するレコードがなければ追加
            query.AppendLine("WHEN NOT MATCHED THEN");
            query.AppendLine("INSERT VALUES (");
            query.AppendLine(" TMP.DPY_NO,");
            query.AppendLine(" TMP.PROCESS_CD,");
            query.AppendLine(" TMP.SUBPROCESS_CD,");
            query.AppendLine(" TMP.COMMIT_DATE,");
            query.AppendLine(" TMP.EMPLOYEE_CD,");
            query.AppendLine(" TMP.WORKTIME_FROM,");
            query.AppendLine(" TMP.WORKTIME_TO,");
            query.AppendLine(" TMP.WORK_MEMO,");
            query.AppendLine(" TMP.OUT_HANSU,");
            query.AppendLine(" TMP.CASE_NO,");
            query.AppendLine(" TMP.COLOR_FLG,");
            query.AppendLine(" TMP.DOC_FLG,");
            query.AppendLine(" TMP.MEMO,");
            query.AppendLine(" TMP.DT, TMP.ID, TMP.PG,");
            query.AppendLine(" TMP.DT, TMP.ID, TMP.PG");
            query.AppendLine(")");

            return query.ToString();
        }

        /// <summary>
        /// 刷版　CSV出力画面　CSVデータ取得クエリ
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <returns></returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/10/21
        public static string GetSappanCsv(Models.Sappan.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("SPODR.SUBNEGA_NO AS \"サブネガNo\",");
            query.AppendLine("SPODR.SPDPY_NO AS \"呼出しNo\",");
            query.AppendLine("CMNY.CMNY_NM AS \"注文内容\",");
            query.AppendLine("CUS.CUSTMER_RNM AS \"発送先\",");
            query.AppendLine("SPODR.BURANDO_NM AS \"ブランド\",");
            query.AppendLine("SPODR.SYOHIN_NM AS \"品名\",");
            query.AppendLine("SPODR.KAN_NM AS \"容器型\",");
            query.AppendLine("SPODR.ZUMEN_NO AS \"図面No\",");
            query.AppendLine("SPODR.LOTSU AS \"ロット数\",");
            query.AppendLine("SPODR.HANSU AS \"版数\",");
            query.AppendLine("( SELECT LISTAGG( CLR.COLOR_KJ, ' ') WITHIN GROUP (ORDER BY SPCLR.EDA_NO)");
            query.AppendLine("  FROM SPCLR_TBL SPCLR INNER JOIN COLOR_MST CLR ON CLR.COLOR_CD = SPCLR.COLOR_CD");
            query.AppendLine("  WHERE SPCLR.DPY_NO = SPODR.SPDPY_NO AND SPCLR.SPJSB_KBN = '1'");
            query.AppendLine(") AS \"色名\",");
            query.AppendLine("PROC.PROCESS_NM AS \"工程\",");
            query.AppendLine("CASE MGMT.PROCESS_CD || MGMT.SUBPROCESS_CD");
            query.AppendLine("    WHEN '0201' THEN '編集'");
            query.AppendLine("    WHEN '0202' THEN '検査'");
            query.AppendLine("    ELSE ''");
            query.AppendLine("END AS \"サブ工程\",");
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY,'YYYY/MM/DD') AS \"予定日\",");
            query.AppendLine("TO_CHAR(MGMT.COMMIT_DATE,'YYYY/MM/DD') AS \"作業日\",");
            query.AppendLine("EMP.EMPLOYEE_NM AS \"担当者名\",");
            query.AppendLine("MGMT.WORKTIME_FROM AS \"作業完了時間\",");
            query.AppendLine("MGMT.OUT_HANSU AS \"アウト版数\",");
            query.AppendLine("MGMT.CASE_NO AS \"ケースNo\",");
            query.AppendLine("CASE MGMT.COLOR_FLG");
            query.AppendLine("    WHEN '1' THEN '済'");
            query.AppendLine("    ELSE ' '");
            query.AppendLine("END AS \"色見本発送\",");
            query.AppendLine("CASE MGMT.DOC_FLG");
            query.AppendLine("    WHEN '1' THEN '済'");
            query.AppendLine("    ELSE ' '");
            query.AppendLine("END AS \"資料発送\",");
            query.AppendLine("MGMT.WORK_MEMO AS \"作業メモ\",");
            query.AppendLine("MGMT.MEMO AS \"メモ\"");

            query.AppendLine("FROM SPODR_TBL SPODR");                                                        // 刷版注文テーブル
            query.AppendLine("INNER JOIN PROGPLAN_SPN_TBL PLAN ON PLAN.DPY_NO = SPODR.SPDPY_NO");            // 刷版工程予定テーブル
            query.AppendLine("INNER JOIN PROGMGMT_SPN_TBL MGMT ON MGMT.DPY_NO = SPODR.SPDPY_NO AND MGMT.PROCESS_CD = PLAN.PROCESS_CD");      // 刷版進捗管理テーブル
            query.AppendLine("LEFT OUTER JOIN CMNY_MST CMNY ON CMNY.CMNY_CD = SPODR.CMNY_CD");               // 注文内容マスタ
            query.AppendLine("LEFT OUTER JOIN PROGPROCESS_MST PROC ON PROC.PROCESS_CD = MGMT.PROCESS_CD");   // 進捗管理工程マスタ
            query.AppendLine("LEFT OUTER JOIN PROGEMPLOYEE_MST EMP ON EMP.EMPLOYEE_CD = MGMT.EMPLOYEE_CD");  // 進捗管理担当者マスタ
            query.AppendLine("LEFT OUTER JOIN CUSTMER_MST CUS ON CUS.CUSTMER_CD = SPODR.CUSTMER_CD");        // 得意先マスタ

            // 作業日
            query.AppendLine("WHERE MGMT.COMMIT_DATE BETWEEN TO_DATE(:0,'RR-MM-DD') AND TO_DATE(:1,'RR-MM-DD')");
            paraList.Add(con.OutputDateFrom);
            if (!string.IsNullOrWhiteSpace(con.OutputDateTo))
            {
                paraList.Add(con.OutputDateTo);
            }
            else
            {
                paraList.Add(con.OutputDateFrom);
            }

            // 工程
            if (con.OutputProcess != null)
            {
                query.AppendLine("AND MGMT.PROCESS_CD IN ( ");
                for (int i = 0; i < con.OutputProcess.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.OutputProcess[i]);
                }
                query.AppendLine(" ) ");
            }


            // 注文内容
            if (con.Cmny_Nm != null)
            {
                query.Append("AND SPODR.CMNY_CD IN ( ");

                for (int i = 0; i < con.Cmny_Nm.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Cmny_Nm[i]);
                }
                query.AppendLine(" ) ");
            }

            // 出力工場
            if (con.OutputFactory_Nm != null)
            {
                query.Append("AND SPODR.CONPO_NO IN ( ");

                for (int i = 0; i < con.OutputFactory_Nm.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.OutputFactory_Nm[i]);
                }
                query.AppendLine(" ) ");
            }

            query.Append("ORDER BY SPODR.SPDPY_NO, SPODR.SUBNEGA_NO, MGMT.PROCESS_CD, MGMT.SUBPROCESS_CD, MGMT.COMMIT_DATE");

            return query.ToString();

        }

        #endregion

        #region 樹脂版

        /// <summary>
        /// 樹脂版 検索画面 検索クエリ
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <returns></returns>
        /// <remarks>
        /// 作成者    ：  kawana
        /// 作成日    ：  2019/10/04
        /// </remarks>
        public static string GetJushihanSearch(Models.Jushihan.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("NVL2(PLAN.WORK_SHIJI,'＊','') AS MARK,");    // 作業指示マーク
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY, 'YYYY/MM/DD') AS YOTEI_DAY,");  // 予定日
            query.AppendLine("CMNY.CMNY_NM,");           // 注文内容
            query.AppendLine("JSBODR.JSBDPY_NO,");       // 呼出しNo
            query.AppendLine("CUSTMER.CUSTMER_RNM,");    // 発送先（得意先）
            query.AppendLine("JSBODR.SUBNEGA_NO,");      // ｻﾌﾞﾈｶﾞNo
            query.AppendLine("JSBODR.BURANDO_NM,");      // ブランド
            query.AppendLine("JSBODR.SYOHIN_NM,");       // 品名
            query.AppendLine("JSBODR.HAN_KBN,");         // 版式
            query.AppendLine("JSBODR.LOTSU,");           // ロット数
            query.AppendLine("JSBODR.hansu,");           // 版数
            query.AppendLine("JSBODR.KAN_NM,");          // 容器型
            query.AppendLine("JSBODR.ZUMEN_NO,");        // 図面No
            query.AppendLine("PLAN.WORK_SHIJI,");        // 作業指示(発送指示)
            query.AppendLine("DECODE(JSBODR.CLRMH_FLG,1,'有') AS GYM_CHK1,");                              // 色見（色見本有無）チェック
            query.AppendLine("DECODE(JSBODR.DOC_FLG,1,'有') AS GYM_CHK2,");                                // 資料（資料有無）チェック
            query.AppendLine("TO_CHAR(MGMT_SPNSEIZO.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_SPNSEIZO,");  // 刷版　製造完了日
            query.AppendLine("NVL2(MGMT_SPNSEIZO.COMMIT_DATE,'レ','') SPN_CHK1,");                         // 刷版　製造完了日チェック
            query.AppendLine("TO_CHAR(MGMT_SPNKENSA.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_SPNKENSA,");  // 刷版　検版完了日
            query.AppendLine("NVL2(MGMT_SPNKENSA.COMMIT_DATE,'レ','') AS SPN_CHK2,");                      // 刷版　検版完了日チェック
            query.AppendLine("TO_CHAR(MGMT_GYOUMU.COMMIT_DATE,'YYYY/MM/DD') AS COMMIT_DATE_GYOUMU");       // 業務課出荷完了日
            query.AppendLine("FROM JSBODR_TBL JSBODR");
            query.AppendLine("INNER JOIN PROGPLAN_JSI_TBL PLAN ON PLAN.DPY_NO  = JSBODR.JSBDPY_NO");
            query.AppendLine("LEFT OUTER JOIN CMNY_MST CMNY    ON CMNY.CMNY_CD = JSBODR.CMNY_CD");
            query.AppendLine("LEFT OUTER JOIN CUSTMER_MST CUSTMER   ON CUSTMER.CUSTMER_CD = JSBODR.CUSTMER_CD");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_JSI_TBL MGMT_SPNSEIZO ON   MGMT_SPNSEIZO.DPY_NO = JSBODR.JSBDPY_NO  AND MGMT_SPNSEIZO.PROCESS_CD = '07' AND MGMT_SPNSEIZO.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_JSI_TBL MGMT_SPNKENSA ON   MGMT_SPNKENSA.DPY_NO = JSBODR.JSBDPY_NO  AND MGMT_SPNKENSA.PROCESS_CD = '08' AND MGMT_SPNKENSA.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_JSI_TBL MGMT_GYOUMU ON     MGMT_GYOUMU.DPY_NO   = JSBODR.JSBDPY_NO  AND MGMT_GYOUMU.PROCESS_CD   = '09' AND MGMT_GYOUMU.SUBPROCESS_CD   = '01'");

            // 工程
            query.AppendLine("WHERE PLAN.PROCESS_CD = :0");
            paraList.Add(con.Process);

            // 予定日
            query.AppendLine("AND PLAN.YOTEI_DAY BETWEEN TO_DATE(:1,'RR-MM-DD') AND TO_DATE(:2,'RR-MM-DD')");
            paraList.Add(con.ScheDateFrom);
            if (!string.IsNullOrWhiteSpace(con.ScheDateTo))
            {
                paraList.Add(con.ScheDateTo);
            }
            else
            {
                paraList.Add(con.ScheDateFrom);
            }


            // 版式
            if (con.Han_Type != null)
            {
                query.Append("AND JSBODR.HAN_KBN IN ( ");

                for (int i = 0; i < con.Han_Type.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Han_Type[i]);
                }

                query.AppendLine(" ) ");
            }

            // ｻﾌﾞﾈｶﾞNo
            if (!string.IsNullOrWhiteSpace(con.Nega_No))
            {
                query.Append("AND JSBODR.SUBNEGA_NO LIKE '%' || :").Append(paraList.Count).AppendLine(" || '%'");
                paraList.Add(con.Nega_No);
            }

            // 伝票No
            if (!string.IsNullOrWhiteSpace(con.Dpy_No))
            {
                query.Append("AND JSBODR.JSBDPY_NO = :").Append(paraList.Count).AppendLine("");
                paraList.Add(con.Dpy_No);
            }

            query.AppendLine("ORDER BY PLAN.YOTEI_DAY,JSBODR.CUSTMER_CD,JSBODR.SUBNEGA_NO");

            return query.ToString();
        }

        /// <summary>
        /// 樹脂版 登録画面 ヘッダ取得クエリ
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="paraList">パラメータリスト</param>
        /// <returns></returns>
        /// <remarks>
        /// 作成者    ：  kawana
        /// 作成日    ：  2019/10/24
        /// </remarks>
        public static string GetJushihanHeader(string dpyno, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");
            query.AppendLine("JSBODR.SUBNEGA_NO,");  // サブネガNo
            query.AppendLine("CMNY.CMNY_NM,");    // 注文内容
            query.AppendLine("JSBODR.LOTSU,");         // ロット数
            query.AppendLine("JSBODR.HANSU,");         // 版数
            query.AppendLine("JSBODR.BURANDO_NM,");  // ブランド名
            query.AppendLine("CUSTMER.CUSTMER_RNM,");// 発送先（得意先コード）
            query.AppendLine("JSBODR.SYOHIN_NM,");    // 商品名
            query.AppendLine("JSBODR.KAN_NM,");     // 容器型
            query.AppendLine("JSBODR.ZUMEN_NO,");  // 図面No
            query.AppendLine("JSBODR.HAN_KBN,");   // 版式
            query.AppendLine("( SELECT LISTAGG( CLR.COLOR_KJ, ' ') WITHIN GROUP(ORDER BY SPCLR.EDA_NO)");
            query.AppendLine("  FROM SPCLR_TBL SPCLR INNER JOIN COLOR_MST CLR ON CLR.COLOR_CD = SPCLR.COLOR_CD");
            query.AppendLine("  WHERE SPCLR.DPY_NO = JSBODR.JSBDPY_NO AND SPCLR.SPJSB_KBN = '2'");
            query.AppendLine(")AS COLOR_NM");       // 色名漢字
            query.AppendLine("FROM JSBODR_TBL JSBODR");
            query.AppendLine("LEFT OUTER JOIN CMNY_MST CMNY ON CMNY.CMNY_CD = JSBODR.CMNY_CD");
            query.AppendLine("LEFT OUTER JOIN CUSTMER_MST CUSTMER ON CUSTMER.CUSTMER_CD = JSBODR.CUSTMER_CD");
            query.AppendLine("WHERE JSBODR.JSBDPY_NO = :0");
            paraList.Add(dpyno);

            return query.ToString();
        }

        /// <summary>
        /// 樹脂版 登録画面 進捗データ取得クエリ
        /// </summary>
        /// <param name="dpyno">伝票No</param>
        /// <param name="paraList">パラメータリスト</param>
        /// <returns></returns>
        /// <remarks>
        /// 作成者    ：  kawana
        /// 作成日    ：  2019/10/24
        /// </remarks>
        public static string GetJushihanMgmt(string dpyno, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("PLAN.DPY_NO,");           //  呼出し№
            query.AppendLine("PLAN.PROCESS_CD,");       //  工程コード
            query.AppendLine("MGMT.SUBPROCESS_CD,");    //  サブ工程コード
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY, 'YYYY-MM-DD') AS YOTEI_DAY,");        //  予定日
            query.AppendLine("TO_CHAR(MGMT.COMMIT_DATE, 'YYYY-MM-DD') AS COMMIT_DATE,");    //  作業日
            query.AppendLine("MGMT.EMPLOYEE_CD,");      //  担当者コード
            query.AppendLine("EMP.EMPLOYEE_NM,");       //  担当者名
            query.AppendLine("MGMT.WORKTIME_FROM,");    //  作業時間From
            query.AppendLine("MGMT.WORKTIME_TO,");      //  作業時間To
            query.AppendLine("MGMT.WORK_MEMO,");        //  作業メモ
            query.AppendLine("MGMT.MEMO,");             //  メモ
            query.AppendLine("MGMT.PRODUCT_LINE,");     //  製造ライン
            query.AppendLine("MGMT.GENSHO_LINE,");      //  現象ライン
            query.AppendLine("MGMT.OUT_HANSU,");        //  アウト版数
            query.AppendLine("MGMT.CASE_NO,");          //  ケースNo
            //色見本発送
            query.AppendLine("CASE WHEN JSBODR.CLRMH_FLG = 1 THEN 1");
            query.AppendLine("      ELSE NULL");
            query.AppendLine(" END AS CHK1,");
            query.AppendLine("MGMT.COLOR_FLG,");
            //資料発送
            query.AppendLine("CASE WHEN JSBODR.DOC_FLG = 1 THEN 1");
            query.AppendLine("      ELSE NULL");
            query.AppendLine(" END AS CHK2,");
            query.AppendLine("MGMT.DOC_FLG");
            query.AppendLine("FROM PROGPLAN_JSI_TBL PLAN");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_JSI_TBL MGMT ON MGMT.DPY_NO = PLAN.DPY_NO AND MGMT.PROCESS_CD = PLAN.PROCESS_CD");
            query.AppendLine("LEFT OUTER JOIN PROGEMPLOYEE_MST EMP ON EMP.EMPLOYEE_CD = MGMT.EMPLOYEE_CD");
            query.AppendLine("LEFT OUTER JOIN JSBODR_TBL JSBODR ON JSBODR.JSBDPY_NO = PLAN.DPY_NO");
            query.AppendLine("WHERE PLAN.DPY_NO = :0");
            paraList.Add(dpyno);
            query.AppendLine("ORDER BY PLAN.PROCESS_CD, MGMT.SUBPROCESS_CD");
            return query.ToString();

        }

        /// <summary>
        /// 樹脂版 登録画面 樹脂版進捗管理テーブル登録クエリ
        /// </summary>
        /// <param name="register">登録情報</param>
        /// <param name="dpyno">伝票No</param>
        /// <param name="process">工程コード</param>
        /// <param name="sub">サブ工程コード</param>
        /// <param name="uid">ユーザーID</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <remarks>
        /// 作成者    ：  kawana
        /// 作成日    ：  2019/10/24
        /// </remarks>
        public static string RegistJushihanMgmt(Models.Jushihan.Register register, string dpyno, string process, string sub, string uid, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            // MERGE
            query.AppendLine("MERGE INTO PROGMGMT_JSI_TBL MGMT ");
            query.AppendLine("USING ( ");
            query.AppendLine("SELECT ");

            query.AppendLine("  :0 AS DPY_NO, ");
            paraList.Add(dpyno);

            query.AppendLine("  :1 AS PROCESS_CD,  ");
            paraList.Add(process);

            query.AppendLine("  :2 AS SUBPROCESS_CD,  ");
            paraList.Add(sub);

            query.AppendLine("  TO_DATE(:3,'RR-MM-DD') AS COMMIT_DATE,  ");
            paraList.Add(register.CommitDate);

            query.AppendLine("  :4 AS EMPLOYEE_CD,  ");
            paraList.Add(register.EmployeeCd);

            query.AppendLine("  :5 AS WORKTIME_FROM,  ");
            paraList.Add(register.WorkTimeFrom);

            query.AppendLine("  :6 AS WORKTIME_TO,  ");
            paraList.Add(register.WorkTimeTo);

            query.AppendLine("  :7 AS WORK_MEMO,  ");
            paraList.Add(register.WorkMemo);

            query.AppendLine("  :8 AS PRODUCT_LINE,  ");
            paraList.Add(register.PRODUCT_LINE);

            query.AppendLine("  :9 AS GENSHO_LINE,  ");
            paraList.Add(register.GENSHO_LINE);

            query.AppendLine("  :10 AS OUT_HANSU,  ");
            paraList.Add(register.OUT_HANSU);

            query.AppendLine("  :11 AS CASE_NO,  ");
            paraList.Add(register.CaseNo);

            query.AppendLine("  :12 AS COLOR_FLG,  ");
            paraList.Add(register.ColorFlg_Str);

            query.AppendLine("  :13 AS DOC_FLG,  ");
            paraList.Add(register.DocFlg_Str);

            query.AppendLine("  :14 AS MEMO,  ");
            paraList.Add(register.Memo);

            query.AppendLine("  :15 AS ID,  ");
            paraList.Add(uid);

            query.AppendLine("   'JUSHIHAN'     AS PG, ");
            query.AppendLine("  TO_DATE(SYSDATE,'RR-MM-DD') AS DT ");
            query.AppendLine("  FROM DUAL ");
            query.AppendLine("  ) TMP ");

            // 条件
            query.AppendLine("ON (");
            query.AppendLine("MGMT.DPY_NO = TMP.DPY_NO");
            query.AppendLine("AND MGMT.PROCESS_CD = TMP.PROCESS_CD");
            query.AppendLine("AND MGMT.SUBPROCESS_CD = TMP.SUBPROCESS_CD");
            query.AppendLine(")");

            // 条件に一致するレコードがあれば更新
            query.AppendLine("WHEN MATCHED THEN ");
            query.AppendLine("UPDATE SET ");
            query.AppendLine("  MGMT.COMMIT_DATE   = TMP.COMMIT_DATE,  ");
            query.AppendLine("  MGMT.EMPLOYEE_CD   = TMP.EMPLOYEE_CD,  ");
            query.AppendLine("  MGMT.WORKTIME_FROM = TMP.WORKTIME_FROM, ");
            query.AppendLine("  MGMT.WORKTIME_TO   = TMP.WORKTIME_TO, ");
            query.AppendLine("  MGMT.WORK_MEMO     = TMP.WORK_MEMO,  ");
            query.AppendLine("  MGMT.PRODUCT_LINE  = TMP.PRODUCT_LINE,  ");
            query.AppendLine("  MGMT.GENSHO_LINE   = TMP.GENSHO_LINE, ");
            query.AppendLine("  MGMT.OUT_HANSU     = TMP.OUT_HANSU,  ");
            query.AppendLine("  MGMT.CASE_NO     = TMP.CASE_NO,  ");
            query.AppendLine("  MGMT.COLOR_FLG     = TMP.COLOR_FLG, ");
            query.AppendLine("  MGMT.DOC_FLG       = TMP.DOC_FLG, ");
            query.AppendLine("  MGMT.MEMO          = TMP.MEMO, ");
            query.AppendLine("  MGMT.UPDT_DT       = TMP.DT, ");
            query.AppendLine("  MGMT.UPDT_USERID   = TMP.ID,  ");
            query.AppendLine("  MGMT.UPDT_PG       = TMP.PG ");

            // 条件に一致するレコードがなければ追加
            query.AppendLine("WHEN NOT MATCHED THEN");
            query.AppendLine("INSERT VALUES (");
            query.AppendLine("  TMP.DPY_NO, ");
            query.AppendLine("  TMP.PROCESS_CD, ");
            query.AppendLine("  TMP.SUBPROCESS_CD, ");
            query.AppendLine("  TMP.COMMIT_DATE, ");
            query.AppendLine("  TMP.EMPLOYEE_CD, ");
            query.AppendLine("  TMP.WORKTIME_FROM, ");
            query.AppendLine("  TMP.WORKTIME_TO, ");
            query.AppendLine("  TMP.WORK_MEMO, ");
            query.AppendLine("  TMP.PRODUCT_LINE, ");
            query.AppendLine("  TMP.GENSHO_LINE, ");
            query.AppendLine("  TMP.OUT_HANSU, ");
            query.AppendLine("  TMP.CASE_NO, ");
            query.AppendLine("  TMP.COLOR_FLG, ");
            query.AppendLine("  TMP.DOC_FLG, ");
            query.AppendLine("  TMP.MEMO, ");
            query.AppendLine("  TMP.DT, TMP.ID, TMP.PG,");
            query.AppendLine("  TMP.DT, TMP.ID, TMP.PG");

            query.AppendLine(")");

            return query.ToString();
        }

        /// <summary>
        /// 樹脂版　CSV出力画面　CSVデータ取得クエリ
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <returns></returns>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/06
        /// </remarks>
        public static string GetJushihanCsv(Models.Jushihan.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("JSBODR.SUBNEGA_NO AS \"サブネガNo\",");
            query.AppendLine("JSBODR.JSBDPY_NO AS \"呼出しNo\",");
            query.AppendLine("CMNY.CMNY_NM AS \"注文内容\",");
            query.AppendLine("CUS.CUSTMER_RNM AS \"発送先\",");
            query.AppendLine("JSBODR.BURANDO_NM AS \"ブランド\",");
            query.AppendLine("JSBODR.SYOHIN_NM AS \"品名\",");
            query.AppendLine("JSBODR.KAN_NM AS \"容器型\",");
            query.AppendLine("JSBODR.ZUMEN_NO AS \"図面No\",");
            query.AppendLine("JSBODR.HAN_KBN AS \"版式\",");
            query.AppendLine("JSBODR.LOTSU AS \"ロット数\",");
            query.AppendLine("JSBODR.HANSU AS \"版数\",");
            query.AppendLine("( SELECT LISTAGG( CLR.COLOR_KJ, ' ') WITHIN GROUP (ORDER BY SPCLR.EDA_NO)");
            query.AppendLine("  FROM SPCLR_TBL SPCLR INNER JOIN COLOR_MST CLR ON CLR.COLOR_CD = SPCLR.COLOR_CD");
            query.AppendLine("  WHERE SPCLR.DPY_NO = JSBODR.JSBDPY_NO AND SPCLR.SPJSB_KBN = '2'");
            query.AppendLine(") AS \"色名\",");
            query.AppendLine("PROC.PROCESS_NM AS \"工程\",");
            query.AppendLine("'' AS \"サブ工程\",");
            query.AppendLine("TO_CHAR(PLAN.YOTEI_DAY,'YYYY/MM/DD') AS \"予定日\",");
            query.AppendLine("TO_CHAR(MGMT.COMMIT_DATE,'YYYY/MM/DD') AS \"作業日\",");
            query.AppendLine("EMP.EMPLOYEE_NM AS \"担当者名\",");
            query.AppendLine("MGMT.WORKTIME_FROM AS \"作業完了時間\",");
            query.AppendLine("MGMT.PRODUCT_LINE AS \"製造ライン\",");
            query.AppendLine("MGMT.GENSHO_LINE AS \"現象ライン\",");
            query.AppendLine("MGMT.OUT_HANSU AS \"アウト版数\",");
            query.AppendLine("MGMT.CASE_NO AS \"ケースNo\",");
            query.AppendLine("CASE MGMT.COLOR_FLG");
            query.AppendLine("    WHEN '1' THEN '済'");
            query.AppendLine("    ELSE ' '");
            query.AppendLine("END AS \"色見本発送\",");
            query.AppendLine("CASE MGMT.DOC_FLG");
            query.AppendLine("    WHEN '1' THEN '済'");
            query.AppendLine("    ELSE ' '");
            query.AppendLine("END AS \"資料発送\",");
            query.AppendLine("MGMT.WORK_MEMO AS \"作業メモ\",");
            query.AppendLine("MGMT.MEMO AS \"メモ\"");

            query.AppendLine("FROM JSBODR_TBL JSBODR");                                                       // 樹脂版注文テーブル
            query.AppendLine("INNER JOIN PROGPLAN_JSI_TBL PLAN ON PLAN.DPY_NO = JSBODR.JSBDPY_NO");           // 樹脂版工程予定テーブル
            query.AppendLine("INNER JOIN PROGMGMT_JSI_TBL MGMT ON MGMT.DPY_NO = JSBODR.JSBDPY_NO AND MGMT.PROCESS_CD = PLAN.PROCESS_CD");      // 樹脂版進捗管理テーブル
            query.AppendLine("LEFT OUTER JOIN CMNY_MST CMNY ON CMNY.CMNY_CD = JSBODR.CMNY_CD");               // 注文内容マスタ
            query.AppendLine("LEFT OUTER JOIN PROGPROCESS_MST PROC ON PROC.PROCESS_CD = MGMT.PROCESS_CD");    // 進捗管理工程マスタ
            query.AppendLine("LEFT OUTER JOIN PROGEMPLOYEE_MST EMP ON EMP.EMPLOYEE_CD = MGMT.EMPLOYEE_CD");   // 進捗管理担当者マスタ
            query.AppendLine("LEFT OUTER JOIN CUSTMER_MST CUS ON CUS.CUSTMER_CD = JSBODR.CUSTMER_CD");        // 得意先マスタ

            // 作業日
            query.AppendLine("WHERE MGMT.COMMIT_DATE BETWEEN TO_DATE(:0,'RR-MM-DD') AND TO_DATE(:1,'RR-MM-DD')");
            paraList.Add(con.OutputDateFrom);
            if (!string.IsNullOrWhiteSpace(con.OutputDateTo))
            {
                paraList.Add(con.OutputDateTo);
            }
            else
            {
                paraList.Add(con.OutputDateFrom);
            }

            // 工程
            if (con.OutputProcess != null)
            {
                query.AppendLine("AND MGMT.PROCESS_CD IN ( ");
                for (int i = 0; i < con.OutputProcess.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.OutputProcess[i]);
                }
                query.AppendLine(" ) ");
            }


            // 版式
            if (con.Han_Type != null)
            {
                query.Append("AND JSBODR.HAN_KBN IN ( ");

                for (int i = 0; i < con.Han_Type.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Han_Type[i]);
                }
                query.AppendLine(" ) ");
            }

            query.Append("ORDER BY JSBODR.JSBDPY_NO, JSBODR.SUBNEGA_NO, MGMT.PROCESS_CD, MGMT.SUBPROCESS_CD, MGMT.COMMIT_DATE");

            return query.ToString();

        }

        #endregion

        #region ログイン

        /// <summary>
        /// ログイン情報取得クエリ
        /// </summary>
        /// <param name="con">ログイン条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <remarks>
        /// 作成者    ：  sesaki
        /// 作成日    ：  2019/11/01
        /// </remarks>
        public static string SystemLogin(Models.User.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT");
            query.AppendLine("EMPLOYEE_CD,");            // 担当者コード
            query.AppendLine("PROCESS_CD");              // 所属工程コード
            query.AppendLine("FROM PROGEMPLOYEE_MST");

            // ID
            query.AppendLine("WHERE EMPLOYEE_CD = :0");
            paraList.Add(con.Id);

            // パスワード
            query.AppendLine("AND  LOGIN_PASS = :1");
            paraList.Add(con.Password);

            return query.ToString();
        }

        #endregion

        #region 製版進捗一覧

        /// <summary>
        /// 製版進捗一覧画面 検索クエリ
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <returns>検索クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/11/11
        /// </remarks>
        public static string GetSeihanMgmtList(Models.SeihanList.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");

            query.AppendLine("    cmny.CMNY_NM");               // 1.注文内容
            query.AppendLine("  , cmkbn.CMKBN_NM");             // 2.注文区分
            query.AppendLine("  , odr.DPY_NO");                 // 3.呼出し№
            query.AppendLine("  , odr.SUBNEGA_NO");             // 4.サブネガＮｏ．
            query.AppendLine("  , tn.BURANDO_NM");              // 5.ブランド
            query.AppendLine("  , tn.SYOHIN_NM");               // 6.品名
            query.AppendLine("  , tn.KAN_NM");                  // 7.容器型
            query.AppendLine("  , tn.ZUMEN_NO");                // 8.図面No
            query.AppendLine("  , tn.HAN_KBN");                 // 9.版式

            query.AppendLine("  , NVL( TO_CHAR( p01.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS HANSHITA_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m01.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS HANSHITA_COMMIT");      // 10.版下予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p02.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS HENSHUH_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m0201.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS HENSHUH_COMMIT");     // 11.編集(編集)予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p02.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS HENSHUK_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m0202.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS HENSHUK_COMMIT");     // 12.編集(検査)予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p03.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS KENSA1_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m0301.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS KENSA1_COMMIT");      // 13.検査(工程1)予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p03.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS KENSA2_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m0302.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS KENSA2_COMMIT");      // 14.検査(工程2)予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p04.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS HKOSEI_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m04.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS HKOSEI_COMMIT");        // 15.平板校予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p05.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS KKOSEI_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m05.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS KKOSEI_COMMIT");        // 16.曲面校予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p06.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS KOSEIKENSA_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m06.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS KOSEIKENSA_COMMIT");    // 17.校正検予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p09.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS GYOUMU_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m09.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS GYOUMU_COMMIT");        // 18.業務予定/完了日

            query.AppendLine("  , p09.WORK_SHIJI");        // 19.発送指示

            query.AppendLine("FROM SHODR_TBL odr");

            query.AppendLine("INNER JOIN PROGPLAN_SEI_TBL p09 ON p09.DPY_NO = odr.DPY_NO and p09.PROCESS_CD = '09'");
            query.AppendLine("LEFT OUTER JOIN PROGPLAN_SEI_TBL p01 ON p01.DPY_NO = odr.DPY_NO and p01.PROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGPLAN_SEI_TBL p02 ON p02.DPY_NO = odr.DPY_NO and p02.PROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGPLAN_SEI_TBL p03 ON p03.DPY_NO = odr.DPY_NO and p03.PROCESS_CD = '03'");
            query.AppendLine("LEFT OUTER JOIN PROGPLAN_SEI_TBL p04 ON p04.DPY_NO = odr.DPY_NO and p04.PROCESS_CD = '04'");
            query.AppendLine("LEFT OUTER JOIN PROGPLAN_SEI_TBL p05 ON p05.DPY_NO = odr.DPY_NO and p05.PROCESS_CD = '05'");
            query.AppendLine("LEFT OUTER JOIN PROGPLAN_SEI_TBL p06 ON p06.DPY_NO = odr.DPY_NO and p06.PROCESS_CD = '06'");

            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL m01 ON m01.DPY_NO = odr.DPY_NO and m01.PROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL m0201 ON m0201.DPY_NO = odr.DPY_NO and m0201.PROCESS_CD = '02' and m0201.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL m0202 ON m0202.DPY_NO = odr.DPY_NO and m0202.PROCESS_CD = '02' and m0202.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL m0301 ON m0301.DPY_NO = odr.DPY_NO and m0301.PROCESS_CD = '03' and m0301.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL m0302 ON m0302.DPY_NO = odr.DPY_NO and m0302.PROCESS_CD = '03' and m0302.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL m04 ON m04.DPY_NO = odr.DPY_NO and m04.PROCESS_CD = '04'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL m05 ON m05.DPY_NO = odr.DPY_NO and m05.PROCESS_CD = '05'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL m06 ON m06.DPY_NO = odr.DPY_NO and m06.PROCESS_CD = '06'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_SEI_TBL m09 ON m09.DPY_NO = odr.DPY_NO and m09.PROCESS_CD = '09'");

            query.AppendLine("LEFT OUTER JOIN THNG_MST tn ON tn.NGSH_KEY = odr.NGSH_KEY");
            query.AppendLine("LEFT OUTER JOIN CMNY_MST cmny ON cmny.CMNY_CD = odr.CMNY_CD");
            query.AppendLine("LEFT OUTER JOIN CMKBN_MST cmkbn ON cmkbn.CMKBN_CD = odr.CMKBN_CD");

            // 出荷日
            query.AppendLine("WHERE p09.YOTEI_DAY BETWEEN TO_DATE(:0,'RR-MM-DD') AND TO_DATE(:1,'RR-MM-DD')");
            paraList.Add(con.YoteiDayFrom);
            if (!string.IsNullOrWhiteSpace(con.YoteiDayTo))
            {
                paraList.Add(con.YoteiDayTo);
            }
            else
            {
                paraList.Add(con.YoteiDayFrom);
            }

            // 校正区分
            if (con.Kosei_Kbn != null)
            {
                query.Append("AND odr.KSKBN_CD IN ( ");

                for (int i = 0; i < con.Kosei_Kbn.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Kosei_Kbn[i]);
                }

                query.AppendLine(" ) ");
            }
            // 版式
            if (con.Han_Type != null)
            {
                query.Append("AND tn.HAN_KBN IN ( ");

                for (int i = 0; i < con.Han_Type.Length; i++)
                {
                    if (i > 0) query.Append(",");
                    query.Append(":").Append(paraList.Count);
                    paraList.Add(con.Han_Type[i]);
                }

                query.AppendLine(" ) ");
            }

            // ｻﾌﾞﾈｶﾞNo
            if (!string.IsNullOrWhiteSpace(con.Nega_No))
            {
                query.Append("AND odr.SUBNEGA_NO LIKE '%' || :").Append(paraList.Count).AppendLine(" || '%'");
                paraList.Add(con.Nega_No);
            }

            // 伝票No
            if (!string.IsNullOrWhiteSpace(con.Dpy_No))
            {
                query.Append("AND odr.DPY_NO = :").Append(paraList.Count).AppendLine("");
                paraList.Add(con.Dpy_No);
            }

            // 品名
            if (!string.IsNullOrWhiteSpace(con.Shohin_Name))
            {
                query.Append("AND tn.SYOHIN_NM LIKE '%' || :").Append(paraList.Count).AppendLine(" || '%'");
                paraList.Add(con.Shohin_Name);
            }

            query.AppendLine("ORDER BY p09.YOTEI_DAY, odr.SUBNEGA_NO");

            return query.ToString();
        }

        #endregion

        #region グラビア進捗一覧

        /// <summary>
        /// グラビア進捗一覧画面 検索クエリ
        /// </summary>
        /// <param name="con">検索条件</param>
        /// <param name="paraList">パラメータ格納リスト</param>
        /// <returns>検索クエリ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/11/13
        /// </remarks>
        public static string GetGurabiaMgmtList(Models.GurabiaList.Condition con, ref List<object> paraList)
        {
            StringBuilder query = new StringBuilder();

            query.AppendLine("SELECT ");

            query.AppendLine("    cmny.CMNY_NM");               // 1.注文内容
            query.AppendLine("  , cmkbn.CMKBN_NM");             // 2.注文区分
            query.AppendLine("  , odr.DPY_NO");                 // 3.呼出し№
            if (con.IsTokan())
            {
                query.AppendLine("  , odr.ORDER_NO || '/' || odr.PRODUCT_CD AS SUBNEGA_NO");    // 4.サブネガＮｏ．
                query.AppendLine("  , odr.BURANDO_NM");             // 5.ブランド
                query.AppendLine("  , odr.SYOHIN_NM");              // 6.品名
                query.AppendLine("  , odr.KIKAKU");                 // 7.規格
            }
            else
            {
                query.AppendLine("  , odr.SUBNEGA_NO");             // 4.サブネガＮｏ．
                query.AppendLine("  , tn.BURANDO_NM");              // 5.ブランド
                query.AppendLine("  , tn.SYOHIN_NM");               // 6.品名
                query.AppendLine("  , tn.KAN_NM AS KIKAKU");        // 7.規格
            }
            query.AppendLine("  , kskbn.KSKBN_NM");             // 8.校正区分

            query.AppendLine("  , NVL( TO_CHAR( p01.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS HANSHITA_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m01.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS HANSHITA_COMMIT");      // 9.版下予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p02.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS HENSHUH_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m0201.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS HENSHUH_COMMIT");     // 10.編集(編集)予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( odr.DENSOU_DAY, 'yy/mm/dd' ), '-' ) AS DENSO_YOTEI");
            query.AppendLine("  , CASE m0201.DENSO_FLG WHEN '1' THEN 'レ' ELSE '' END AS DENSO_COMMIT");        // 11.電送予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p02.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS HENSHUK_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m0202.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS HENSHUK_COMMIT");     // 12.編集(検査)予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p03.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS KENSA1_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m0301.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS KENSA1_COMMIT");      // 13.検査(工程1)予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p03.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS KENSA2_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m0302.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS KENSA2_COMMIT");      // 14.検査(工程2)予定/完了日
            query.AppendLine("  , NVL( TO_CHAR( p09.YOTEI_DAY, 'yy/mm/dd' ), '-' ) AS GYOUMU_YOTEI");
            query.AppendLine("  , NVL( TO_CHAR( m09.COMMIT_DATE, 'yy/mm/dd' ), '-' ) AS GYOUMU_COMMIT");        // 15.業務予定/完了日

            query.AppendLine("  , p09.WORK_SHIJI");        // 16.発送指示

            if (con.IsTokan())
            {
                query.AppendLine("FROM TOKANODR_TBL odr");
            }
            else
            {
                query.AppendLine("FROM SHODR_TBL odr");
            }

            query.AppendLine("INNER JOIN PROGPLAN_GRA_TBL p09 ON p09.DPY_NO = odr.DPY_NO and p09.PROCESS_CD = '09'");
            query.AppendLine("LEFT OUTER JOIN PROGPLAN_GRA_TBL p01 ON p01.DPY_NO = odr.DPY_NO and p01.PROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGPLAN_GRA_TBL p02 ON p02.DPY_NO = odr.DPY_NO and p02.PROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGPLAN_GRA_TBL p03 ON p03.DPY_NO = odr.DPY_NO and p03.PROCESS_CD = '03'");

            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL m01 ON m01.DPY_NO = odr.DPY_NO and m01.PROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL m0201 ON m0201.DPY_NO = odr.DPY_NO and m0201.PROCESS_CD = '02' and m0201.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL m0202 ON m0202.DPY_NO = odr.DPY_NO and m0202.PROCESS_CD = '02' and m0202.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL m0301 ON m0301.DPY_NO = odr.DPY_NO and m0301.PROCESS_CD = '03' and m0301.SUBPROCESS_CD = '01'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL m0302 ON m0302.DPY_NO = odr.DPY_NO and m0302.PROCESS_CD = '03' and m0302.SUBPROCESS_CD = '02'");
            query.AppendLine("LEFT OUTER JOIN PROGMGMT_GRA_TBL m09 ON m09.DPY_NO = odr.DPY_NO and m09.PROCESS_CD = '09'");

            if (con.IsTokan())
            {
                query.AppendLine("LEFT OUTER JOIN TOKAN_CMNY_MST cmny ON cmny.CMNY_CD = odr.CMNY_CD");
                query.AppendLine("LEFT OUTER JOIN TOKAN_CMKBN_MST cmkbn ON cmkbn.CMKBN_CD = odr.CMKBN_CD");
                query.AppendLine("LEFT OUTER JOIN TOKAN_KSKBN_MST kskbn ON kskbn.KSKBN_CD = odr.KSKBN_CD");
            }
            else
            {
                query.AppendLine("LEFT OUTER JOIN THGNG_MST tn ON tn.NGSH_KEY = odr.NGSH_KEY");
                query.AppendLine("LEFT OUTER JOIN CMNY_MST cmny ON cmny.CMNY_CD = odr.CMNY_CD");
                query.AppendLine("LEFT OUTER JOIN CMKBN_MST cmkbn ON cmkbn.CMKBN_CD = odr.CMKBN_CD");
                query.AppendLine("LEFT OUTER JOIN KSKBN_MST kskbn ON kskbn.KSKBN_CD = odr.KSKBN_CD");
            }

            // 出荷日
            query.AppendLine("WHERE p09.YOTEI_DAY BETWEEN TO_DATE(:0,'RR-MM-DD') AND TO_DATE(:1,'RR-MM-DD')");
            paraList.Add(con.YoteiDayFrom);
            if (!string.IsNullOrWhiteSpace(con.YoteiDayTo))
            {
                paraList.Add(con.YoteiDayTo);
            }
            else
            {
                paraList.Add(con.YoteiDayFrom);
            }

            // 東洋製罐・東罐興業 条件分岐
            if (con.IsTokan())
            {
                // 校正区分
                if (con.Tokan_Kosei_Kbn != null)
                {
                    query.Append("AND odr.KSKBN_CD IN ( ");

                    for (int i = 0; i < con.Tokan_Kosei_Kbn.Length; i++)
                    {
                        if (i > 0) query.Append(",");
                        query.Append(":").Append(paraList.Count);
                        paraList.Add(con.Tokan_Kosei_Kbn[i]);
                    }

                    query.AppendLine(" ) ");
                }

            }
            else
            {
                // 得意先
                query.Append("AND odr.CUSTMER_CD <> '9904'");

                // 校正区分
                if (con.Kosei_Kbn != null)
                {
                    query.Append("AND odr.KSKBN_CD IN ( ");

                    for (int i = 0; i < con.Kosei_Kbn.Length; i++)
                    {
                        if (i > 0) query.Append(",");
                        query.Append(":").Append(paraList.Count);
                        paraList.Add(con.Kosei_Kbn[i]);
                    }

                    query.AppendLine(" ) ");
                }

            }

            // ｻﾌﾞﾈｶﾞNo
            if (!string.IsNullOrWhiteSpace(con.Nega_No))
            {
                if (con.IsTokan())
                {
                    query.Append("AND odr.ORDER_NO || '/' || odr.PRODUCT_CD");
                }
                else
                {
                    query.Append("AND odr.SUBNEGA_NO");
                }
                query.Append(" LIKE '%' || :").Append(paraList.Count).AppendLine(" || '%'");
                paraList.Add(con.Nega_No);
            }

            // 伝票No
            if (!string.IsNullOrWhiteSpace(con.Dpy_No))
            {
                query.Append("AND odr.DPY_NO = :").Append(paraList.Count).AppendLine("");
                paraList.Add(con.Dpy_No);
            }

            query.AppendLine("ORDER BY GYOUMU_YOTEI, SUBNEGA_NO");

            return query.ToString();
        }

        #endregion
    }

}