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

	/// <summary>
	/// Set the watch mode. It should be set to "Manual" to manual to set the
	/// display directly from another script.
	/// </summary>
	/// <param name="mode">The WatchMode enum member describing the mode
	/// to use.</param>
	public void SetMode(WatchMode mode)
	{
		this.mode = mode;
	}

	/// <summary>
	/// Set the text displayed on the watch.
	/// </summary>
	/// <param name="displayText">The text to display on the next Update() call.</param>
	public void SetText(string displayText)
	{
		this.displayText.text = displayText;
	}
}
