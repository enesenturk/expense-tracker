namespace Base.PrimitiveTypeHelpers._Type.Entensions
{
	public static class TypeExtensions
	{

		public static string GetBaseAssemblyName(this Type type)
		{
			return type?.Assembly?.FullName.Split(".")[0];
		}

	}
}
