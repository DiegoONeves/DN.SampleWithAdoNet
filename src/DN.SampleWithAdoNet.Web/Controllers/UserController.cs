using DN.SampleWithAdoNet.DomainModel;
using DN.SampleWithAdoNet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DN.SampleWithAdoNet.Web.Controllers
{
    public class UserController : Controller
    {
        private IConnectionFactory factory;
        private AdoNetContext context;
        private UserRepository userRepo;
        public UserController()
        {
            factory = new AppConfigConnectionFactory("Sample");
            context = new AdoNetContext(factory);
            userRepo = new UserRepository(context);
        }
        // GET: User
        public ActionResult Index()
        {
            var users = userRepo.ListAll();

            var viewModel = new List<UserViewModel>();
            foreach (var item in users)
            {
                viewModel.Add(new UserViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email
                });
            }

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserNewViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var domainUser = new User();
                domainUser.Id = Guid.NewGuid();
                domainUser.Name = userViewModel.Name;
                domainUser.Email = userViewModel.Email;
                domainUser.Password = userViewModel.Password;

                using (var uow = context.CreateUnitOfWork())
                {
                    userRepo.Create(domainUser);

                    uow.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }

        public ActionResult Edit(Guid id)
        {
            var userDomain = userRepo.GetById(id);
            var userViewModel = new UserUpdateViewModel();
            userViewModel.Id = userDomain.Id;
            userViewModel.Name = userDomain.Name;
            userViewModel.Email = userDomain.Email;

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserUpdateViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var domainUser = new User();
                domainUser.Id = userViewModel.Id;
                domainUser.Name = userViewModel.Name;
                domainUser.Email = userViewModel.Email;

                using (var uow = context.CreateUnitOfWork())
                {
                    userRepo.Update(domainUser);

                    uow.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }
    }
}