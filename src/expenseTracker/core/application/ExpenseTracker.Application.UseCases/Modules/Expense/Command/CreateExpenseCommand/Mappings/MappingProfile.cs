using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseCommand.Dtos;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.UseCases.Modules.Expense.Command.CreateExpenseCommand.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<Create_Expense_CommandDto, t_expense>()
				.ForMember(dest => dest.t_category_id, opt => opt.MapFrom(src => src.CategoryId))
				.ForMember(dest => dest.t_sub_category_id, opt => opt.MapFrom(src => src.SubCategoryId))
				.ForMember(dest => dest.date, opt => opt.MapFrom(src => src.Date))
				.ForMember(dest => dest.amount, opt => opt.MapFrom(src => src.Amount))
				.ForMember(dest => dest.is_necessary, opt => opt.MapFrom(src => src.IsNecessary));

		}
	}
}
