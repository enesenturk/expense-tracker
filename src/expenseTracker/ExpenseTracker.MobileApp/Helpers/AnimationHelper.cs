namespace ExpenseTracker.MobileApp.Helpers
{
	public static class AnimationHelper
	{

		public static async Task StartFadeBlinkAsync(VisualElement element)
		{
			if (element == null) return;

			while (true)
			{
				await element.FadeTo(0.4, 600);
				await element.FadeTo(1, 1200);
			}
		}

	}
}
