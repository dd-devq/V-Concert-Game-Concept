//========================================================
// - for making singleton object
// - usage
//		+ declare class(derived )	
//			public class OnlyOne : BaseSingleton< OnlyOne >
//		+ client
//			OnlyOne.Instance.[method]
//========================================================

using UnityEngine;

public abstract class Singleton<T> where T : new()
{
	private static T _instance;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new T();
			}
			return _instance;
		}
	}
}

/// <summary>
/// Singleton for mono behavior object, only return existed object, don't create new
/// </summary>
/// <typeparam name="T"></typeparam>
public class ManualSingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	private static bool _applicationIsQuitting;

	public static T Instance
	{
		get
		{
			if (_applicationIsQuitting)
				return null;

			if (!_instance)
			{
				Debug.LogError("Cannot find Object with type " + typeof(T));
			}

			return _instance;
		}
	}

	public static bool IsInstanceValid()
	{
		return _instance != null;
	}

	//MUST OVERRIDE AWAKE AT CHILD CLASS
	public virtual void Awake()
	{
		if (_instance != null)
		{
			Debug.LogWarning("Already has instance of " + typeof(T));
			Destroy(this);
			return;
		}

		if (_instance == null)
			_instance = (T)(MonoBehaviour)this;

		if (_instance == null)
		{
			Debug.LogError("NULL after Awake  " + typeof(T));
		}
	}

	protected virtual void OnDestroy()
	{
		if (_instance == this)
		{
			_instance = null;
		}
	}


	private void OnApplicationQuit()
	{
		_applicationIsQuitting = true;
	}
}