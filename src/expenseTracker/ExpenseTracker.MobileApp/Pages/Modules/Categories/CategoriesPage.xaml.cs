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
		}

		// Backend'den kategori çekme
		private void LoadCategoriesFromBackend()
		{
			var backendCategories = new[]
			{
				new CategoryModel { Name = "Yemek" },
				new CategoryModel { Name = "Ulaşım" },
				new CategoryModel { Name = "Eğlence" },
				new CategoryModel { Name = "Market" },
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
	}
}
