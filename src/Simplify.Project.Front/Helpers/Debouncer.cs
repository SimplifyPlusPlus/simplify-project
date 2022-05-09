namespace Simplify.Project.Front.Helpers;

public class Debouncer
{
	public static Action<T> Debounce<T>(Action<T> action, TimeSpan interval)
	{
		if (action == null) throw new ArgumentNullException(nameof(action));

		var last = 0;
		return arg =>
		{
			var current = System.Threading.Interlocked.Increment(ref last);
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
