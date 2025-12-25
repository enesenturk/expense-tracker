using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryQuery.Dtos;
using ExpenseTracker.Application.Utilities.Helpers;
using ExpenseTracker.Application.Utilities.Mediator;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Repositories.Abstractions.Modules.Category;
using System.Linq.Expressions;

namespace ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryQuery
{
	public class GetList_SubCategory_QueryHandler : UseCaseHandler<GetList_SubCategory_QueryDto, GetList_SubCategory_ResponseDto>
	{

		#region CTOR

		private readonly ISubCategoryRepository _subCategoryRepository;

		public GetList_SubCategory_QueryHandler(IMapper mapper, ISubCategoryRepository subCategoryRepository) : base(mapper)
		{
			_subCategoryRepository = subCategoryRepository;
		}

		#endregion

		public override async Task<GetList_SubCategory_ResponseDto> Handle(GetList_SubCategory_QueryDto query, CancellationToken cancellationToken)
		{
			Expression<Func<t_sub_category, bool>> predicate = x => x.t_category_id == query.CategoryId;

			List<t_sub_category> records = await _subCategoryRepository.GetListAsync(
				orderBy: o => o.OrderBy(n => n.name),
				predicate: predicate
				);

			return new GetList_SubCategory_ResponseDto
			{
				Records = ListHelper.MapListRecord<t_sub_category, GetList_SubCategory_SingleResponseDto>(_mapper, records)
			};
		}

	}
}
