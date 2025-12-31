using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryTableQuery.Dtos;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryTableQuery
{
	public class GetList_SubCategoryTable_QueryHandler : UseCaseHandler<GetList_SubCategoryTable_QueryDto, GetList_SubCategoryTable_ResponseDto>
	{

		#region CTOR

		private readonly ISubCategoryRepository _subCategoryRepository;

		public GetList_SubCategoryTable_QueryHandler(IMapper mapper, ISubCategoryRepository subCategoryRepository) : base(mapper)
		{
			_subCategoryRepository = subCategoryRepository;
		}

		#endregion

		public override async Task<GetList_SubCategoryTable_ResponseDto> Handle(GetList_SubCategoryTable_QueryDto query, CancellationToken cancellationToken)
		{
			List<t_sub_category> records = await _subCategoryRepository.GetListAsync(
				orderBy: o => o.OrderBy(i => i.is_other).ThenBy(n => n.name)
				);

			return new GetList_SubCategoryTable_ResponseDto
			{
				Records = _mapper.Map<List<GetList_SubCategoryTable_SingleResponseDto>>(records)
			};
		}

	}
}
