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
    public class DepositTermsController : Controller
    {
        private readonly BankDepositsContext _context;

        public DepositTermsController(BankDepositsContext context)
        {
            _context = context;
        }

        // GET: DepositTerms
        public async Task<IActionResult> Index()
        {
            return View(await _context.DepositTerms.ToListAsync());
        }

        // GET: DepositTerms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositTerm = await _context.DepositTerms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depositTerm == null)
            {
                return NotFound();
            }

            return View(depositTerm);
        }

        // GET: DepositTerms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DepositTerms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DaysAmount,MonthsAmount,YearsAmount,Id")] DepositTerm depositTerm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depositTerm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(depositTerm);
        }

        // GET: DepositTerms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositTerm = await _context.DepositTerms.FindAsync(id);
            if (depositTerm == null)
            {
                return NotFound();
            }
            return View(depositTerm);
        }

        // POST: DepositTerms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DaysAmount,MonthsAmount,YearsAmount,Id")] DepositTerm depositTerm)
        {
            if (id != depositTerm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depositTerm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepositTermExists(depositTerm.Id))
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
            return View(depositTerm);
        }

        // GET: DepositTerms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositTerm = await _context.DepositTerms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depositTerm == null)
            {
                return NotFound();
            }

            return View(depositTerm);
        }

        // POST: DepositTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depositTerm = await _context.DepositTerms.FindAsync(id);
            _context.DepositTerms.Remove(depositTerm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepositTermExists(int id)
        {
            return _context.DepositTerms.Any(e => e.Id == id);
        }
    }
}
