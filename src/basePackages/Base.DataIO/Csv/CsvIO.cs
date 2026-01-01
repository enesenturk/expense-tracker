using Base.PrimitiveTypeHelpers._Type.Converters;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace Base.DataIO.Csv
{
	public class CsvIO : ICsvIO
	{

		public string CreateCsv<T>(List<T> list, bool isAddHeader, CultureInfo culture = null)
		{
			if (culture == null)
				culture = CultureInfo.InvariantCulture;

			StringBuilder builder = new StringBuilder();

			foreach (var entity in list)
			{
				var values = typeof(T)
					.GetProperties()
					.Select(p => TypeConverters.Serialize(p.GetValue(entity)));

				builder.AppendLine(string.Join(",", values));
			}

			return builder.ToString();
		}

		public async Task<List<T>> ReadCsv<T>(string _filePath, CultureInfo culture = null) where T : class, new()
		{
			if (culture == null)
				culture = CultureInfo.InvariantCulture;

			List<T> list = new List<T>();

			string[] lines = await File.ReadAllLinesAsync(_filePath);

			foreach (string line in lines)
			{
				if (string.IsNullOrWhiteSpace(line))
					continue;

				string[] values = line.Split(',');
				T entity = new T();
				PropertyInfo[] props = typeof(T).GetProperties();

				for (int i = 0; i < props.Length && i < values.Length; i++)
				{
					object convertedValue = TypeConverters.Deserialize(values[i], props[i].PropertyType);
					props[i].SetValue(entity, convertedValue);
				}

				list.Add(entity);
			}

			return list;
		}

	}
}
