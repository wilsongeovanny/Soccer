using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Soccer.Web.Data;
using Soccer.Web.Data.Entities;
using Soccer.Web.Helpers;
using Soccer.Web.Models;
using System;
using System.Threading.Tasks;


namespace Soccer.Web.Controllers
{
    public class TeamEntitiesController : Controller
    {
        private readonly DataContext _context;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;


        public TeamEntitiesController(DataContext context, IImageHelper imageHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: TeamEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teams.ToListAsync());
        }

        // GET: TeamEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamEntity teamEntity = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamEntity == null)
            {
                return NotFound();
            }

            return View(teamEntity);
        }

        // GET: TeamEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // borrar el bind Bind("Id,Name,LogoPath") y lococamos un try un aexception
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamViewModel teamViewModel)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;
                if (teamViewModel.LogoFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(teamViewModel.LogoFile, "Teams");
                }
                TeamEntity teamEntity = _converterHelper.ToTeamEntity(teamViewModel, path, true);
                _context.Add(teamEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, $"Ya exite teams : {teamEntity.Name}.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                    //ModelState.AddModelError(string.Empty, ex.InnerException.Message);


                }

            }
            return View(teamViewModel);
        }

        // GET: TeamEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamEntity teamEntity = await _context.Teams.FindAsync(id);
            if (teamEntity == null)
            {
                return NotFound();
            }
            TeamViewModel teamViewModel = _converterHelper.ToTeamViewModel(teamEntity);
            return View(teamViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamViewModel teamViewModel)
        {
            if (id != teamViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string path = teamViewModel.LogoPath;
                if (teamViewModel.LogoFile != null)
                {
                    path = await _imageHelper.UploadImageAsync(teamViewModel.LogoFile, "Teams");
                }
                TeamEntity teamEntity = _converterHelper.ToTeamEntity(teamViewModel, path, false);
                _context.Update(teamEntity);
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, $" ya exite teams : {teamEntity.Name}.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                    //ModelState.AddModelError(string.Empty, ex.InnerException.Message);


                }

                /*try
                {
                    _context.Update(teamEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamEntityExists(teamEntity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));*/
            }
            return View(teamViewModel);
        }

        // GET: TeamEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamEntity teamEntity = await _context.Teams
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teamEntity == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(teamEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /*
                // POST: TeamEntities/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(int id)
                {
                    TeamEntity teamEntity = await _context.Teams.FindAsync(id);

                }

                private bool TeamEntityExists(int id)
                {
                    return _context.Teams.Any(e => e.Id == id);
                }*/
    }
}
