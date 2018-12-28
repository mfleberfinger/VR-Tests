using System;
using UnityEngine;
using UnityEngine.UI;

// Allows the watch display to show time or be controlled by other scripts.
public class Watch : MonoBehaviour
{
	[Tooltip("How should the watch behave?")]
	[SerializeField]
	private WatchMode mode = WatchMode.clock;

	[Tooltip("The UI Text object on the watch face.")]
	[SerializeField]
	private Text displayText = null;

	private void Update()
	{
		if (mode == WatchMode.clock)
			displayText.text = DateTime.Now.ToLocalTime().ToShortTimeString();
	}
}
