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
    [Authorize]
    public class DepositsController : Controller
    {
        private readonly BankDepositsContext _context;

        public DepositsController(BankDepositsContext context)
        {
            _context = context;
        }

        // GET: Deposits
        public async Task<IActionResult> Index()
        {
            var bankDepositsContext = _context.Deposits.Include(d => d.DepositTerm);
            return View(await bankDepositsContext.ToListAsync());
        }

        // GET: Deposits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits
                .Include(d => d.DepositTerm)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        [Authorize(Roles = EmployeePositionConsts.Admin)]
        // GET: Deposits/Create
        public IActionResult Create()
        {
            ViewBag.DepositTerms = _context.DepositTerms.ToList().Select((d) => {
                if (d.DaysAmount != 0)
                {
                    return new SelectListItem($"{d.DaysAmount.ToString()} д.", d.Id.ToString());
                }
                else if (d.MonthsAmount != 0)
                {
                    return new SelectListItem($"{d.MonthsAmount.ToString()} мес.", d.Id.ToString());
                }

                return new SelectListItem($"{d.YearsAmount.ToString()} г.", d.Id.ToString());

            });

            ViewBag.Currencies = CurrencyConsts.Dict.Select(s => new SelectListItem(s.Value, s.Key.ToString()));

            return View();
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = EmployeePositionConsts.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepositName,PaymentPeriod,DepositTermId,Rate,AccountReplenishment,Currency,Id")] Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deposit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DepositTerms = _context.DepositTerms.ToList().Select((d) => {
                if (d.DaysAmount != 0)
                {
                    return new SelectListItem($"{d.DaysAmount.ToString()} дней", d.Id.ToString());
                }
                else if (d.MonthsAmount != 0)
                {
                    return new SelectListItem($"{d.MonthsAmount.ToString()} месяцев", d.Id.ToString());
                }

                return new SelectListItem($"{d.YearsAmount.ToString()} лет", d.Id.ToString());

            });

            ViewBag.Currencies = CurrencyConsts.Dict.Select(s => new SelectListItem(s.Value, s.Key.ToString()));

            return View(deposit);
        }

        // GET: Deposits/Delete/5
        [Authorize(Roles = EmployeePositionConsts.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deposit = await _context.Deposits
                .Include(d => d.DepositTerm)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deposit == null)
            {
                return NotFound();
            }

            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [Authorize(Roles = EmployeePositionConsts.Admin)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deposit = await _context.Deposits.FindAsync(id);
            _context.Deposits.Remove(deposit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepositExists(int id)
        {
            return _context.Deposits.Any(e => e.Id == id);
        }
    }
}
