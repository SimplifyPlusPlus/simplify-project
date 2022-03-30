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
		var task = new Task(() => Console.Error.WriteLine(errorMessage));
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

			return httpResponse.StatusCode == HttpStatusCode.OK
				? await httpResponse.Content.ReadFromJsonAsync<T>()
				: new T();
		}
		catch (Exception e)
		{
			errorTask.Start();
			await errorTask;
			WriteException(e, endpoint);
		}

		return null;
	}

	private static void WriteException(Exception e, string endpoint)
	{
		Console.Error.WriteLine($"Обращение к эндпоинту {endpoint} привело к ошибке -> {e.Message}");
		Console.Error.WriteLine($"{e.StackTrace}");
	}
}