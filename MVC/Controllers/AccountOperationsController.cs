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
    public class AccountOperationsController : Controller
    {
        private readonly BankDepositsContext _context;

        public AccountOperationsController(BankDepositsContext context)
        {
            _context = context;
        }

        // GET: AccountOperations
        public async Task<IActionResult> Index()
        {
            var bankDepositsContext = _context.AccountOperations.Include(a => a.Account);
            return View(await bankDepositsContext.ToListAsync());
        }

        // GET: AccountOperations/Details/5
        [Authorize(Roles = EmployeePositionConsts.CashierOrManagerOrAdmin)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountOperation = await _context.AccountOperations
                .Include(a => a.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountOperation == null)
            {
                return NotFound();
            }

            return View(accountOperation);
        }

        [Authorize(Roles = EmployeePositionConsts.CashierOrManager)]
        // GET: AccountOperations/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id");
            return View();
        }

        // POST: AccountOperations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = EmployeePositionConsts.CashierOrManager)]
        public async Task<IActionResult> Create([Bind("AccountId,DateTime,Amount,Type,Id")] AccountOperation accountOperation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountOperation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", accountOperation.AccountId);
            return View(accountOperation);
        }

        private bool AccountOperationExists(int id)
        {
            return _context.AccountOperations.Any(e => e.Id == id);
        }
    }
}
