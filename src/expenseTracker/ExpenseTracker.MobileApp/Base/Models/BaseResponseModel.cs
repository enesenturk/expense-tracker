namespace ExpenseTracker.MobileApp.Base.Models
{
	public class BaseResponseModel<T>
	{

		public T Response { get; set; }
		public string Message { get; set; }

	}
}