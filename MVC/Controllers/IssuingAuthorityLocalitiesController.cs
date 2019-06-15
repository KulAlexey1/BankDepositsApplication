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
    
    public class IssuingAuthorityLocalitiesController : Controller
    {
        private readonly BankDepositsContext _context;

        public IssuingAuthorityLocalitiesController(BankDepositsContext context)
        {
            _context = context;
        }

        // GET: IssuingAuthorityLocalities
        public async Task<IActionResult> Index()
        {
            var bankDepositsContext = _context.IssuingAuthoritiesLocalities.Include(i => i.IssuingAuthority).Include(i => i.Locality);
            return View(await bankDepositsContext.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetIssuingAuthoritesBy(int? localityId)
        {
            if (localityId == null)
            {
                return NotFound();
            }

            var issuingAuthorities = await _context.IssuingAuthoritiesLocalities
                .Where(m => m.LocalityId == localityId)
                .Select(m => m.IssuingAuthority).ToListAsync();

            return Json(issuingAuthorities);
        }

        [Authorize(Roles = EmployeePositionConsts.Admin)]
        // GET: IssuingAuthorityLocalities/Create
        public IActionResult Create()
        {
            ViewData["IssuingAuthorityId"] = new SelectList(_context.IssuingAuthorities, "Id", "Name");
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "LocalityName");
            return View();
        }

        [Authorize(Roles = EmployeePositionConsts.Admin)]
        // POST: IssuingAuthorityLocalities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssuingAuthorityId,LocalityId")] IssuingAuthorityLocality issuingAuthorityLocality)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issuingAuthorityLocality);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IssuingAuthorityId"] = new SelectList(_context.IssuingAuthorities, "Id", "Name", issuingAuthorityLocality.IssuingAuthorityId);
            ViewData["LocalityId"] = new SelectList(_context.Localities, "Id", "LocalityName", issuingAuthorityLocality.LocalityId);
            return View(issuingAuthorityLocality);
        }

        [Authorize(Roles = EmployeePositionConsts.Admin)]
        // GET: IssuingAuthorityLocalities/Delete/5
        public async Task<IActionResult> Delete(int? issuingAuthorityId, int? localityId)
        {
            if (issuingAuthorityId == null || localityId == null)
            {
                return NotFound();
            }

            var issuingAuthorityLocality = await _context.IssuingAuthoritiesLocalities
                .Include(i => i.IssuingAuthority)
                .Include(i => i.Locality)
                .FirstOrDefaultAsync(m => m.IssuingAuthorityId == issuingAuthorityId && m.LocalityId == localityId);
            if (issuingAuthorityLocality == null)
            {
                return NotFound();
            }

            return View(issuingAuthorityLocality);
        }

        [Authorize(Roles = EmployeePositionConsts.Admin)]
        // POST: IssuingAuthorityLocalities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? issuingAuthorityId, int? localityId)
        {
            var issuingAuthorityLocality = await _context.IssuingAuthoritiesLocalities.FirstOrDefaultAsync(m => m.IssuingAuthorityId == issuingAuthorityId && m.LocalityId == localityId);
            _context.IssuingAuthoritiesLocalities.Remove(issuingAuthorityLocality);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssuingAuthorityLocalityExists(int id)
        {
            return _context.IssuingAuthoritiesLocalities.Any(e => e.IssuingAuthorityId == id);
        }
    }
}
