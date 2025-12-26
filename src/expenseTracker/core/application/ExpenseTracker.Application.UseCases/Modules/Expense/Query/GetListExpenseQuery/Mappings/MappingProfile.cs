using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseQuery.Dtos;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Query.GetListExpenseQuery.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<t_expense, GetList_Expense_SingleResponseDto>()
				.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.t_category_id))
				.ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.t_sub_category_id))
				.ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.date))
				.ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.amount))
				.ForMember(dest => dest.IsNecessary, opt => opt.MapFrom(src => src.is_necessary));

		}
	}
}
