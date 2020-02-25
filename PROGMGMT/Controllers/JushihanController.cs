using PROGMGMT.Models.Jushihan;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROGMGMT.Controllers
{
    /// <summary>
    /// �����ł̌����E�o�^�ECSV�o�͉�ʂ̃R���g���[��
    /// </summary>
    /// <remarks>
    /// �쐬��    �F  kawana
    /// �쐬��    �F  2019/10/04
    /// </remarks>
    public class JushihanController : Controller
    {
        // GET: Jushihan
        public ActionResult Index()
        {
            return RedirectToAction("Login", "User");
        }

        #region �������

        [HttpGet]
        public ActionResult Search()
        {
            // ���[�U�[ID�Ȃ���΃��O�C���y�[�W��
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
            // ���[�U�[ID�Ȃ���΃��O�C���y�[�W��
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            SearchViewModel searchView = new SearchViewModel(condition);
            searchView.GetSearchResults();
            return View(searchView);
        }

        #endregion

        #region �o�^���

        [HttpGet]
        public ActionResult Register()
        {
            // ���[�U�[ID�Ȃ���΃��O�C���y�[�W��
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            string dpyno = Request.Unvalidated["dpyno"];
            string process = Request.Unvalidated["process"];
            RegisterViewModel registView = new RegisterViewModel(dpyno, process);
            return View(registView);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Register(RegisterViewModel register)
        {
            // ���[�U�[ID�Ȃ���΃��O�C���y�[�W��
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            // �T�[�o���ŕύX�������ڂ���ʂɔ��f�����邽�߂̏���
            ModelState.Clear();

            string dpyno = Request.Unvalidated["dpyno"];
            string process = Request.Unvalidated["process"];
            string uid = (string)Session["UserId"];

            bool result = register.RegisterGroup.RegistMgmt(dpyno, process, uid);
            RegisterViewModel registerView = new RegisterViewModel(dpyno, process, result);

            return View(registerView);
        }

        #endregion

        #region CSV�o�͉��
        [HttpGet]
        public ActionResult Output()
        {
            // ���[�U�[ID�Ȃ���΃��O�C���y�[�W��
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
            // ���[�U�[ID�Ȃ���΃��O�C���y�[�W��
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