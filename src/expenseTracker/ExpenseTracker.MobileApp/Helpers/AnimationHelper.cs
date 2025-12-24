using Microsoft.Maui.Animations;

namespace ExpenseTracker.MobileApp.Helpers
{
	public static class AnimationHelper
	{

		public static async void StartFadeBlinkAsync(Label element)
		{
			if (element == null) return;

			while (true)
			{
				await element.FadeTo(0.4, 600);
				await element.FadeTo(1, 1200);
			}
		}

		public static async void StartBlinking(Frame frame, Color fromColor, Color toColor)
		{
			int steps = 25;
			int delay = 50;

			while (true)
			{
				for (int i = 0; i <= steps; i++)
				{
					float t = i / (float)steps;
					frame.BackgroundColor = fromColor.Lerp(toColor, t);
					await Task.Delay(delay);
				}

				for (int i = 0; i <= steps; i++)
				{
					float t = i / (float)steps;
					frame.BackgroundColor = toColor.Lerp(fromColor, t);
					await Task.Delay(delay);
				}
			}
		}

	}
}
