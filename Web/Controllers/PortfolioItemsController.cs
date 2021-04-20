using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Web.ViewModel;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;

namespace Web.Controllers
{
    public class PortfolioItemsController : Controller
    {
        private readonly IUnitOfWork<PortfolioItem> _portofolio;
        private readonly IHostingEnvironment _hosting;

        public PortfolioItemsController(IUnitOfWork<PortfolioItem> portofolio, IHostingEnvironment hosting)
        {
            _portofolio = portofolio;
            _hosting = hosting;
        }

        // GET: PortfolioItems
        public IActionResult Index()
        {
            return View(_portofolio.Entity.GetAll());
        }

        // GET: PortfolioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portofolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // GET: PortfolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PortoflioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File !=null)
                {
                    string upload = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(upload,model.File.FileName);
                    model.File.CopyTo(new FileStream (fullPath,FileMode.Create));
                }
                PortfolioItem portfolioItem = new PortfolioItem
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.File.FileName
                };
                _portofolio.Entity.Insert(portfolioItem);
                _portofolio.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portofolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }
            PortoflioViewModel portoflioViewModel = new PortoflioViewModel
            {
                Id = portfolioItem.Id,
                ProjectName = portfolioItem.ProjectName,
                Description = portfolioItem.Description,
                ImageUrl = portfolioItem.ImageUrl

            };
            return View(portoflioViewModel);
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id,PortoflioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string upload = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(upload, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                    PortfolioItem portfolioItem = new PortfolioItem
                    {
                        Id = model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };
                    _portofolio.Entity.Update(portfolioItem);
                    _portofolio.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfolioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portofolio.Entity.GetById(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {

            _portofolio.Entity.Delete(id);
            _portofolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioItemExists(Guid id)
        {
            return _portofolio.Entity.GetAll().Any(e => e.Id == id);
        }
    }
}
