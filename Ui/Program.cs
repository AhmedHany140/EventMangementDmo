using Application.DTOs;
using System.Text.Json;
using System.Reflection;
using Domain.Entities;

namespace Ui
{
	public class Program
	{
		static string BaseUrl = "https://localhost:7299";

		static async Task Main(string[] args)
		{
			Console.BackgroundColor = ConsoleColor.Yellow;
			Console.ForegroundColor = ConsoleColor.Black;

			HttpClient client = new HttpClient();

			string[] Controllers = {
				"Acount", "ChatMessage", "Event", "Poll",
				"Registeration", "Resource", "Session",
				"Sponsor", "Ticket", "VirtualRoom", "EventSponsor"
			};

			Console.WriteLine("Fetching data from all controllers...\n");

			foreach (var controllerName in Controllers)
			{
				string endpointUrl = $"{BaseUrl}/api/{controllerName}/GetAll";

				try
				{
					Console.WriteLine($"=== {controllerName.ToUpper()} DATA ===");

					var response = await client.GetAsync(endpointUrl);

					if (!response.IsSuccessStatusCode)
					{
						Console.WriteLine($"Error fetching {controllerName}: {response.StatusCode}");
						Console.WriteLine($"Response: {await response.Content.ReadAsStringAsync()}");
						Console.WriteLine();
						continue;
					}

					var responseContent = await response.Content.ReadAsStringAsync();
					PrintJsonAsTable(responseContent);
					Console.WriteLine(); // Add space between controllers
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Exception while fetching {controllerName}: {ex.Message}");
					if (ex.InnerException != null)
						Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
					Console.WriteLine();
				}
			}

			Console.WriteLine("Finished fetching all data. Press any key to exit...");
			Console.ReadKey();
		}

		static void PrintJsonAsTable(string json)
		{
			try
			{
				using var doc = JsonDocument.Parse(json);
				var root = doc.RootElement;

				if (root.ValueKind != JsonValueKind.Array || root.GetArrayLength() == 0)
				{
					Console.WriteLine("No data available.");
					return;
				}

				// For each item in the array, print the properties vertically in columns
				foreach (var element in root.EnumerateArray())
				{
					foreach (var property in element.EnumerateObject())
					{
						// Print property name and its value
						Console.WriteLine($"{property.Name,-20} : {FormatValue(property.Value)}");
					}
					Console.WriteLine(new string('-', 50)); // Separate each record
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error parsing JSON: {ex.Message}");
			}
		}

		static string FormatValue(JsonElement value)
		{
			if (value.ValueKind == JsonValueKind.Null)
				return "NULL";

			if (value.ValueKind == JsonValueKind.Object || value.ValueKind == JsonValueKind.Array)
				return "[Complex Object]";

			return value.ToString();
		}
	}
}