using System.Data;

namespace Base.PrimitiveTypeHelpers._DataTable.Entensions
{
	public static class DataTableExtensions
	{

		public static List<T> ConvertDataTable<T>(this DataTable dataTable) where T : new()
		{
			List<T> records = new List<T>();

			foreach (DataRow row in dataTable.Rows)
			{
				T record = GetobjectFromDataRow<T>(row);
				records.Add(record);
			}

			return records;
		}

		#region Behind the Scenes

		private static T GetobjectFromDataRow<T>(DataRow dataRow)
		{
			Type temp = typeof(T);
			T obj = Activator.CreateInstance<T>();

			foreach (DataColumn column in dataRow.Table.Columns)
			{
				var propertyInfos = temp.GetProperties();

				var field = propertyInfos.Where(x => x.Name == column.ColumnName).FirstOrDefault();

				if (field is null)
					continue;

				var drValue = dataRow[column.ColumnName];
				if (drValue is DBNull)
					continue;

				if (field.Name.Equals("point"))
				{
					double[] coordinate = ((double[])drValue);
					propertyInfos.Where(x => x.Name.Equals("longitude")).FirstOrDefault().SetValue(obj, coordinate[0], null);
					propertyInfos.Where(x => x.Name.Equals("latitude")).FirstOrDefault().SetValue(obj, coordinate[1], null);
				}

				field.SetValue(obj, drValue, null);
			}

			return obj;
		}

		#endregion

	}
}
