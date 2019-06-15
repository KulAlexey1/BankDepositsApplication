using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BDA.Core;
using BDA.Domain;
using BDA.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BDA.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IIssuingAuthorityRepository issuingAuthorityRepository;
        private readonly ILocalityRepository localityRepository;
        private readonly IPhoneNumberOperatorRepository phoneNumberOperatorRepository;
        private readonly IIssuingAuthorityLocalityRepository issuingAuthorityLocalityRepository;
        private readonly IStreetRepository streetRepository;

        public AccountController(
            IEmployeeRepository employeeRepository,
            IIssuingAuthorityRepository issuingAuthorityRepository,
            ILocalityRepository localityRepository,
            IPhoneNumberOperatorRepository phoneNumberOperatorRepository,
            IIssuingAuthorityLocalityRepository issuingAuthorityLocalityRepository,
            IStreetRepository streetRepository)
        {
            this.employeeRepository = employeeRepository;
            this.issuingAuthorityRepository = issuingAuthorityRepository;
            this.localityRepository = localityRepository;
            this.phoneNumberOperatorRepository = phoneNumberOperatorRepository;
            this.issuingAuthorityLocalityRepository = issuingAuthorityLocalityRepository;
            this.streetRepository = streetRepository;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Depositors");
            }

            ViewBag.Citizenships = CitizenshipConsts.Dict.Select(a => new SelectListItem(a.Value, a.Key.ToString()));
            
            var localities = await localityRepository.GetBy().ToListAsync();
            ViewBag.Localities = localities.Select(a => new SelectListItem($"{a.Region} {a.LocalityName}", a.Id.ToString()));

            var firstLocality = localities.FirstOrDefault();

            if (firstLocality != null)
            {
                var issuingAuthorityLocalities = await issuingAuthorityLocalityRepository.GetBy(a => a.LocalityId == firstLocality.Id, a => a.IssuingAuthority).ToListAsync();
                ViewBag.IssuingAuthorities = issuingAuthorityLocalities
                    .Select(a => new SelectListItem(a.IssuingAuthority.Name, a.IssuingAuthority.Id.ToString()));

                var streets = await streetRepository.GetBy(a => a.LocalityId == firstLocality.Id).ToListAsync();
                ViewBag.Streets = streets.Select(a => new SelectListItem($"{a.Postcode}, {a.StreetName}", a.Id.ToString()));
            }

            var phoneNumberOperators = await phoneNumberOperatorRepository.GetBy(null, a => a.PhoneNumberOperatorsCodes).ToListAsync();
            ViewBag.PhoneNumberOperators = phoneNumberOperators.Select(a => new SelectListItem(a.Operator, a.Id.ToString()));
            ViewBag.PhoneNumberOperatorCodes = phoneNumberOperators.FirstOrDefault()?.PhoneNumberOperatorsCodes.Select(a => new SelectListItem(a.Code.ToString(), a.Code.ToString()));

            return View(new Employee());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Employee model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Depositors");
            }

            model.Position = EmployeePosition.Employee;

            employeeRepository.Create(model);
            await employeeRepository.Commit();


            return RedirectToAction("SignIn", "Account");
        }

        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Depositors");
            }
            var model = new SignInViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(SignInViewModel signInViewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Depositors");
            }

            var employee = await employeeRepository.GetBy(a => a.Email == signInViewModel.Email && a.Password == signInViewModel.Password, a => a.Passport).FirstOrDefaultAsync();
            if (employee == null)
            {
                ModelState.AddModelError("", "Неверная электронная почта или пароль.");
            }

            if (!ModelState.IsValid)
            {
                return View(signInViewModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, signInViewModel.Email),
                new Claim(ClaimTypes.Name, employee.Passport.ShortFullName),
                new Claim(ClaimTypes.Role, employee.Position.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2),
                IsPersistent = true,
                IssuedUtc = DateTimeOffset.UtcNow,
                RedirectUri = "/Account/SignIn"
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Depositors");
        }

        [Authorize]
        public async Task<ActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("SignIn", "Account");
        }

        [Authorize]
        public async Task<IActionResult> Details()
        {
            var employeeEmail = User.FindFirst(ClaimTypes.Email).Value;
            var employee = await employeeRepository.GetBy(a => a.Email == employeeEmail)
                .Include(a=>a.Passport)
                .ThenInclude(a=>a.IssuingAuthorityLocality).ThenInclude(a => a.Locality)
                .Include(a => a.Address)
                .ThenInclude(a => a.Street).
                ThenInclude(a => a.Locality).
                Include(a => a.PhoneNumber).FirstOrDefaultAsync();

            if (employee == null)
            {
                return await SignOut();
            }

            return View(employee);
        }

        [Authorize]
        public async Task<IActionResult> EditDetails()
        {
            var employeeEmail = User.FindFirst(ClaimTypes.Email).Value;
            var employee = await employeeRepository.GetBy(a => a.Email == employeeEmail).FirstOrDefaultAsync();

            if (employee == null)
            {
                return await SignOut();
            }

            return View(employee);
        }
    }
}