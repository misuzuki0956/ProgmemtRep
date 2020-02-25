namespace PROGMGMT.Common
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/09/01
    /// </remarks>
    public class Constants
    {
        // 製品コード
        public const string PRODUCT_SEIHAN      = "01";  // 製版
        public const string PRODUCT_GURABIA     = "02";  // グラビア
        public const string PRODUCT_SAPPAN      = "03";  // 刷版
        public const string PRODUCT_JUSHIHAN    = "04";  // 樹脂版

        // 工程コード
        public const string PROCESS_HANSHITA    = "01";  // 版下
        public const string PROCESS_HENSHU      = "02";  // 編集
        public const string PROCESS_KENSA       = "03";  // 検査
        public const string PROCESS_HKOSEI      = "04";  // 平板校正
        public const string PROCESS_KKOSEI      = "05";  // 曲面校正
        public const string PROCESS_KOSEIKENSA  = "06";  // 校正検査
        public const string PROCESS_SPNSEIZO    = "07";  // 刷版製造
        public const string PROCESS_SPNKENSA    = "08";  // 刷版製造
        public const string PROCESS_GYOUMU      = "09";  // 業務課出荷

        // Sub工程コード
        public const string PROCESS_SUB1        = "01";  // サブ１
        public const string PROCESS_SUB2        = "02";  // サブ２
    }
}