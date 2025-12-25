using AutoMapper;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.CreateCategoryCommand.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Command.DeleteCategoryCommand.Dtos;
using ExpenseTracker.Application.UseCases.Modules.Category.Query.GetListCategoryQuery.Dtos;
using ExpenseTracker.Domain.Resources.Languages;
using ExpenseTracker.MobileApp.Base;
using ExpenseTracker.MobileApp.Constants;
using ExpenseTracker.MobileApp.Helpers;
using ExpenseTracker.MobileApp.Pages.Modules.Categories.Dtos;
using MediatR;
using System.Collections.ObjectModel;

namespace ExpenseTracker.MobileApp.Pages.Modules.Categories
{
	public partial class CategoriesPage : BaseContentPage
	{

		private ObservableCollection<GetList_Category_SingleResponseModel> _categories;

		public CategoriesPage(IMediator mediator, IMapper mapper)
			: base(mediator, mapper,)
		{
			InitializeComponent();

			lblCategories.Text = uiMessage.CATEGORIES;
			lblCategories.TextColor = ColorConstants.Purple;

			btnNew.Text = $"+ {uiMessage.NEW}";
			btnNew.BackgroundColor = ColorConstants.Purple;
		}

		#region Create

		private async void OnCreateCategoryClicked(object sender, EventArgs e)
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
				Culture = PreferencesHelper.GetCulture(),
				Name = newCategory
			};

			await _mediator.Send(command);

			GetList_Category_SingleResponseModel newRecord = _mapper.Map<GetList_Category_SingleResponseModel>(command);
			int newIdex = _categories.LastOrDefault()?.Index ?? 0;
			newRecord.RowColor = newIdex % 2 == 0 ? ColorConstants.MiddlePurple : ColorConstants.SoftPurple;

			_categories.Add(newRecord);
		}

		#endregion

		#region Read

		public async Task LoadDataAsync()
		{
			base.OnAppearing();

			GetList_Category_QueryDto query = new GetList_Category_QueryDto
			{
				Culture = PreferencesHelper.GetCulture()
			};

			GetList_Category_ResponseDto response = await _mediator.Send(query);

			List<GetList_Category_SingleResponseModel> records = _mapper.Map<List<GetList_Category_SingleResponseModel>>(response.Records);

			_categories = new ObservableCollection<GetList_Category_SingleResponseModel>(records);

			categoriesCollection.ItemsSource = _categories;
		}

		#endregion

		#region Update

		private async void OnUpdateCategoryClicked(object sender, EventArgs e)
		{
			Button button = sender as Button;

			if (button.CommandParameter is Guid categoryId)
			{

			}
		}

		#endregion

		#region Delete

		private async void OnDeleteCategoryClicked(object sender, EventArgs e)
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

				Delete_Category_ResponseDto response = await _mediator.Send(command);

				if (response.IsApprovalRequired)
				{
					bool isApproved = await Microsoft.Maui.Controls.Application.Current.MainPage.DisplayAlert(uiMessage.WARNING, uiMessage.Category_in_use, uiMessage.YES, uiMessage.NO);

					if (!isApproved)
						return;

					Delete_Category_CommandDto reCommand = new Delete_Category_CommandDto
					{
						Id = categoryId,
						IsApproved = true
					};

					await _mediator.Send(reCommand);
				}

				var remove = _categories.FirstOrDefault(c => c.Id == categoryId);

				if (remove != null)
					_categories.Remove(remove);
			}
		}

		#endregion

	}

}