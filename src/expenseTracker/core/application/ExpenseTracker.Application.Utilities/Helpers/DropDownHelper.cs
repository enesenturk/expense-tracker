using Base.Dto;
using ExpenseTracker.Domain.Resources.Helpers;
using ExpenseTracker.Domain.Resources.Languages;

namespace ExpenseTracker.Application.Utilities.Helpers
{
	public class DropDownHelper
	{

		public static List<JSonDto> GetDropDownFromEnum<T>(bool addSelectOption = true)
		{
			List<JSonDto> dropdown = new List<JSonDto>();

			if (addSelectOption)
				AddSelectOption(ref dropdown);

			string[] names = GetNames<T>();

			AddOptions<T>(ref dropdown, names);

			return dropdown;
		}

		#region Behind the Scenes

		private static void AddOptions<T>(ref List<JSonDto> dropdown, string[] names)
		{
			for (int i = 0; i < names.Length; i++)
			{
				int key = GetKeyFromEnum<T>(names[i]);
				string value = GetValueFromLanguage(names[i]);

				dropdown.Add(new JSonDto
				{
					Key = key.ToString(),
					Value = value
				});
			}
		}

		private static int GetKeyFromEnum<T>(string name)
		{
			return (int)Enum.Parse(typeof(T), name);
		}

		private static string GetValueFromLanguage(string name)
		{
			return uiMessageHelper.GetUiMessage(name);
		}

		private static string[] GetNames<T>()
		{
			return Enum.GetNames(typeof(T));
		}

		private static void AddSelectOption(ref List<JSonDto> dropdown)
		{
			JSonDto select = new JSonDto
			{
				Key = "null",
				Value = uiMessage.SELECT
			};

			dropdown.Add(select);
		}

		#endregion

	}
}
