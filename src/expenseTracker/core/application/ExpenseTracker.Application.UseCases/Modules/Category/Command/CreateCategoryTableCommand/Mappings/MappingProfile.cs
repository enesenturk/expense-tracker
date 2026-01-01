using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryTableCommand.Dtos;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryTableCommand.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<Create_CategoryTable_SingleCommandDto, t_category>()
				.ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.culture, opt => opt.MapFrom(src => src.Culture))
				.ForMember(dest => dest.is_expense_created, opt => opt.MapFrom(src => src.IsExpenseCreated))
				.ForMember(dest => dest.is_other, opt => opt.MapFrom(src => src.IsOther));

		}
	}
}
