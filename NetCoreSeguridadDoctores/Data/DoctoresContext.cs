using Microsoft.EntityFrameworkCore;
using NetCoreSeguridadDoctores.Models;
using System.Data.Common;

namespace NetCoreSeguridadDoctores.Data
{
    public class DoctoresContext : DbContext
    {
        public DoctoresContext(DbContextOptions<DoctoresContext> options)
            : base(options) { }

        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Enfermo> Enfermos { get; set; }
    }
}
