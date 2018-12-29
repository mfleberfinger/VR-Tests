using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overscore : MonoBehaviour
{
    [Tooltip("Text component of the scoreboard.")]
	[SerializeField]
	private Text scoreText = null;

	private Overlord overlord;

	private void Start()
	{
		GameObject overlordGO = GameObject.FindGameObjectWithTag("Overlord");

		if(overlordGO == null)
			Debug.LogError("The Overlord script's game object could not be found." +
				" Add it to the scene and tag it, you dunce.");
		else
			overlord = overlordGO.GetComponent<Overlord>();
	}

	private void Update()
	{
		scoreText.text = string.Format("Greetings, citizen.\n" +
			"Know your worth.\n" +
			"Money: {0:c2}\n" +
			"Debt: {1:n5} AYW", overlord.m_money, overlord.m_AYW);
	}
}
