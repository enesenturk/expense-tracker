namespace ExpenseTracker.MobileApp.Base.Dtos
{
	public class BaseResponseModel<T>
	{

		public T Response { get; set; }
		public string Message { get; set; }

	}
}