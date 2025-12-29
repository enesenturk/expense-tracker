using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryCommand.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteCategoryCommand.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery.Dtos;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Base.Models;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules.Categories.Models.Request;
using ExpenseTracker.MobileApp.Pages.Modules.Categories.Models.Response;
using MediatR;
using System.Collections.ObjectModel;

namespace ExpenseTracker.MobileApp.Pages.Modules.Categories
{
	public partial class CategoriesPage : BaseContentPage
	{

		#region CTOR

		private ObservableCollection<GetList_Category_SingleResponseModel> _categories;

		public CategoriesPage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper)
		{
			InitializeComponent();

			gridMain.BackgroundColor = ColorConstants.SoftGrey;

			lblCategories.Text = uiMessage.CATEGORIES;
			lblCategories.TextColor = ColorConstants.Purple;

			btnNew.Text = $"+ {uiMessage.NEW}";
			btnNew.BackgroundColor = ColorConstants.Purple;
		}

		#endregion

		#region Create

		private async void OnCreateClicked(object sender, EventArgs e)
		{
			string newCategory = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayPromptAsync(
				uiMessage.ADD_CATEGORY,
				string.Empty,
				accept: uiMessage.ADD,
				cancel: uiMessage.CANCEL,
				placeholder: uiMessage.NAME,
				maxLength: 25,
				keyboard: Keyboard.Text
				);

			if (string.IsNullOrWhiteSpace(newCategory))
				return;

			if (newCategory.Contains(','))
			{
				await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Please_not_use_comma, uiMessage.OK);
				return;
			}

			Create_Category_CommandDto command = new Create_Category_CommandDto
			{
				Culture = SettingsHelper.GetCultureCode(),
				Name = newCategory
			};

			BaseResponseModel<Create_Category_ResponseDto> response = await ProxyCallerAsync<Create_Category_CommandDto, Create_Category_ResponseDto>(command);

			if (!string.IsNullOrEmpty(response.Message))
				return;

			GetList_Category_SingleResponseModel newRecord = _mapper.Map<GetList_Category_SingleResponseModel>(command);
			newRecord.Id = response.Response.Id;
			_categories.Add(newRecord);

			Refresh(ref _categories, o => o.OrderBy(i => i.IsOther).ThenBy(n => n.Name), categoriesCollection);
		}

		#endregion

		#region Read

		public override async Task LoadDataAsync()
		{
			base.OnAppearing();

			GetList_Category_QueryDto query = new GetList_Category_QueryDto
			{
				Culture = SettingsHelper.GetCultureCode(),
			};

			BaseResponseModel<GetList_Category_ResponseDto> response = await ProxyCallerAsync<GetList_Category_QueryDto, GetList_Category_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return;

			List<GetList_Category_SingleResponseModel> records = _mapper.Map<List<GetList_Category_SingleResponseModel>>(response.Response.Records);

			_categories = new ObservableCollection<GetList_Category_SingleResponseModel>(records);

			categoriesCollection.ItemsSource = _categories;
		}

		#endregion

		#region Update

		private void OnUpdateClicked(object sender, EventArgs e)
		{
			Button button = sender as Button;

			GetList_Category_SingleResponseModel singleModel = button.BindingContext as GetList_Category_SingleResponseModel;

			if (singleModel != null)
			{
				Update_Category_RequestModel requestModel = new Update_Category_RequestModel
				{
					Id = singleModel.Id,
					Name = singleModel.Name
				};

				var layout = new LayoutPage(_mediator, _mapper);
				Microsoft.Maui.Controls.Application.Current.MainPage = layout;
				layout.SetPage(new CategoryPage(_mediator, _mapper, requestModel));
			}
		}

		#endregion

		#region Delete

		private async void OnDeleteClicked(object sender, EventArgs e)
		{
			Button button = sender as Button;

			if (button.CommandParameter is Guid categoryId)
			{
				bool isConfirmed = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Are_you_sure, uiMessage.YES, uiMessage.NO);

				if (!isConfirmed)
					return;

				Delete_Category_CommandDto command = new Delete_Category_CommandDto
				{
					Id = categoryId
				};

				BaseResponseModel<Delete_Category_ResponseDto> response = await ProxyCallerAsync<Delete_Category_CommandDto, Delete_Category_ResponseDto>(command);

				if (!string.IsNullOrEmpty(response.Message))
					return;

				if (response.Response.IsApprovalRequired)
				{
					bool isApproved = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Category_in_use, uiMessage.YES, uiMessage.NO);

					if (!isApproved)
						return;

					Delete_Category_CommandDto reCommand = new Delete_Category_CommandDto
					{
						Id = categoryId,
						IsApproved = true
					};

					BaseResponseModel<Delete_Category_ResponseDto> reResponse = await ProxyCallerAsync<Delete_Category_CommandDto, Delete_Category_ResponseDto>(reCommand);

					if (!string.IsNullOrEmpty(reResponse.Message))
						return;
				}

				var remove = _categories.FirstOrDefault(c => c.Id == categoryId);

				if (remove != null)
					_categories.Remove(remove);

				Refresh(ref _categories, o => o.OrderBy(i => i.IsOther).ThenBy(n => n.Name), categoriesCollection);
			}
		}

		#endregion

	}

}