using AutoMapper;
using Base.Exceptions.ExceptionModels;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base.Models;
using ExpenseTracker.MobileApp.Constants;
using MediatR;
using System.Collections.ObjectModel;

namespace ExpenseTracker.MobileApp.Base
{
	public class BaseContentPage : ContentPage
	{

		protected readonly IMediator _mediator;
		protected readonly IMapper _mapper;

		protected readonly BaseMediatorCaller _baseMediatorCaller;

		public BaseContentPage(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		public async Task<BaseResponseModel<Response>> ProxyCallerAsync<Request, Response>(Request request)
		{
			try
			{
				Response response = (Response)await _mediator.Send(request);

				BaseResponseModel<Response> responseModel = new BaseResponseModel<Response>
				{
					Response = response,
					Message = string.Empty
				};

				return responseModel;
			}
			catch (Exception e)
			{
				string exceptionMessage = GetExceptionMessage(e);

				await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, exceptionMessage, uiMessage.OK);

				BaseResponseModel<Response> responseModel = new BaseResponseModel<Response>
				{
					Message = exceptionMessage
				};

				return responseModel;
			}
		}

		public virtual async Task LoadDataAsync()
		{
			await Task.CompletedTask;
		}

		public void Refresh<T>(ref ObservableCollection<T> records, Func<IEnumerable<T>, IOrderedEnumerable<T>> orderBy, CollectionView collectionView) where T : ListRecordModel
		{
			List<T> updatedRecords = new List<T>();

			List<T> orderedRecords = orderBy(records).ToList();

			for (int i = 0; i < orderedRecords.Count; i++)
			{
				T record = orderedRecords[i];

				record.Index = i;
				record.RowColor = record.Index % 2 == 0 ? ColorConstants.MiddlePurple : ColorConstants.SoftPurple;

				updatedRecords.Add(record);
			}

			records = new ObservableCollection<T>(updatedRecords);
			collectionView.ItemsSource = records;
		}

		#region Behind the Scenes

		private string GetExceptionMessage(Exception e)
		{
			if (e is BusinessRuleException)
			{
				return e.Message;
			}

			return uiMessage.Error_occurred;
		}

		#endregion

	}
}