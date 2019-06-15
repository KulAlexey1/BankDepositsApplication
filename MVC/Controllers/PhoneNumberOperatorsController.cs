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
    public class PhoneNumberOperatorsController : Controller
    {
        private readonly BankDepositsContext _context;

        public PhoneNumberOperatorsController(BankDepositsContext context)
        {
            _context = context;
        }

        // GET: PhoneNumberOperators
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhoneNumberOperators.ToListAsync());
        }

        // GET: PhoneNumberOperators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumberOperator = await _context.PhoneNumberOperators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneNumberOperator == null)
            {
                return NotFound();
            }

            return View(phoneNumberOperator);
        }

        // GET: PhoneNumberOperators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhoneNumberOperators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Operator,Id")] PhoneNumberOperator phoneNumberOperator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phoneNumberOperator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phoneNumberOperator);
        }

        // GET: PhoneNumberOperators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumberOperator = await _context.PhoneNumberOperators.FindAsync(id);
            if (phoneNumberOperator == null)
            {
                return NotFound();
            }
            return View(phoneNumberOperator);
        }

        // POST: PhoneNumberOperators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Operator,Id")] PhoneNumberOperator phoneNumberOperator)
        {
            if (id != phoneNumberOperator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneNumberOperator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneNumberOperatorExists(phoneNumberOperator.Id))
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
            return View(phoneNumberOperator);
        }

        // GET: PhoneNumberOperators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumberOperator = await _context.PhoneNumberOperators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneNumberOperator == null)
            {
                return NotFound();
            }

            return View(phoneNumberOperator);
        }

        // POST: PhoneNumberOperators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phoneNumberOperator = await _context.PhoneNumberOperators.FindAsync(id);
            _context.PhoneNumberOperators.Remove(phoneNumberOperator);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneNumberOperatorExists(int id)
        {
            return _context.PhoneNumberOperators.Any(e => e.Id == id);
        }
    }
}
