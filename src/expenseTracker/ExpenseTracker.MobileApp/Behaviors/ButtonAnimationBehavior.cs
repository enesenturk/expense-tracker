namespace ExpenseTracker.MobileApp.Behaviors
{
	public class ButtonAnimationBehavior : Behavior<Button>
	{
		protected override void OnAttachedTo(Button bindable)
		{
			base.OnAttachedTo(bindable);
			bindable.Clicked += OnButtonClicked;
		}

		protected override void OnDetachingFrom(Button bindable)
		{
			base.OnDetachingFrom(bindable);
			bindable.Clicked -= OnButtonClicked;
		}

		private async void OnButtonClicked(object? sender, EventArgs e)
		{
			if (sender is not Button btn)
				return;

			await btn.ScaleTo(1.2, 100);
			await btn.ScaleTo(1, 100);
		}
	}
}
