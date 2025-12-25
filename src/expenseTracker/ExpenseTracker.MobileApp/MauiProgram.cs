using ExpenseTracker.Application.UseCases;
using ExpenseTracker.Infrastructure.Repositories;

namespace ExpenseTracker.MobileApp
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

			string appDataDirectory = FileSystem.AppDataDirectory;

			string[] seedFiles = { "categories.csv", "subcategories.csv" };

			foreach (var file in seedFiles)
			{
				string destPath = Path.Combine(appDataDirectory, file);

				File.Delete(destPath);

				if (!File.Exists(destPath))
				{
					using (var stream = FileSystem.OpenAppPackageFileAsync($"SeedData/{file}").Result)
					{
						using (var reader = new StreamReader(stream))
						{
							File.WriteAllText(destPath, reader.ReadToEnd());
						}
					}
				}
			}

			builder.Services.AddMobileAppServices();
			builder.Services.AddUseCaseCommonServices();
			builder.Services.AddRepositoryServices(appDataDirectory);

			return builder.Build();
		}

	}
}
