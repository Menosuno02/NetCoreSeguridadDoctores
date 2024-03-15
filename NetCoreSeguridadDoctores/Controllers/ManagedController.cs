using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NetCoreSeguridadDoctores.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using NetCoreSeguridadDoctores.Models;


namespace NetCoreSeguridadDoctores.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryDoctores repo;

        public ManagedController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login
            (string apellido, int iddoctor)
        {
            Doctor doctor = await this.repo.LogInDoctor(apellido, iddoctor);
            if (doctor != null)
            {
                ClaimsIdentity identity =
                    new ClaimsIdentity
                    (CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                Claim claimName = new Claim(ClaimTypes.Name, doctor.Apellido);
                Claim claimDoctorNo = new Claim(ClaimTypes.NameIdentifier, doctor.DoctorNo.ToString());
                Claim claimHospitalCod = new Claim("HospitalCod", doctor.HospitalCod.ToString());
                Claim claimEspecialidad = new Claim(ClaimTypes.Role, doctor.Especialidad);
                Claim claimSalario = new Claim("Salario", doctor.Salario.ToString());
                identity.AddClaim(claimName);
                identity.AddClaim(claimDoctorNo);
                identity.AddClaim(claimHospitalCod);
                identity.AddClaim(claimEspecialidad);
                identity.AddClaim(claimSalario);
                // Incluimos Claim inventado que solo tendrá un doctor
                if (doctor.DoctorNo == 111)
                {
                    identity.AddClaim(
                        new Claim("Administrador", "Soy el admin"));
                }
                ClaimsPrincipal userPricipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPricipal);
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["MENSAJE"] = "Apellido/ID erróneos";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
