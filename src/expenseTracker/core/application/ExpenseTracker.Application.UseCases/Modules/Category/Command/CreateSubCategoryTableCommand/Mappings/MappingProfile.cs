using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryTableCommand.Dtos;
using ExpenseTracker.Domain.Entities;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryTableCommand.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<Create_SubCategoryTable_SingleCommandDto, t_sub_category>()
				.ForMember(dest => dest.t_category_id, opt => opt.MapFrom(src => src.CategoryId))
				.ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.is_expense_created, opt => opt.MapFrom(src => src.IsExpenseCreated))
				.ForMember(dest => dest.is_other, opt => opt.MapFrom(src => src.IsOther));

		}
	}
}
