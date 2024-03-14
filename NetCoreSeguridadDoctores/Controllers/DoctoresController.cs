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
            return View(inscripcion);
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
    }
}
