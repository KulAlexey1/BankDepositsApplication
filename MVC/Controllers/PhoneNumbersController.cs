﻿using System;
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
    [Authorize(Roles = EmployeePositionConsts.Manager)]
    public class PhoneNumbersController : Controller
    {
        private readonly BankDepositsContext _context;

        public PhoneNumbersController(BankDepositsContext context)
        {
            _context = context;
        }

        // GET: PhoneNumbers
        public async Task<IActionResult> Index()
        {
            var bankDepositsContext = _context.PhoneNumbers.Include(p => p.Operator);
            return View(await bankDepositsContext.ToListAsync());
        }

        // GET: PhoneNumbers/Create
        public IActionResult Create()
        {
            ViewData["OperatorId"] = new SelectList(_context.PhoneNumberOperators, "Id", "Operator");
            return View();
        }

        // POST: PhoneNumbers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OperatorId,OperatorCode,Number,Id")] PhoneNumber phoneNumber)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phoneNumber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OperatorId"] = new SelectList(_context.PhoneNumberOperators, "Id", "Operator", phoneNumber.OperatorId);
            return View(phoneNumber);
        }

        // GET: PhoneNumbers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }
            ViewData["OperatorId"] = new SelectList(_context.PhoneNumberOperators, "Id", "Operator", phoneNumber.OperatorId);
            return View(phoneNumber);
        }

        // POST: PhoneNumbers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OperatorId,OperatorCode,Number,Id")] PhoneNumber phoneNumber)
        {
            if (id != phoneNumber.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneNumber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneNumberExists(phoneNumber.Id))
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
            ViewData["OperatorId"] = new SelectList(_context.PhoneNumberOperators, "Id", "Operator", phoneNumber.OperatorId);
            return View(phoneNumber);
        }

        // GET: PhoneNumbers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneNumber = await _context.PhoneNumbers
                .Include(p => p.Operator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            return View(phoneNumber);
        }

        // POST: PhoneNumbers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);
            _context.PhoneNumbers.Remove(phoneNumber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneNumberExists(int id)
        {
            return _context.PhoneNumbers.Any(e => e.Id == id);
        }
    }
}
