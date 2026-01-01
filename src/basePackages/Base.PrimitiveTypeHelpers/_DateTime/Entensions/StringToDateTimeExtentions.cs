using Base.Exceptions.ExceptionModels;
using System.Globalization;

namespace Base.PrimitiveTypeHelpers._DateTime.Entensions
{
	public static class StringToDateTimeExtentions
	{

		public static DateTime StringToDateTimeExactConverter(this string date, string dateFormat)
		{
			DateTime Converted;

			bool isOK = DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out Converted);

			if (!isOK)
				throw new BusinessRuleException("Valid time format is: " + dateFormat);

			return Converted;
		}

	}
}
