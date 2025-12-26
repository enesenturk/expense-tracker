namespace ExpenseTracker.MobileApp.Constants
{
	public class ColorConstants
	{

		public static string SoftGreyCode => "#EBEBEB";
		public static Color SoftGrey => Color.FromRgba(SoftGreyCode);

		public static Color Purple => Color.FromRgba("#512BD4");
		public static Color MiddlePurple => Color.FromRgba("#A895EA");
		public static Color SoftPurple => Color.FromRgba("#D9CCF9");

		public static List<string> Colors => new List<string>
		{
			"#4E79A7", // Soft Blue
            "#59A14F", // Soft Green
            "#F28E2B", // Soft Orange
            "#E15759", // Soft Red
            "#76B7B2", // Soft Teal
            "#EDC948", // Soft Yellow
            "#B07AA1", // Soft Purple
            "#FF9DA7", // Soft Pink
            "#9C755F", // Soft Brown
            "#BAB0AC", // Soft Gray

            "#A0CBE8", // Light Blue
            "#8CD17D", // Light Green
            "#FFBE7D", // Light Orange
            "#FF9D9A", // Light Red
            "#86BCB6", // Light Teal
            "#F1CE63", // Light Yellow
            "#D4A6C8", // Light Lavender
            "#FABFD2", // Light Pink
            "#C7B1A3", // Light Brown
            "#D7D5D4"  // Light Gray
        };

		public static string GetColorByIndex(int index)
		{
			int safeIndex = index % Colors.Count;

			return Colors[safeIndex];
		}

	}
}
