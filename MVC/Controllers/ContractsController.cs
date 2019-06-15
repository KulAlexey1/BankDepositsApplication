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
    public class ContractsController : Controller
    {
        private readonly BankDepositsContext _context;

        public ContractsController(BankDepositsContext context)
        {
            _context = context;
        }

        [Authorize(Roles = EmployeePositionConsts.CashierOrManagerOrAdmin)]
        // GET: Contracts
        public async Task<IActionResult> Index()
        {
            var bankDepositsContext = _context.Contracts.Include(c => c.Account).Include(c => c.Deposit).Include(c => c.Depositor).Include(c => c.Employee);
            return View(await bankDepositsContext.ToListAsync());
        }

        [Authorize(Roles = EmployeePositionConsts.CashierOrManagerOrAdmin)]
        // GET: Contracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Account)
                .Include(c => c.Deposit)
                .Include(c => c.Depositor)
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // GET: Contracts/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id");
            ViewData["DepositId"] = new SelectList(_context.Deposits, "Id", "DepositName");
            ViewData["DepositorId"] = new SelectList(_context.Depositors, "Id", "Id");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Password");
            return View();
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // POST: Contracts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepositId,DepositorId,EmployeeId,AccountId,ConclusionDate,TerminationDate,Id")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", contract.AccountId);
            ViewData["DepositId"] = new SelectList(_context.Deposits, "Id", "DepositName", contract.DepositId);
            ViewData["DepositorId"] = new SelectList(_context.Depositors, "Id", "Id", contract.DepositorId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Password", contract.EmployeeId);
            return View(contract);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // GET: Contracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", contract.AccountId);
            ViewData["DepositId"] = new SelectList(_context.Deposits, "Id", "DepositName", contract.DepositId);
            ViewData["DepositorId"] = new SelectList(_context.Depositors, "Id", "Id", contract.DepositorId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Password", contract.EmployeeId);
            return View(contract);
        }

        // POST: Contracts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = EmployeePositionConsts.Manager)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepositId,DepositorId,EmployeeId,AccountId,ConclusionDate,TerminationDate,Id")] Contract contract)
        {
            if (id != contract.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Id", contract.AccountId);
            ViewData["DepositId"] = new SelectList(_context.Deposits, "Id", "DepositName", contract.DepositId);
            ViewData["DepositorId"] = new SelectList(_context.Depositors, "Id", "Id", contract.DepositorId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Password", contract.EmployeeId);
            return View(contract);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // GET: Contracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracts
                .Include(c => c.Account)
                .Include(c => c.Deposit)
                .Include(c => c.Depositor)
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        [Authorize(Roles = EmployeePositionConsts.Manager)]
        // POST: Contracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.Id == id);
        }
    }
}
