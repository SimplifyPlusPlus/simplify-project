using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Simplify.Project.Front.Helpers;

/// <summary>
/// Помощник Http-клиента
/// </summary>
public static class HttpClientHelper
{
	/// <summary>
	/// Получить данные с сервера
	/// </summary>
	/// <param name="httpClient">Http-клиент</param>
	/// <param name="endpoint">Адрес запроса</param>
	/// <param name="errorMessage">Сообщение об ошибке</param>
	/// <typeparam name="T">Тип возвращаемых данных</typeparam>
	/// <returns>Данные</returns>
	public static async Task<T?> GetJsonFromServer<T>(HttpClient httpClient, string endpoint, string errorMessage)
		where T : class, new()
	{
		var task = new Task(() => Console.Error.WriteLineAsync(errorMessage));
		return await GetJsonFromServer<T>(httpClient, endpoint, task);
	}
	
	/// <summary>
	/// Получить данные с сервера
	/// </summary>
	/// <param name="httpClient">Http-клиент</param>
	/// <param name="endpoint">Адрес запроса</param>
	/// <param name="errorTask">Действие при возникновении ошибки</param>
	/// <typeparam name="T">Тип возвращаемых данных</typeparam>
	/// <returns>Данные</returns>
	public static async Task<T?> GetJsonFromServer<T>(HttpClient httpClient, string endpoint, Task errorTask)
		where T : class, new()
	{
		if (httpClient is null)
			throw new ArgumentNullException(nameof(httpClient));

		if (string.IsNullOrEmpty(endpoint))
			throw new ArgumentNullException(nameof(endpoint));

		try
		{
			using var httpResponse = await httpClient.GetAsync(endpoint);

			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			};

			if (httpResponse.StatusCode == HttpStatusCode.OK)
			{
				return await httpResponse.Content.ReadFromJsonAsync<T>(options);
			}
			else
			{
				return new T();
			}
			
			return httpResponse.StatusCode == HttpStatusCode.OK
				? await httpResponse.Content.ReadFromJsonAsync<T>(options)
				: new T();
		}
		catch (Exception e)
		{
			errorTask.Start();
			await errorTask;
			await WriteException(e, endpoint);
		}

		return null;
	}

	private static async Task WriteException(Exception e, string endpoint)
	{
		await Console.Error.WriteLineAsync("--- Начало ошибки --------------------");
		await Console.Error.WriteLineAsync($"Обращение к эндпоинту {endpoint} привело к ошибке -> {e.Message}");
		await Console.Error.WriteLineAsync($"{e.StackTrace}");
		await Console.Error.WriteLineAsync("--- Конец ошибки ---------------------");
	}
}