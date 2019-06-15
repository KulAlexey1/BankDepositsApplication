using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDA.DAL.EF;
using BDA.Domain;
using Microsoft.AspNetCore.Authorization;

namespace BDA.MVC.Controllers
{
    
    public class StreetsController : Controller
    {
        private readonly BankDepositsContext _context;

        public StreetsController(BankDepositsContext context)
        {
            _context = context;
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // GET: Streets
        public async Task<IActionResult> Index()
        {
            var bankDepositsContext = _context.Streets.Include(s => s.Locality);
            return View(await bankDepositsContext.ToListAsync());
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // GET: Streets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var street = await _context.Streets
                .Include(s => s.Locality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (street == null)
            {
                return NotFound();
            }

            return View(street);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetStreetsBy(int localityId)
        {
            var streets = await _context.Streets.Where(a => a.LocalityId == localityId).ToListAsync();

            return Json(streets);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // GET: Streets/Create
        public IActionResult Create()
        {
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "LocalityName");
            return View();
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // POST: Streets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LocalityId,StreetName,Postcode,Id")] Street street)
        {
            if (ModelState.IsValid)
            {
                _context.Add(street);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "LocalityName", street.LocalityId);
            return View(street);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // GET: Streets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var street = await _context.Streets.FindAsync(id);
            if (street == null)
            {
                return NotFound();
            }
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "LocalityName", street.LocalityId);
            return View(street);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // POST: Streets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocalityId,StreetName,Postcode,Id")] Street street)
        {
            if (id != street.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(street);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreetExists(street.Id))
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
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "LocalityName", street.LocalityId);
            return View(street);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // GET: Streets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var street = await _context.Streets
                .Include(s => s.Locality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (street == null)
            {
                return NotFound();
            }

            return View(street);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // POST: Streets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var street = await _context.Streets.FindAsync(id);
            _context.Streets.Remove(street);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StreetExists(int id)
        {
            return _context.Streets.Any(e => e.Id == id);
        }
    }
}
