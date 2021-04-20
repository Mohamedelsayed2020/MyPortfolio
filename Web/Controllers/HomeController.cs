using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModel;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortfolioItem> _portfolioItem;

        public HomeController(
            IUnitOfWork<Owner> owner,
            IUnitOfWork<PortfolioItem> portfolioItem
            )
        {
            _owner = owner;
            _portfolioItem = portfolioItem;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                owner = _owner.Entity.GetAll().First(),
                portfolioItems = _portfolioItem.Entity.GetAll().ToList()
            };
            return View(homeViewModel);
        }
    }
}
