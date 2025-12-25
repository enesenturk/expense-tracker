using Base.Exceptions.ExceptionModels;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base.Dtos;
using MediatR;

namespace ExpenseTracker.MobileApp.Base
{
	public class BaseMediatorCaller
	{

		public async Task<BaseResponseModel<Response>> ProxyCallerAsync<Request, Response>(Page page, IMediator mediator, Request request)
		{
			try
			{
				Response response = (Response)await mediator.Send(request);

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

				BaseResponseModel<Response> responseModel = new BaseResponseModel<Response>
				{
					Message = exceptionMessage
				};

				return responseModel;
			}
		}

		private string GetExceptionMessage(Exception e)
		{
			if (e is BusinessRuleException)
			{
				return e.Message;
			}

			return uiMessage.Error_occurred;
		}

	}
}
