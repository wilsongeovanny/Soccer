using Microsoft.EntityFrameworkCore;
using Soccer.Web.Data.Entities;

namespace Soccer.Web.Data
{
    public class DataContext : DbContext
    {   // creamos un nconstructor con ctor y anadiendo  :  Dbconetxt  
        //  ay que agregar un parametro que estan el los parentesis
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //prop creamos  el mobre de la entidad
        public DbSet<TeamEntity> Teams { get; set; }

        //  mandar a la base de  datos 
        // nos vamos a   appsetings.json
        // y pegamos un codigo 
    }
}
