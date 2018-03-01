using FasTnT.Domain.Repositories;
using FasTnT.Web.Models.Masterdata;
using System.Linq;
using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class MasterDataController : Controller
    {
        private readonly IMasterdataRepository _masterdataRepository;

        public MasterDataController(IMasterdataRepository masterdataRepository)
        {
            _masterdataRepository = masterdataRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Read_Masterdata(int pageSize = 10, int page = 0)
        {
            var masterdata = _masterdataRepository.Query().Skip(page * pageSize).Take(pageSize).MapToViewModel();
            var totalCount = _masterdataRepository.Query().Count();
            var summary = new MasterdataSummary
            {
                MasterData = masterdata,
                TotalMasterDataCount = totalCount,
                ItemsPerPage = pageSize,
                CurrentPage = page
            };

            return PartialView("_ListMasterdata", summary);
        }
    }
}