using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryTableQuery.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryTableQuery.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseTableQuery.Dtos;
using ExpenseTracker.MobileApp.Pages.Modules.Settings.Models.Response;

namespace ExpenseTracker.MobileApp.Pages.Modules.Settings.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<GetList_CategoryTable_SingleResponseDto, GetList_CategoryTable_SingleResponseModel>();
			CreateMap<GetList_SubCategoryTable_SingleResponseDto, GetList_SubCategoryTable_SingleResponseModel>();
			CreateMap<GetList_ExpenseTable_SingleResponseDto, GetList_ExpenseTable_SingleResponseModel>();

		}
	}
}
