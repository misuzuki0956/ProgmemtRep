using System.Web.Mvc;
using PROGMGMT.Models.Gurabia;

namespace PROGMGMT.Controllers
{
    /// <summary>
    /// グラビアの検索・登録・CSV出力画面のコントローラ
    /// </summary>
    /// <remarks>
    /// 作成者    ：  sesaki
    /// 作成日    ：  2019/11/13
    /// </remarks>
    public class GurabiaController : Controller
    {
        /// GET: Gurabia
        public ActionResult Index()
        {
            return RedirectToAction("Login", "User");
        }


        #region 検索画面

        [HttpGet]
        public ActionResult Search()
        {
            // ユーザーIDなければログインページへ
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            SearchViewModel searchView = new SearchViewModel();
            return View(searchView);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Search(Condition condition)
        {
            // ユーザーIDなければログインページへ
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            SearchViewModel searchView = new SearchViewModel(condition);
            searchView.GetSearchResults();
            return View(searchView);
        }
        #endregion


        #region 登録画面

        [HttpGet]
        public ActionResult Register()
        {
            // ユーザーIDなければログインページへ
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string dpyno = Request.Unvalidated["dpyno"];
            string process = Request.Unvalidated["process"];
            string customer = Request.Unvalidated["customer"];
            RegisterViewModel registView = new RegisterViewModel(dpyno, process, customer);
            return View(registView);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Register(RegisterViewModel register)
        {
            // ユーザーIDなければログインページへ
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            // サーバ側で変更した項目を画面に反映させるための処理
            ModelState.Clear();

            string dpyno = Request.Unvalidated["dpyno"];
            string process = Request.Unvalidated["process"];
            string customer = Request.Unvalidated["customer"];
            string uid = (string)Session["UserId"];

            bool result = register.RegisterGroup.RegistMgmt(dpyno, process, uid);
            RegisterViewModel registerView = new RegisterViewModel(dpyno, process, customer, result);

            return View(registerView);
        }
        #endregion


        #region CSV出力画面

        [HttpGet]
        public ActionResult Output()
        {
            // ユーザーIDなければログインページへ
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            CsvOutputModel outputView = new CsvOutputModel();
            return View(outputView);
        }

        [HttpPost]
        public ActionResult Output(CsvOutputModel model)
        {
            // ユーザーIDなければログインページへ
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            CsvOutputModel outputView = new CsvOutputModel(model.Condition);
            if (outputView.GetCsvData())
            {
                return File(outputView.CsvData, "text/csv", outputView.CsvName);
            }
            return View(outputView);
        }
        #endregion
    }
}

