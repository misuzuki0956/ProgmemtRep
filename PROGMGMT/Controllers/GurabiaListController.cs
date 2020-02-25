using System.Web.Mvc;
using PROGMGMT.Models.GurabiaList;

namespace PROGMGMT.Controllers
{
    /// <summary>
    /// グラビア進捗一覧画面のコントローラ
    /// </summary>
    /// <remarks>
    /// 作成者    ：  nakao
    /// 作成日    ：  2019/11/13
    /// </remarks>
    public class GurabiaListController : Controller
    {
        // GET: GurabiaList
        public ActionResult Index()
        {
            return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult Search()
        {
            // ユーザーIDなければログインページへ
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            SearchViewModel viewModel = new SearchViewModel();
            return View(viewModel);
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
    }
}