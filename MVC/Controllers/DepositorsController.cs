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
    public class DepositorsController : Controller
    {
        private readonly BankDepositsContext _context;

        public DepositorsController(BankDepositsContext context)
        {
            _context = context;
        }

        // GET: Depositors
        public async Task<IActionResult> Index()
        {
            var bankDepositsContext = _context.Depositors.Include(d => d.Address).ThenInclude(d => d.Street).ThenInclude(d => d.Locality)
                .Include(d => d.Passport).Include(d => d.PhoneNumber).ThenInclude(d => d.Operator);
            return View(await bankDepositsContext.ToListAsync());
        }

        // GET: Depositors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositor = await _context.Depositors
                .Include(d => d.Address).ThenInclude(s => s.Street).ThenInclude(s => s.Locality)
                .Include(d => d.Passport)
                .Include(d => d.PhoneNumber).ThenInclude(s => s.Operator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depositor == null)
            {
                return NotFound();
            }

            return View(depositor);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // GET: Depositors/Create
        public IActionResult Create()
        {
            ViewBag.Addresses = _context.Addresses.Include(s => s.Street).Select(s => new SelectListItem($"{s.Street.Locality.LocalityName} {s.Street.StreetName} {s.House} {s.Housing}", s.Id.ToString()));
            ViewBag.Passports = _context.Passports.Select(s => new SelectListItem(s.FullName, s.Id.ToString()));
            ViewBag.PhoneNumbers = _context.PhoneNumbers.Include(s => s.Operator).Select(s => new SelectListItem($"{s.Operator.Operator} {s.OperatorCode} {s.Number}", s.Id.ToString()));
            return View();
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // POST: Depositors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassportId,AddressId,PhoneNumberId,Email,Id")] Depositor depositor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(depositor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Addresses = _context.Addresses.Include(s => s.Street).Select(s => new SelectListItem($"{s.Street.Locality.LocalityName} {s.Street.StreetName} {s.House} {s.Housing}", s.Id.ToString()));
            ViewBag.Passports = _context.Passports.Select(s => new SelectListItem(s.FullName, s.Id.ToString()));
            ViewBag.PhoneNumbers = _context.PhoneNumbers.Include(s => s.Operator).Select(s => new SelectListItem($"{s.Operator.Operator} {s.OperatorCode} {s.Number}", s.Id.ToString()));
            return View(depositor);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // GET: Depositors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositor = await _context.Depositors.FindAsync(id);
            if (depositor == null)
            {
                return NotFound();
            }
            ViewBag.Addresses = _context.Addresses.Include(s => s.Street).Select(s => new SelectListItem($"{s.Street.Locality.LocalityName} {s.Street.StreetName} {s.House} {s.Housing}", s.Id.ToString()));
            ViewBag.Passports = _context.Passports.Select(s => new SelectListItem(s.FullName, s.Id.ToString()));
            ViewBag.PhoneNumbers = _context.PhoneNumbers.Include(s => s.Operator).Select(s => new SelectListItem($"{s.Operator.Operator} {s.OperatorCode} {s.Number}", s.Id.ToString()));
            return View(depositor);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // POST: Depositors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassportId,AddressId,PhoneNumberId,Email,Id")] Depositor depositor)
        {
            if (id != depositor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(depositor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepositorExists(depositor.Id))
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
            ViewBag.Addresses = _context.Addresses.Include(s => s.Street).Select(s => new SelectListItem($"{s.Street.Locality.LocalityName} {s.Street.StreetName} {s.House} {s.Housing}", s.Id.ToString()));
            ViewBag.Passports = _context.Passports.Select(s => new SelectListItem(s.FullName, s.Id.ToString()));
            ViewBag.PhoneNumbers = _context.PhoneNumbers.Include(s => s.Operator).Select(s => new SelectListItem($"{s.Operator.Operator} {s.OperatorCode} {s.Number}", s.Id.ToString()));
            return View(depositor);
        }

        // GET: Depositors/Delete/5
        [Authorize(Roles = EmployeePositionConsts.Manager)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var depositor = await _context.Depositors
                .Include(d => d.Address)
                .Include(d => d.Passport)
                .Include(d => d.PhoneNumber)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (depositor == null)
            {
                return NotFound();
            }

            return View(depositor);
        }

        // POST: Depositors/Delete/5
        [Authorize(Roles = EmployeePositionConsts.Manager)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var depositor = await _context.Depositors.FindAsync(id);
            _context.Depositors.Remove(depositor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepositorExists(int id)
        {
            return _context.Depositors.Any(e => e.Id == id);
        }
    }
}
