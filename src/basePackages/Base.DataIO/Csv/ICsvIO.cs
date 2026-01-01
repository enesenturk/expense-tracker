using System.Globalization;

namespace Base.DataIO.Csv
{
	public interface ICsvIO
	{

		string CreateCsv<T>(List<T> list, bool isAddHeader, CultureInfo culture = null);

		Task<List<T>> ReadCsv<T>(string _filePath, CultureInfo culture = null) where T : class, new();

	}
}
