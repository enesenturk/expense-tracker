using Base.DataAccess.Entity;
using Base.DataAccess.Repositories.Base.Abstract;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Base.DataAccess.Repositories.Base.Concrete
{
	public class CsvRepositoryBase<T> : IRepositoryBase<T> where T : IEntity, new()
	{

		#region CTOR

		private readonly CultureInfo _culture = CultureInfo.InvariantCulture;
		private readonly string _filePath;

		public CsvRepositoryBase(string filePath)
		{
			_filePath = filePath;

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
			var values = props.Select(p => p.GetValue(entity)?.ToString() ?? "");
			var line = Environment.NewLine + string.Join(",", values);

			await File.AppendAllTextAsync(_filePath, line);

			return entity;
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

		public async Task<List<T>> GetListAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Expression<Func<T, bool>> predicate = null)
		{
			List<T> list = new List<T>();

			string[] lines = await File.ReadAllLinesAsync(_filePath);

			foreach (string line in lines)
			{
				if (string.IsNullOrEmpty(line))
					continue;

				string[] values = line.Split(',');
				T entity = new T();
				PropertyInfo[] props = typeof(T).GetProperties();

				for (int i = 0; i < props.Length && i < values.Length; i++)
				{
					Type propType = props[i].PropertyType;

					object convertedValue;

					if (propType == typeof(Guid))
					{
						convertedValue = Guid.Parse(values[i]);
					}
					else if (propType.IsEnum)
					{
						convertedValue = Enum.Parse(propType, values[i]);
					}
					else
					{
						convertedValue = Convert.ChangeType(values[i], propType);
					}

					props[i].SetValue(entity, convertedValue);
				}

				list.Add(entity);
			}

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
			var sb = new StringBuilder();

			foreach (var entity in list)
			{
				var values = typeof(T).GetProperties().Select(p => p.GetValue(entity)?.ToString() ?? "");
				sb.AppendLine(string.Join(",", values));
			}

			await File.WriteAllTextAsync(_filePath, sb.ToString());
		}

		private string Serialize(object value)
		{
			if (value == null)
				return "";

			return value switch
			{
				DateTime dt => dt.ToString("yyyy-MM-dd HH:mm:ss", _culture),
				decimal d => d.ToString(_culture),
				double d => d.ToString(_culture),
				float f => f.ToString(_culture),
				bool b => b.ToString(),
				_ => value.ToString()
			};
		}

		private object Deserialize(string value, Type targetType)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;

			if (targetType == typeof(DateTime))
				return DateTime.ParseExact(
					value,
					"yyyy-MM-dd HH:mm:ss",
					_culture
				);

			if (targetType == typeof(decimal))
				return decimal.Parse(value, _culture);

			if (targetType == typeof(double))
				return double.Parse(value, _culture);

			if (targetType == typeof(float))
				return float.Parse(value, _culture);

			if (targetType.IsEnum)
				return Enum.Parse(targetType, value);

			return Convert.ChangeType(value, targetType, _culture);
		}

		#endregion

	}
}
