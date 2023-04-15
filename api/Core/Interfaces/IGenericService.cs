using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IGenericService<TEntity, TEntityCreateEdit> where TEntity : class, new() where TEntityCreateEdit : class, new()
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<TEntity> GetByIdAsync(int id);
		Task<int> CreateAsync(TEntityCreateEdit entity);
		Task<bool> UpdateAsync(TEntityCreateEdit entity);
		Task<bool> RemoveAsync(int id);
		Task SaveAsync();
	}
}
