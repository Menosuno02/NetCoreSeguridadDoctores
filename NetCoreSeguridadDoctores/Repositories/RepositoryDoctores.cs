using Microsoft.EntityFrameworkCore;
using NetCoreSeguridadDoctores.Data;
using NetCoreSeguridadDoctores.Models;

namespace NetCoreSeguridadDoctores.Repositories
{
    public class RepositoryDoctores
    {
        private DoctoresContext context;
        public RepositoryDoctores(DoctoresContext context)
        {
            this.context = context;
        }

        public async Task<Doctor> LogInDoctor(string apellido, int id)
        {
            Doctor doctor = await this.context.Doctores
                .FirstOrDefaultAsync(d => d.Apellido == apellido
                && d.DoctorNo == id);
            return doctor;
        }

        public async Task<List<Enfermo>> GetEnfermosAsync()
        {
            return await this.context.Enfermos.ToListAsync();
        }

        public async Task<Enfermo> FindEnfermoAsync(int inscripcion)
        {
            return await this.context.Enfermos
                .FirstOrDefaultAsync(e => e.Inscripcion == inscripcion);
        }

        public async Task DeleteEnfermoAsync(int inscripcion)
        {
            Enfermo enfermo = await this.context.Enfermos
                .FirstOrDefaultAsync(e => e.Inscripcion == inscripcion);
            this.context.Enfermos.Remove(enfermo);
            this.context.SaveChangesAsync();
        }
    }
}
