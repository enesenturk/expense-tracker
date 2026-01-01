using Base.DataAccess.Entity;
using Base.DataAccess.Repositories.Base.Abstract;
using Base.DataIO.Csv;
using Base.PrimitiveTypeHelpers._Type.Converters;
using System.Linq.Expressions;

namespace Base.DataAccess.Repositories.Base.Concrete
{
	public class CsvRepositoryBase<T> : IRepositoryBase<T> where T : IEntity, new()
	{

		#region CTOR

		private readonly string _filePath;

		private readonly ICsvIO _csvIO;

		public CsvRepositoryBase(string filePath, ICsvIO csvIO)
		{
			_filePath = filePath;

			_csvIO = csvIO;

			if (!File.Exists(_filePath))
				File.WriteAllText(_filePath, string.Empty);
		}

		#endregion

		#region Create

		public async Task<T> AddAsync(T entity)
		{
			if (entity.id == Guid.Empty)
				entity.id = Guid.NewGuid();

			var props = typeof(T).GetProperties();
			var values = props.Select(p => TypeConverters.Serialize(p.GetValue(entity)));
			var line = Environment.NewLine + string.Join(",", values);

			await File.AppendAllTextAsync(_filePath, line);

			return entity;
		}

		public async Task AddRangeAsync(List<T> entities)
		{
			foreach (var entity in entities)
			{
				await AddAsync(entity);
			}
		}

		#endregion

		#region Read

		public async Task<T> GetAsync(Guid id)
		{
			List<T> list = await GetListAsync(
				orderBy: o => o.OrderBy(i => i.id)
			);

			return list.FirstOrDefault(x => x.id == id);
		}

		public async Task<List<T>> GetListAsync(
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
			Expression<Func<T, bool>> predicate = null)
		{
			List<T> list = await _csvIO.ReadCsv<T>(_filePath);

			if (predicate != null)
				list = list.Where(predicate.Compile()).ToList();

			if (orderBy != null)
				list = orderBy(list.AsQueryable()).ToList();

			return list;
		}

		#endregion

		#region Update

		public async Task<T> UpdateAsync(T entity)
		{
			List<T> list = await GetListAsync(
				orderBy: o => o.OrderBy(i => i.id)
				);

			List<T> newList = list.Where(x => x.id != entity.id).ToList();

			newList.Add(entity);

			await WriteAllAsync(newList);

			return entity;
		}

		#endregion

		#region Delete

		public async Task DeleteAsync(T entity)
		{
			List<T> list = await GetListAsync(
				orderBy: o => o.OrderBy(i => i.id)
				);

			List<T> newList = list.Where(x => x.id != entity.id).ToList();

			await WriteAllAsync(newList);
		}

		public async Task DeleteRangeAsync(List<T> entities)
		{
			List<T> list = await GetListAsync(
				orderBy: o => o.OrderBy(i => i.id)
				);

			List<T> newList = list.Where(x => !entities.Select(g => g.id).Contains(x.id)).ToList();

			await WriteAllAsync(newList);
		}

		#endregion

		#region Behind the Scenes

		private async Task WriteAllAsync(List<T> list)
		{
			string sb = _csvIO.CreateCsv(list, isAddHeader: false);

			await File.WriteAllTextAsync(_filePath, sb);
		}

		#endregion

	}
}
