using Microsoft.AspNetCore.Mvc;
using NetCoreSeguridadDoctores.Filters;
using NetCoreSeguridadDoctores.Models;
using NetCoreSeguridadDoctores.Repositories;

namespace NetCoreSeguridadDoctores.Controllers
{
    public class DoctoresController : Controller
    {
        private RepositoryDoctores repo;

        public DoctoresController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        [AuthorizeDoctores(Policy = "PERMISOSELEVADOS")]
        public async Task<IActionResult> Enfermos()
        {
            List<Enfermo> enfermos = await this.repo.GetEnfermosAsync();
            return View(enfermos);
        }

        [AuthorizeDoctores]
        public async Task<IActionResult> Perfil()
        {
            return View();
        }

        [AuthorizeDoctores]
        public async Task<IActionResult> Delete(int inscripcion)
        {
            if (inscripcion == 0) return RedirectToAction("Enfermos");
            Enfermo enfermo = await this.repo.FindEnfermoAsync(inscripcion);
            return View(enfermo);
        }

        [HttpPost]
        [AuthorizeDoctores]
        public async Task<IActionResult> Delete(int? inscripcion)
        {
            if (inscripcion != null)
            {
                await this.repo.DeleteEnfermoAsync(inscripcion.Value);
            }
            return RedirectToAction("Enfermos");
        }

        [AuthorizeDoctores(Policy = "AdminOnly")]
        public IActionResult AdminDoctores()
        {
            return View();
        }
    }
}
