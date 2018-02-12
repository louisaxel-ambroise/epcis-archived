using FasTnT.Domain.Model.Users;
using FasTnT.Domain.Repositories;
using FasTnT.Domain.Services.Users.Dashboard;
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
        private readonly IApiUserUpdator _apiUserUpdator;

        public UsersController(IUserRepository userRepository, IApiUserUpdator apiUserUpdator)
        {
            _userRepository = userRepository ?? throw new ArgumentException(nameof(userRepository));
            _apiUserUpdator = apiUserUpdator ?? throw new ArgumentException(nameof(apiUserUpdator));
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Read_Users()
        {
            var model = _userRepository.Query()
                .Where(u => u.Role == UserType.ApiUser)
                .MapToApiUserViewModel();

            return PartialView("_UserList", model.ToList());
        }

        public ActionResult Details(Guid id)
        {
            if (id == null || id == Guid.Empty) return RedirectToAction("List");

            var user = _userRepository.Load(id);
            return View(user.MapToApiUserViewModel());
        }

        public ActionResult Edit(Guid id)
        {
            if(id == null || id == Guid.Empty) return RedirectToAction("List");

            var user = _userRepository.Load(id);
            return View(user.MapToApiUserViewModel());
        }

        [HttpPost]
        public ActionResult Edit(Guid id, ApiUserViewModel model)
        {
            if (id != null && id != Guid.Empty)
            {
                if (!string.IsNullOrEmpty(model.Password) && !model.Password.Equals(model.PasswordConfirmation, StringComparison.InvariantCulture))
                {
                    return View(model); // TODO: add password mismatch error
                }

                _apiUserUpdator.Update(id, model.Name, model.IsActive, model.Password);
            }

            return RedirectToAction("List");
        }
    }
}