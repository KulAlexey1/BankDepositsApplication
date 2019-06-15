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
using BDA.Core;

namespace BDA.MVC.Controllers
{
    [Authorize(Roles = EmployeePositionConsts.Manager)]
    public class PassportsController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IIssuingAuthorityRepository issuingAuthorityRepository;
        private readonly ILocalityRepository localityRepository;
        private readonly IPhoneNumberOperatorRepository phoneNumberOperatorRepository;
        private readonly IIssuingAuthorityLocalityRepository issuingAuthorityLocalityRepository;
        private readonly IStreetRepository streetRepository;
        private readonly BankDepositsContext _context;

        public PassportsController(BankDepositsContext context,
            IEmployeeRepository employeeRepository,
            IIssuingAuthorityRepository issuingAuthorityRepository,
            ILocalityRepository localityRepository,
            IPhoneNumberOperatorRepository phoneNumberOperatorRepository,
            IIssuingAuthorityLocalityRepository issuingAuthorityLocalityRepository,
            IStreetRepository streetRepository)
        {
            _context = context;
            this.employeeRepository = employeeRepository;
            this.issuingAuthorityRepository = issuingAuthorityRepository;
            this.localityRepository = localityRepository;
            this.phoneNumberOperatorRepository = phoneNumberOperatorRepository;
            this.issuingAuthorityLocalityRepository = issuingAuthorityLocalityRepository;
            this.streetRepository = streetRepository;
        }

        // GET: Passports
        public async Task<IActionResult> Index()
        {
            var bankDepositsContext = _context.Passports.Include(p => p.IssuingAuthorityLocality);
            return View(await bankDepositsContext.ToListAsync());
        }

        // GET: Passports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passport = await _context.Passports
                .Include(p => p.IssuingAuthorityLocality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passport == null)
            {
                return NotFound();
            }

            return View(passport);
        }

        // GET: Passports/Create
        public async Task<IActionResult> Create()
        {
            var localities = await localityRepository.GetBy().ToListAsync();
            ViewBag.Localities = localities.Select(a => new SelectListItem($"{a.Region} {a.LocalityName}", a.Id.ToString()));

            var firstLocality = localities.FirstOrDefault();

            if (firstLocality != null)
            {
                var issuingAuthorityLocalities = await issuingAuthorityLocalityRepository.GetBy(a => a.LocalityId == firstLocality.Id, a => a.IssuingAuthority).ToListAsync();
                ViewBag.IssuingAuthorities = issuingAuthorityLocalities
                    .Select(a => new SelectListItem(a.IssuingAuthority.Name, a.IssuingAuthority.Id.ToString()));
            }

            ViewBag.Citizenships = CitizenshipConsts.Dict.Select(c => new SelectListItem(c.Value, c.Key.ToString()));
            return View();
        }

        // POST: Passports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,MiddleName,LastName,BirthDate,Gender,CitizenshipId,Number,IdentificationNumber,IssuingAuthorityId,IssuingAuthorityLocalityId,IssueDate,ExpirationDate,Citizenship,Id")] Passport passport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var localities = await localityRepository.GetBy().ToListAsync();
            ViewBag.Localities = localities.Select(a => new SelectListItem($"{a.Region} {a.LocalityName}", a.Id.ToString()));

            var firstLocality = localities.FirstOrDefault();

            if (firstLocality != null)
            {
                var issuingAuthorityLocalities = await issuingAuthorityLocalityRepository.GetBy(a => a.LocalityId == firstLocality.Id, a => a.IssuingAuthority).ToListAsync();
                ViewBag.IssuingAuthorities = issuingAuthorityLocalities
                    .Select(a => new SelectListItem(a.IssuingAuthority.Name, a.IssuingAuthority.Id.ToString()));
            }

            ViewBag.Citizenships = CitizenshipConsts.Dict.Select(c => new SelectListItem(c.Value, c.Key.ToString()));

            return View(passport);
        }

        // GET: Passports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passport = await _context.Passports.FindAsync(id);
            if (passport == null)
            {
                return NotFound();
            }

            var localities = await localityRepository.GetBy().ToListAsync();
            ViewBag.Localities = localities.Select(a => new SelectListItem($"{a.Region} {a.LocalityName}", a.Id.ToString()));

            var firstLocality = localities.FirstOrDefault();

            if (firstLocality != null)
            {
                var issuingAuthorityLocalities = await issuingAuthorityLocalityRepository.GetBy(a => a.LocalityId == firstLocality.Id, a => a.IssuingAuthority).ToListAsync();
                ViewBag.IssuingAuthorities = issuingAuthorityLocalities
                    .Select(a => new SelectListItem(a.IssuingAuthority.Name, a.IssuingAuthority.Id.ToString()));
            }

            ViewBag.Citizenships = CitizenshipConsts.Dict.Select(c => new SelectListItem(c.Value, c.Key.ToString()));

            return View(passport);
        }

        // POST: Passports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,MiddleName,LastName,BirthDate,Gender,CitizenshipId,Number,IdentificationNumber,IssuingAuthorityId,IssuingAuthorityLocalityId,IssueDate,ExpirationDate,Citizenship,Id")] Passport passport)
        {
            if (id != passport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassportExists(passport.Id))
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

            var localities = await localityRepository.GetBy().ToListAsync();
            ViewBag.Localities = localities.Select(a => new SelectListItem($"{a.Region} {a.LocalityName}", a.Id.ToString()));

            var firstLocality = localities.FirstOrDefault();

            if (firstLocality != null)
            {
                var issuingAuthorityLocalities = await issuingAuthorityLocalityRepository.GetBy(a => a.LocalityId == firstLocality.Id, a => a.IssuingAuthority).ToListAsync();
                ViewBag.IssuingAuthorities = issuingAuthorityLocalities
                    .Select(a => new SelectListItem(a.IssuingAuthority.Name, a.IssuingAuthority.Id.ToString()));
            }

            ViewBag.Citizenships = CitizenshipConsts.Dict.Select(c => new SelectListItem(c.Value, c.Key.ToString()));

            return View(passport);
        }

        // GET: Passports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passport = await _context.Passports
                .Include(p => p.IssuingAuthorityLocality)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passport == null)
            {
                return NotFound();
            }

            return View(passport);
        }

        // POST: Passports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passport = await _context.Passports.FindAsync(id);
            _context.Passports.Remove(passport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassportExists(int id)
        {
            return _context.Passports.Any(e => e.Id == id);
        }
    }
}
