using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseQuery.Dtos;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Pages.Modules.Expenses.Models.Response;

namespace ExpenseTracker.MobileApp.Pages.Modules.Expenses.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<GetList_Expense_SingleResponseDto, GetList_Expense_SingleResponseModel>()
				.ForMember(dest => dest.Date_formatted, opt => opt.MapFrom(src => src.Date.ToShortDateString()))
				.ForMember(dest => dest.Necessary, opt => opt.MapFrom(src => src.IsNecessary ? "+" : "-"))
				.ForMember(dest => dest.RowColor, opt => opt.MapFrom(src =>
					src.Index % 2 == 0 ? ColorConstants.MiddlePurple : ColorConstants.SoftPurple
				));

		}
	}
}
