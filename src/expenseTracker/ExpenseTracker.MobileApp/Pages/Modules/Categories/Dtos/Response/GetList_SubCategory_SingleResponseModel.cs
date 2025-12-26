using ExpenseTracker.MobileApp.Base.Dtos;

namespace ExpenseTracker.MobileApp.Pages.Modules.Categories.Dtos.Response
{
	public class GetList_SubCategory_SingleResponseModel : ListRecordModel
	{
		public string Name { get; set; }
		public bool IsOther { get; set; }
	}
}
