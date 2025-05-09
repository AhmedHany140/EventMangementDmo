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
			HttpClient client = new HttpClient();

			string[] Controllers = {
				"Acount", "ChatMessage", "Event", "Poll",
				"Registeration", "Resource", "Session",
				"Sponsor", "Ticket", "VirtualRoom" ,"EventSponsor","SessionSpeaker"
			}; 

			Console.WriteLine("Available Controllers:");
			Console.WriteLine(string.Join(", ", Controllers));

			Console.Write("Enter Controller Name: ");
			string controllerName = Console.ReadLine()?.Trim();

			// Validate input
			if (!Controllers.Contains(controllerName, StringComparer.OrdinalIgnoreCase))
			{
				Console.WriteLine("Invalid controller name.");
				Console.ReadKey();
				return;
			}

			// Fix case sensitivity
			var correctControllerName = Controllers.First(c =>
				c.Equals(controllerName, StringComparison.OrdinalIgnoreCase));

			string endpointUrl = $"{BaseUrl}/api/{correctControllerName}/GetAll";

			try
			{
				Console.WriteLine($"Sending request to: {endpointUrl}");

				var response = await client.GetAsync(endpointUrl);

				if (!response.IsSuccessStatusCode)
				{
					Console.WriteLine($"Error: {response.StatusCode}");
					Console.WriteLine($"Response: {await response.Content.ReadAsStringAsync()}");
					Console.ReadKey();
					return;
				}

				var responseContent = await response.Content.ReadAsStringAsync();

				// Format and display JSON as table with properties in one column
				PrintJsonAsTable(responseContent);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Exception: {ex.Message}");
				if (ex.InnerException != null)
					Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
			}

			Console.WriteLine("\nPress any key to exit...");
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
					Console.WriteLine("No data to display.");
					return;
				}

				// For each item in the array, print the properties vertically in columns
				foreach (var element in root.EnumerateArray())
				{
					foreach (var property in element.EnumerateObject())
					{
						// Print property name and its value
						Console.WriteLine($"{property.Name,-20} : {property.Value}");
					}
					Console.WriteLine(new string('-', 50)); // Separate each record
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error parsing JSON: {ex.Message}");
			}
		}
	}
}
