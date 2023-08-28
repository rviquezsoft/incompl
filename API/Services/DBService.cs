using API.Context;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace API.Services
{
    public class DBService<T> : IDBService<T> where T : class
    {
        private readonly IDbContextFactory<ElementoContext> _factory;

        public DBService(IDbContextFactory<ElementoContext> factory)
        {
            _factory = factory;
        }

        //Get generico para cualquier entidad
        public async Task<List<T>> Get()
        {
            using (var context = await _factory.CreateDbContextAsync())
            {
                return await context.Set<T>().ToListAsync();
            }
        }

        public async Task<string> Post(T entity)
        {
            using (var context = await _factory.CreateDbContextAsync())
            {
                await context.Set<T>().AddAsync(entity);
                int result = await context.SaveChangesAsync();

                return result == 1 ? "ok" : "Algo salió mal!";
            }
        }

        public async Task<string> Patch(T entity)
        {
            using (var context = await _factory.CreateDbContextAsync())
            {
                //obtener el tipo de la entidad
                var entityType = context.Model.FindEntityType(typeof(T));

                //obtener las claves primarias
                var keyProperties = entityType.FindPrimaryKey().Properties;

                //obtener valores de las primarias
                var primaryKeyValues = new object[keyProperties.Count];
                for (int i = 0; i < keyProperties.Count; i++)
                {
                    primaryKeyValues[i] = keyProperties[i].PropertyInfo.GetValue(entity);
                }
                //obtener la entidad de la base de datos en base a las primarias
                var existingEntity = await context.Set<T>().FindAsync(primaryKeyValues);

                if (existingEntity == null)
                    return "La entidad no se encontró.";

                //establecer los valores nuevos
                foreach (var prop in entityType.GetProperties())
                {
                    if (!keyProperties.Contains(prop))
                    {
                        prop.PropertyInfo.SetValue(existingEntity, prop.PropertyInfo.GetValue(entity));
                    }
                }

                int result = await context.SaveChangesAsync();

                return result == 1 ? "ok" : "Algo salió mal!";
            }
        }

    }
}
