using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryCommand.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryCommand.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryQuery.Dtos;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Pages.Modules.Categories.Dtos.Response;

namespace ExpenseTracker.MobileApp.Pages.Modules.Categories.Mappings
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<GetList_Category_SingleResponseDto, GetList_Category_SingleResponseModel>()
				.ForMember(dest => dest.RowColor, opt => opt.MapFrom(src =>
					src.Index % 2 == 0 ? ColorConstants.MiddlePurple : ColorConstants.SoftPurple
				));
			CreateMap<Create_Category_CommandDto, GetList_Category_SingleResponseModel>();


			CreateMap<GetList_SubCategory_SingleResponseDto, GetList_SubCategory_SingleResponseModel>()
				.ForMember(dest => dest.RowColor, opt => opt.MapFrom(src =>
					src.Index % 2 == 0 ? ColorConstants.MiddlePurple : ColorConstants.SoftPurple
				));
			CreateMap<Create_SubCategory_CommandDto, GetList_SubCategory_SingleResponseModel>();

		}
	}

}