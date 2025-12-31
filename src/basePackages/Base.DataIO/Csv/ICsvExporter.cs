namespace Base.DataIO.Csv
{
	public interface ICsvExporter
	{

		byte[] CreateCvs<T>(List<T> data, bool isAddHeader);

	}
}
