using Newtonsoft.Json;
using System.Text;

namespace Base.PrimitiveTypeHelpers._String.Converters
{
	public class StringConverter
	{

		public static T Base64ToJson<T>(string base64)
		{
			T response = default;

			byte[] byteArrayFromBase64 = Convert.FromBase64String(base64);

			using (var memoryStream = new MemoryStream(byteArrayFromBase64))
			{
				using (var reader = new StreamReader(memoryStream))
				{
					// Read the stream to a string
					string jsonString = reader.ReadToEnd();

					if (typeof(T) == typeof(string))
					{
						response = (T)(object)jsonString;
					}
					else
					{
						response = JsonConvert.DeserializeObject<T>(jsonString);
					}
				}
			}

			return response;
		}

		public static string Base64ToString(string base64)
		{
			byte[] bytes = Convert.FromBase64String(base64);
			string decodedString = Encoding.UTF8.GetString(bytes);

			return decodedString;
		}

		public static byte[] ConvertBase64ToByteArray(string base64)
		{
			return Convert.FromBase64String(base64);
		}

	}
}
