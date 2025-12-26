using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateSubCategoryCommand.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteSubCategoryCommand.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateCategoryCommand.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.UpdateSubCategoryCommand.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListSubCategoryQuery.Dtos;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Base.Models;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Pages.Modules.Categories.Models.Request;
using ExpenseTracker.MobileApp.Pages.Modules.Categories.Models.Response;
using MediatR;
using System.Collections.ObjectModel;

namespace ExpenseTracker.MobileApp.Pages.Modules.Categories
{
	public partial class CategoryPage : BaseContentPage
	{

		#region CTOR

		private Update_Category_RequestModel _model;

		private ObservableCollection<GetList_SubCategory_SingleResponseModel> _subCategories;

		public CategoryPage(IMediator mediator, IMapper mapper, Update_Category_RequestModel model)
			: base(mediator, mapper)
		{
			InitializeComponent();

			gridMain.BackgroundColor = ColorConstants.SoftGrey;

			_model = model;

			lblCategory.Text = uiMessage.CATEGORY_DETAIL;
			lblCategory.TextColor = ColorConstants.Purple;

			entryCategoryName.Text = _model.Name;

			btnSave.BackgroundColor = ColorConstants.Purple;
			btnSave.Text = uiMessage.UPDATE;
			btnSave.IsVisible = false;

			btnNew.Text = $"+ {uiMessage.NEW}";
			btnNew.BackgroundColor = ColorConstants.Purple;
		}

		#endregion

		#region Create

		private async void OnCreateClicked(object sender, EventArgs e)
		{
			string newSubCategory = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayPromptAsync(
				uiMessage.ADD_SUB_CATEGORY,
				string.Empty,
				accept: uiMessage.ADD,
				cancel: uiMessage.CANCEL,
				placeholder: uiMessage.NAME,
				maxLength: 25,
				keyboard: Keyboard.Text
				);

			if (string.IsNullOrWhiteSpace(newSubCategory))
				return;

			if (newSubCategory.Contains(','))
			{
				await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Please_not_use_comma, uiMessage.OK);
				return;
			}

			Create_SubCategory_CommandDto command = new Create_SubCategory_CommandDto
			{
				CategoryId = _model.Id,
				Name = newSubCategory
			};

			BaseResponseModel<Create_SubCategory_ResponseDto> response = await ProxyCallerAsync<Create_SubCategory_CommandDto, Create_SubCategory_ResponseDto>(command);

			if (!string.IsNullOrEmpty(response.Message))
				return;

			GetList_SubCategory_SingleResponseModel newRecord = _mapper.Map<GetList_SubCategory_SingleResponseModel>(command);
			newRecord.Id = response.Response.Id;

			_subCategories.Add(newRecord);

			Refresh(ref _subCategories, o => o.OrderBy(i => i.IsOther).ThenBy(n => n.Name), subCategoriesCollection);
		}

		#endregion

		#region Read

		public override async void LoadDataAsync()
		{
			base.OnAppearing();

			GetList_SubCategory_QueryDto query = new GetList_SubCategory_QueryDto
			{
				CategoryId = _model.Id
			};

			BaseResponseModel<GetList_SubCategory_ResponseDto> response = await ProxyCallerAsync<GetList_SubCategory_QueryDto, GetList_SubCategory_ResponseDto>(query);

			if (!string.IsNullOrEmpty(response.Message))
				return;

			List<GetList_SubCategory_SingleResponseModel> records = _mapper.Map<List<GetList_SubCategory_SingleResponseModel>>(response.Response.Records);

			_subCategories = new ObservableCollection<GetList_SubCategory_SingleResponseModel>(records);

			subCategoriesCollection.ItemsSource = _subCategories;
		}

		#endregion

		#region Update

		private void EntryCategoryName_TextChanged(object sender, TextChangedEventArgs e)
		{
			btnSave.IsVisible = e.NewTextValue != _model.Name;
		}

		private async void OnUpdateClicked(object sender, EventArgs e)
		{
			string newName = entryCategoryName.Text;

			if (newName.Contains(','))
			{
				await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Please_not_use_comma, uiMessage.OK);
				return;
			}

			if (_model.Name != newName)
			{
				Update_Category_CommandDto command = new Update_Category_CommandDto
				{
					Id = _model.Id,
					Name = newName
				};

				BaseResponseModel<Unit> response = await ProxyCallerAsync<Update_Category_CommandDto, Unit>(command);

				if (!string.IsNullOrEmpty(response.Message))
				{
					entryCategoryName.Text = _model.Name;
					return;
				}

				btnSave.IsVisible = false;
				await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Successfully_updated, uiMessage.OK);
			}
		}

		private async void OnUpdateSubClicked(object sender, EventArgs e)
		{
			Button button = sender as Button;

			GetList_SubCategory_SingleResponseModel singleModel = button.BindingContext as GetList_SubCategory_SingleResponseModel;

			if (singleModel != null)
			{
				string newName = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayPromptAsync(
					uiMessage.UPDATE_SUB_CATEGORY,
					string.Empty,
					accept: uiMessage.UPDATE,
					cancel: uiMessage.CANCEL,
					placeholder: singleModel.Name,
					maxLength: 25,
					keyboard: Keyboard.Text
					);

				if (string.IsNullOrWhiteSpace(newName))
					return;

				if (newName.Contains(','))
				{
					await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Please_not_use_comma, uiMessage.OK);
					return;
				}

				Update_SubCategory_CommandDto command = new Update_SubCategory_CommandDto
				{
					Id = singleModel.Id,
					CategoryId = _model.Id,
					Name = newName
				};

				BaseResponseModel<Unit> response = await ProxyCallerAsync<Update_SubCategory_CommandDto, Unit>(command);

				if (!string.IsNullOrEmpty(response.Message))
					return;

				_subCategories.FirstOrDefault(x => x.Id == singleModel.Id).Name = newName;

				Refresh(ref _subCategories, o => o.OrderBy(i => i.IsOther).ThenBy(n => n.Name), subCategoriesCollection);

				await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.SUCCESSFUL, uiMessage.Successfully_updated, uiMessage.OK);
			}
		}

		#endregion

		#region Delete

		private async void OnDeleteClicked(object sender, EventArgs e)
		{
			Button button = sender as Button;

			if (button.CommandParameter is Guid subCategoryId)
			{
				bool isConfirmed = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Are_you_sure, uiMessage.YES, uiMessage.NO);

				if (!isConfirmed)
					return;

				Delete_SubCategory_CommandDto command = new Delete_SubCategory_CommandDto
				{
					Id = subCategoryId
				};

				BaseResponseModel<Delete_SubCategory_ResponseDto> response = await ProxyCallerAsync<Delete_SubCategory_CommandDto, Delete_SubCategory_ResponseDto>(command);

				if (!string.IsNullOrEmpty(response.Message))
					return;

				if (response.Response.IsApprovalRequired)
				{
					bool isApproved = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Category_in_use, uiMessage.YES, uiMessage.NO);

					if (!isApproved)
						return;

					Delete_SubCategory_CommandDto reCommand = new Delete_SubCategory_CommandDto
					{
						Id = subCategoryId,
						IsApproved = true
					};

					BaseResponseModel<Delete_SubCategory_ResponseDto> reResponse = await ProxyCallerAsync<Delete_SubCategory_CommandDto, Delete_SubCategory_ResponseDto>(reCommand);

					if (!string.IsNullOrEmpty(reResponse.Message))
						return;
				}

				var remove = _subCategories.FirstOrDefault(c => c.Id == subCategoryId);

				if (remove != null)
					_subCategories.Remove(remove);

				Refresh(ref _subCategories, o => o.OrderBy(i => i.IsOther).ThenBy(n => n.Name), subCategoriesCollection);
			}
		}

		#endregion

	}
}
