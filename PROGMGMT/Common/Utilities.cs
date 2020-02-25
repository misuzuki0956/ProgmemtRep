using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace PROGMGMT.Common
{
    /// <summary>
    /// 共通処理クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/09/01
    /// </remarks>
    public class Utilities
    {
        /// <summary>
        /// 日付チェック（1.From未入力、2.FromTo逆転）
        /// </summary>
        /// <param name="dateFrom">日付From</param>
        /// <param name="dateTo">日付To</param>
        /// <param name="name">項目名</param>
        /// <returns>エラーメッセージ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string CheckDateFromTo(string dateFrom, string dateTo, string name)
        {
            string msg = "";
            if (string.IsNullOrEmpty(dateFrom))
            {
                msg = string.Format(Resources.TextResource.ErrorFormatDateFrom, name);
            }
            if (!string.IsNullOrEmpty(dateTo) && dateTo.CompareTo(dateFrom) < 0)
            {
                msg = string.Format(Resources.TextResource.ErrorFormatDateFromTo, name);
            }
            return msg;
        }

        /// <summary>
        /// 時間チェック（空の場合現在時刻 [HH:mm] を返す）
        /// </summary>
        /// <param name="time">時間</param>
        /// <returns>時間文字列</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string CheckWorkTime(string time)
        {
            string nowtime;
            if (string.IsNullOrWhiteSpace(time))
            {
                nowtime = DateTime.Now.ToString("HH:mm");
            }
            else
            {
                nowtime = time;
            }
            return nowtime;
        }
        /// <summary>
        /// 時間チェック（空の場合現在時刻 [HH:mm] を返す）
        /// </summary>
        /// <param name="time">時間</param>
        /// <param name="day">作業完了日</param>
        /// <returns>時間</returns>
        public static string CheckWorkTime(string time, string day)
        {
            string nowtime;
            if ((string.IsNullOrWhiteSpace(time)) && (!string.IsNullOrWhiteSpace(day)))
            {
                nowtime = DateTime.Now.ToString("HH:mm");
            }
            else
            {
                nowtime = time;
            }
            return nowtime;
        }

        /// <summary>
        /// DataTableをCSVデータに変換
        /// </summary>
        /// <param name="table">出力用データ</param>
        /// <returns>CSV用データ</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static byte[] DataTableToCsv(DataTable table)
        {
            MemoryStream stream = new MemoryStream();

            using (StreamWriter writer = new StreamWriter(stream, Encoding.GetEncoding("shift_jis")))
            {
                List<object> list = new List<object>();

                // ヘッダ
                foreach (DataColumn col in table.Columns)
                {
                    list.Add(col);
                }
                writer.WriteLine(string.Join(",", list));

                // データ
                foreach (DataRow row in table.Rows)
                {
                    list.Clear();
                    foreach (object item in row.ItemArray)
                    {
                        string str = item.ToString();
                        str = CheckNeedDoubleQuote(str);
                        list.Add(str);
                    }
                    writer.WriteLine(string.Join(",", list));
                }
            }
            return stream.ToArray();
        }

        /// <summary>
        /// CSV出力項目ダブルクォーテーション対応
        /// </summary>
        /// <param name="str">チェック項目</param>
        /// <returns>対応済み項目</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        private static string CheckNeedDoubleQuote(string str)
        {
            // チェック対象（カンマ、ダブルクォーテーション、タブ、改行）
            char[] checkArray = new char[] { ',', '"', '\t', '\r', '\n' };

            // 含まれていなければ対処不要
            if (str.IndexOfAny(checkArray) < 0)
            {
                return str;
            }

            // 項目内のダブルクォーテーション対応
            if (str.IndexOf('"') > -1)
            {
                str = str.Replace("\"", "\"\"");
            }

            return "\"" + str + "\"";
        }

        /// <summary>
        /// タイムスタンプ付きCSVファイル名取得
        /// </summary>
        /// <param name="title">ファイルタイトル</param>
        /// <returns>ファイル名</returns>
        /// <remarks>
        /// 作成者    ：  nakao
        /// 作成日    ：  2019/09/01
        /// </remarks>
        public static string GetCsvFileName(string title)
        {
            return string.Format(Resources.TextResource.FormatCsvFile, title, DateTime.Now);
        }

        /// <summary>
        /// 文字列1をtrueで返却する
        /// </summary>
        /// <param name="getStr">DB項目（資料/色見本/電送）</param>
        /// <returns>true/false</returns>
        public static bool SetStrToFlg(string getStr)
        {
            bool RtBool = false;
            if (getStr.Equals("1")) RtBool = true;
            return RtBool;
        }

        /// <summary>
        /// フラグtrue/falseを文字列で返却する
        /// </summary>
        /// <param name="flg">画面項目（資料/色見本/電送）</param>
        /// <returns>0:なし 1:有り</returns>
        public static String SetFlgToStr(bool flg)
        {
            String RtStr = "0";
            if (flg) RtStr = "1";
            return RtStr;
        }
    }
}