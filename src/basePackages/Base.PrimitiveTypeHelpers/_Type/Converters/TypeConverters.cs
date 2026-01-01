using System.Globalization;

namespace Base.PrimitiveTypeHelpers._Type.Converters
{
	public class TypeConverters
	{

		public static object Deserialize(string value, Type targetType, CultureInfo culture = null)
		{
			if (culture == null)
				culture = CultureInfo.InvariantCulture;

			if (string.IsNullOrWhiteSpace(value))
				return null;

			if (targetType == typeof(Guid))
				return Guid.Parse(value);

			if (targetType == typeof(DateTime))
				return DateTime.ParseExact(
				value,
					"yyyy-MM-dd HH:mm:ss",
					culture
				);

			if (targetType == typeof(decimal))
				return decimal.Parse(value, culture);

			if (targetType == typeof(double))
				return double.Parse(value, culture);

			if (targetType == typeof(float))
				return float.Parse(value, culture);

			if (targetType == typeof(bool))
				return bool.Parse(value);

			return Convert.ChangeType(value, targetType, culture);
		}

		public static string Serialize(object value, CultureInfo culture = null)
		{
			if (value == null)
				return "";

			if (culture == null)
				culture = CultureInfo.InvariantCulture;

			return value switch
			{
				Guid g => g.ToString(),
				DateTime dt => dt.ToString("yyyy-MM-dd HH:mm:ss", culture),
				decimal d => d.ToString(culture),
				double d => d.ToString(culture),
				float f => f.ToString(culture),
				bool b => b.ToString(),
				_ => value.ToString()
			};
		}

	}
}
