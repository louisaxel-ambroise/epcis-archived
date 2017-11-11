using FasTnT.Domain.Repositories;
using FasTnT.Web.Models.Users;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Read_Users()
        {
            var model = _userRepository.Query().MapToUserViewModel();

            return PartialView("_UserList", model.ToList());
        }
    }
}