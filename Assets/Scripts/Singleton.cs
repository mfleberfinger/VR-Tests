using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T instance { get; private set; }

	/// <summary>
	/// Sets the static variable "instance" or deletes this game object if an
	/// instance already exists.
	/// </summary>
	/// <param name="thisObject">Usually <c>this</c>.</param>
	protected void InitializeSingleton(T thisObject)
	{
		if(instance == null)
			instance = thisObject;
		else if(instance != thisObject)
		{
			Destroy(thisObject.gameObject);
			Debug.LogWarning("An extra instance of a singleton was detected and" +
				" destroyed.");
		}
	}
}