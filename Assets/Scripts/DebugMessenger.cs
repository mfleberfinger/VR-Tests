using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Simple script for displaying messages that can be read by a person wearing
/// a VR headset.
/// </summary>
public class DebugMessenger : Singleton<DebugMessenger>
{
	[Tooltip("The Text object to use for debug messages.")]
	[SerializeField]
	private Text m_debugText;

	private void Awake()
	{
		InitializeSingleton(this);
	}

	private void Update()
	{
		if(Input.GetButtonDown("Fire2"))
			ToggleDisplay();
	}

	/// <summary>
	/// Set the entire debug text area to a given string.
	/// </summary>
	/// <param name="text">The text to display.</param>
	public void SetDebugText(string text)
	{
		m_debugText.text = text;
	}

	/// <summary>
	/// Add a string to the displayed text on a new line. If the text box is filled,
	/// remove the oldest (top) line.
	/// </summary>
	/// <param name="text">The text to display.</param>
	public void ConcatDebugText(string text)
	{
		m_debugText.text =  m_debugText.text + "\n" + text;
	}

	/// <summary>
	/// Hide the debug text if it is visible. If it is hidden, make it visible.
	/// </summary>
	public void ToggleDisplay()
	{
		if(m_debugText.enabled)
			m_debugText.enabled = false;
		else
			m_debugText.enabled = true;
	}
}
