using Microsoft.EntityFrameworkCore;
using POC.Infrastructure.Data;
using POC.Infrastructure.Repositories.Interfaces;
using POC.Shared.Responses;

namespace POC.Infrastructure.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entity;
        private string _addMessage = "Ya existe el registro que estas intentando crear.";
        private string _deleteMessageError = "No se puede borrar, porque tiene registros relacionados";
        private string _notFoundMessage = "Registro no encontrado";

        public GenericRepository(DataContext context)
        {
            _context = context;
            _entity = context.Set<T>();
        }

        public virtual async Task<ActionResponse<T>> AddAsync(T entity)
        {
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                return EntityActionResponse(true, entity);
            }
            catch (DbUpdateException)
            {
                return ActionResponseMessage(true, this._addMessage);
            }
            catch (Exception exception)
            {
                return ExceptionActionResponse(exception);
            }
        }

        public virtual async Task<ActionResponse<T>> DeleteAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row == null)
            {
                return ActionResponseMessage(false, this._notFoundMessage);
            }

            try
            {
                _entity.Remove(row);
                await _context.SaveChangesAsync();
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                };
            }
            catch
            {
                return ActionResponseMessage(false, this._deleteMessageError);
            }
        }

        public virtual async Task<ActionResponse<T>> GetAsync(int id)
        {
            var row = await _entity.FindAsync(id);
            if (row != null)
            {
                return new ActionResponse<T>
                {
                    WasSuccess = true,
                    Result = row
                };
            }
            return ActionResponseMessage(false, this._notFoundMessage);
        }

        public virtual async Task<ActionResponse<IEnumerable<T>>> GetAsync()
        {
            return new ActionResponse<IEnumerable<T>>
            {
                WasSuccess = true,
                Result = await _entity.ToListAsync()
            };
        }

        public virtual async Task<ActionResponse<T>> UpdateAsync(T entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return EntityActionResponse(true, entity);
            }
            catch (DbUpdateException)
            {
                return ActionResponseMessage(true, this._addMessage);
            }
            catch (Exception exception)
            {
                return ExceptionActionResponse(exception);
            }
        }

        private ActionResponse<T> ExceptionActionResponse(Exception exception)
        {
            return new ActionResponse<T>
            {
                WasSuccess = false,
                Message = exception.Message
            };
        }

        private ActionResponse<T> ActionResponseMessage(bool success, string message)
        {
            return new ActionResponse<T>
            {
                WasSuccess = success,
                Message = message
            };
        }

        private ActionResponse<T> EntityActionResponse(bool success, T entity)
        {
            return new ActionResponse<T>
            {
                WasSuccess = success,
                Result = entity
            };
        }
    }
}