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
    [Authorize(Roles = EmployeePositionConsts.Admin)]
    public class IssuingAuthoritiesController : Controller
    {
        private readonly BankDepositsContext _context;

        public IssuingAuthoritiesController(BankDepositsContext context)
        {
            _context = context;
        }

        // GET: IssuingAuthorities
        public async Task<IActionResult> Index()
        {
            return View(await _context.IssuingAuthorities.ToListAsync());
        }

        // GET: IssuingAuthorities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issuingAuthority = await _context.IssuingAuthorities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issuingAuthority == null)
            {
                return NotFound();
            }

            return View(issuingAuthority);
        }

        // GET: IssuingAuthorities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IssuingAuthorities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] IssuingAuthority issuingAuthority)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issuingAuthority);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issuingAuthority);
        }

        // GET: IssuingAuthorities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issuingAuthority = await _context.IssuingAuthorities.FindAsync(id);
            if (issuingAuthority == null)
            {
                return NotFound();
            }
            return View(issuingAuthority);
        }

        // POST: IssuingAuthorities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] IssuingAuthority issuingAuthority)
        {
            if (id != issuingAuthority.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issuingAuthority);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssuingAuthorityExists(issuingAuthority.Id))
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
            return View(issuingAuthority);
        }

        // GET: IssuingAuthorities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issuingAuthority = await _context.IssuingAuthorities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issuingAuthority == null)
            {
                return NotFound();
            }

            return View(issuingAuthority);
        }

        // POST: IssuingAuthorities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issuingAuthority = await _context.IssuingAuthorities.FindAsync(id);
            _context.IssuingAuthorities.Remove(issuingAuthority);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssuingAuthorityExists(int id)
        {
            return _context.IssuingAuthorities.Any(e => e.Id == id);
        }
    }
}
