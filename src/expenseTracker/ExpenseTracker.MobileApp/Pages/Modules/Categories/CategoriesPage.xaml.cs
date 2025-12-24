using ExpenseTracker.Domain.Resources;
using ExpenseTracker.MobileApp.Constants;
using System.Collections.ObjectModel;

namespace ExpenseTracker.MobileApp.Pages.Modules.Categories
{
	public partial class CategoriesPage : ContentPage
	{
		public ObservableCollection<CategoryModel> Categories { get; set; } = new ObservableCollection<CategoryModel>();

		public CategoriesPage()
		{
			InitializeComponent();
			categoriesCollection.ItemsSource = Categories;
			LoadCategoriesFromBackend();

			lblNew.Text = uiMessage.CATEGORIES;
			lblNew.TextColor = ColorConstants.Purple;

			btnNew.Text = $"+ {uiMessage.NEW}";
			btnNew.BackgroundColor = ColorConstants.Purple;
		}

		// Backend'den kategori çekme
		private void LoadCategoriesFromBackend()
		{
			var backendCategories = new[]
			{
				new CategoryModel { Name = "Beslenme",        RowColor = Color.FromArgb("#D9CCF9") },
				new CategoryModel { Name = "Eğlence",         RowColor = Color.FromArgb("#A895EA") },
				new CategoryModel { Name = "Faturalar",       RowColor = Color.FromArgb("#D9CCF9") },
				new CategoryModel { Name = "Giyim",           RowColor = Color.FromArgb("#A895EA") },
				new CategoryModel { Name = "Kişisel Gelişim", RowColor = Color.FromArgb("#D9CCF9") },
				new CategoryModel { Name = "Sağlık",          RowColor = Color.FromArgb("#A895EA") },
				new CategoryModel { Name = "Teknoloji",       RowColor = Color.FromArgb("#D9CCF9") },
				new CategoryModel { Name = "Ulaşım",          RowColor = Color.FromArgb("#A895EA") },
				new CategoryModel { Name = "Diğer",           RowColor = Color.FromArgb("#D9CCF9") }
			};

			foreach (var cat in backendCategories)
				Categories.Add(cat);
		}

		// Yeni kategori ekleme
		private async void OnAddCategoryClicked(object sender, EventArgs e)
		{
			string result = await DisplayPromptAsync("Yeni Kategori", "Kategori adı girin:");
			if (!string.IsNullOrWhiteSpace(result))
			{
				var newCategory = new CategoryModel { Name = result.Trim() };
				Categories.Add(newCategory);

				// TODO: Burada backend'e POST isteği at
			}
		}

		// Kategori silme
		private async void OnDeleteCategoryClicked(object sender, EventArgs e)
		{
			if (sender is Button btn && btn.BindingContext is CategoryModel category)
			{
				bool confirm = await DisplayAlert("Sil", $"'{category.Name}' kategorisini silmek istediğinize emin misiniz?", "Evet", "Hayır");
				if (confirm)
				{
					Categories.Remove(category);

					// TODO: Burada backend'e DELETE isteği at
				}
			}
		}
	}

	public class CategoryModel
	{
		public string Name { get; set; }
		public Color RowColor { get; set; }
	}
}
