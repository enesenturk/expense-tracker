namespace Base.PrimitiveTypeHelpers._File.Converters
{
	public static class FileConverters
	{

		public static string PhysicalFileToBase64(string physicalPath)
		{
			byte[] bytes = File.ReadAllBytes(physicalPath);

			return Convert.ToBase64String(bytes);
		}

	}
}
