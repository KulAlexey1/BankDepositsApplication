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
    public class CurrencyConversionsController : Controller
    {
        private readonly BankDepositsContext _context;

        public CurrencyConversionsController(BankDepositsContext context)
        {
            _context = context;
        }

        // GET: CurrencyConversions
        public async Task<IActionResult> Index()
        {
            return View(await _context.CurrencyConversions.ToListAsync());
        }

        // GET: CurrencyConversions/Edit/5
        public async Task<IActionResult> Edit(Currency currency, bool buy, decimal amount)
        {

            var currencyConversion = await _context.CurrencyConversions.FirstOrDefaultAsync(a => a.Currency == currency && a.Buy == buy && a.AmountInNativeCurrencyPerUnit.Equals(amount));
            if (currencyConversion == null)
            {
                return NotFound();
            }
            return View(currencyConversion);
        }

        // POST: CurrencyConversions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Currency id, [Bind("Currency,Buy,AmountInNativeCurrencyPerUnit")] CurrencyConversion currencyConversion)
        {
            if (id != currencyConversion.Currency)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(currencyConversion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyConversionExists(currencyConversion.Currency))
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
            return View(currencyConversion);
        }

        private bool CurrencyConversionExists(Currency id)
        {
            return _context.CurrencyConversions.Any(e => e.Currency == id);
        }
    }
}
