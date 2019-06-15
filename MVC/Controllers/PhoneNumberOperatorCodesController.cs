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
    
    public class PhoneNumberOperatorCodesController : Controller
    {
        private readonly BankDepositsContext _context;

        public PhoneNumberOperatorCodesController(BankDepositsContext context)
        {
            _context = context;
        }

        [Authorize(Roles = EmployeePositionConsts.Admin)]
        // GET: PhoneNumberOperatorCodes
        public async Task<IActionResult> Index()
        {
            var bankDepositsContext = _context.PhoneNumberOperatorsCodes.Include(p => p.PhoneNumberOperator);
            return View(await bankDepositsContext.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetPhoneNumberOperatorCodesBy(int phoneNumberOperatorId)
        {
            var phoneNumberOperatorCodes = await _context.PhoneNumberOperatorsCodes.Where(a => a.PhoneNumberOperatorId == phoneNumberOperatorId)
                .Select(a => a.Code).ToListAsync();

            return Json(phoneNumberOperatorCodes);
        }

        [Authorize(Roles = EmployeePositionConsts.Admin)]
        // GET: PhoneNumberOperatorCodes/Create
        public IActionResult Create()
        {
            ViewData["PhoneNumberOperatorId"] = new SelectList(_context.PhoneNumberOperators, "Id", "Operator");
            return View();
        }

        [Authorize(Roles = EmployeePositionConsts.Admin)]
        // POST: PhoneNumberOperatorCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhoneNumberOperatorId,Code")] PhoneNumberOperatorCode phoneNumberOperatorCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phoneNumberOperatorCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PhoneNumberOperatorId"] = new SelectList(_context.PhoneNumberOperators, "Id", "Operator", phoneNumberOperatorCode.PhoneNumberOperatorId);
            return View(phoneNumberOperatorCode);
        }

        [Authorize(Roles = EmployeePositionConsts.Admin)]
        // GET: PhoneNumberOperatorCodes/Delete/5
        public async Task<IActionResult> Delete(int? phoneNumberOperatorId, int? code)
        {
            if (phoneNumberOperatorId == null || code == null)
            {
                return NotFound();
            }

            var phoneNumberOperatorCode = await _context.PhoneNumberOperatorsCodes
                .Include(p => p.PhoneNumberOperator)
                .FirstOrDefaultAsync(m => m.PhoneNumberOperatorId == phoneNumberOperatorId && m.Code == code);
            if (phoneNumberOperatorCode == null)
            {
                return NotFound();
            }

            return View(phoneNumberOperatorCode);
        }

        [Authorize(Roles = EmployeePositionConsts.Admin)]
        // POST: PhoneNumberOperatorCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? phoneNumberOperatorId, int? code)
        {
            var phoneNumberOperatorCode = await _context.PhoneNumberOperatorsCodes.FirstOrDefaultAsync(m => m.PhoneNumberOperatorId == phoneNumberOperatorId && m.Code == code);
            _context.PhoneNumberOperatorsCodes.Remove(phoneNumberOperatorCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneNumberOperatorCodeExists(int id)
        {
            return _context.PhoneNumberOperatorsCodes.Any(e => e.PhoneNumberOperatorId == id);
        }
    }
}
