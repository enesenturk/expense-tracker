using System.Reflection;
using System.Text;

namespace Base.DataIO.Csv
{
	public class CsvExporter : ICsvExporter
	{

		public byte[] CreateCvs<T>(List<T> data, bool isAddHeader)
		{
			Type type = typeof(T);
			PropertyInfo[] columnInfos = type.GetProperties();

			StringBuilder builder = new StringBuilder();

			if (isAddHeader)
			{
				foreach (PropertyInfo columnInfo in columnInfos)
					builder.Append(columnInfo.Name + ",");
			}

			builder.Append("\r\n");

			for (int i = 0; i < data.Count; i++)
			{
				T rowData = data[i];

				foreach (PropertyInfo columnInfo in columnInfos)
				{
					object cellValue = columnInfo.GetValue(rowData, null);

					builder.Append(cellValue + ",");
				}

				builder.Append("\r\n");
			}

			return Encoding.UTF8.GetBytes(builder.ToString());
		}

	}
}
