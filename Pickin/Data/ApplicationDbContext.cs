using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Pickin.Enums;
using Pickin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pickin.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Producto> Producto { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Pedidos_Productos> Pedidos_Productos { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var modifiedEntries = ChangeTracker.Entries().Where(x => x.Entity is AuditableEntity
                                && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

                foreach (var entry in modifiedEntries)
                {
                    var entity = entry.Entity as AuditableEntity;

                    if (entity == null) continue;

                    var now = DateTime.Now;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            if (entity.Estado == (int)States.PendingChanges)
                            {
                                entity.Estado = (int)States.PendingChanges;
                            }
                            else
                            {
                                entity.Estado = (int)States.Created;
                            }
                            entity.FechaAlta = now;
                            break;
                        case EntityState.Modified:
                            base.Entry(entity).Property(x => x.FechaAlta).IsModified = false;
                            base.Entry(entity).Property(x => x.UsuarioAlta).IsModified = false;
                            if (entity.Estado == (int)States.Deleted)
                            {
                                entity.Estado = (int)States.Deleted;
                                entity.FechaBaja = now;
                            }
                            else
                            {
                                entity.Estado = entity.Estado;
                                entity.FechaModificacion = now;
                            }
                            break;
                        default:
                            base.Entry(entity).Property(x => x.FechaAlta).IsModified = false;
                            base.Entry(entity).Property(x => x.UsuarioAlta).IsModified = false;
                            entity.Estado = entity.Estado;
                            entity.FechaModificacion = now;
                            break;
                    }
                }

                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException conEx)
            {
                var data = conEx.Entries.Single();
                data.OriginalValues.SetValues(data.GetDatabaseValues());
                if (data.OriginalValues == null)
                {
                    Console.WriteLine("The entity being updated is already deleted by another user...");
                }
                else
                {
                    Console.WriteLine("The entity being updated has already been updated by another user...");
                }

                throw new ApplicationException("", conEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.InnerException.Message, ex.InnerException.InnerException);
            }
        }
    }
}
