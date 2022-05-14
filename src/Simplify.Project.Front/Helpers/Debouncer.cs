namespace Simplify.Project.Front.Helpers;

/// <summary>
/// Дебаунсер
/// </summary>
public class Debouncer
{

	/// <summary>
	/// Откладывает выполнение функции, если пользователь совершает какое-либо действие
	/// </summary>
	/// <param name="action">Функция</param>
	/// <param name="interval">Время неактивности, после которого выполнится функция</param>
	/// <exception cref="ArgumentNullException">Функция является null</exception>
	public static Action<T> Debounce<T>(Action<T> action, TimeSpan interval)
	{
		ArgumentNullException.ThrowIfNull(action, nameof(action));

		var last = 0;
		return arg =>
		{
			var current = Interlocked.Increment(ref last);
			Task.Delay(interval).ContinueWith(task =>
			{
				if (current == last)
				{
					action(arg);
				}
			});
		};
	}
}
