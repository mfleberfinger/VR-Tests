using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
	[Tooltip("Amount of money the player starts the game with")]
	[SerializeField]
	private float initialPay = 50f;

	[Tooltip("Time for which the player must fend off the children")]
	[SerializeField]
	private float timeLimit = 120f;

	// The XR Rig's watch/wrist display.
	private Watch watch;
	
	private float money, timeRemaining;
	private Overlord overlord;

	private void Start()
	{
		GameObject overlordGO = GameObject.FindGameObjectWithTag("Overlord");

		if(overlordGO == null)
			Debug.LogError("The Overlord script's game object could not be found." +
				" Add it to the scene and tag it, you dunce.");
		else
			overlord = overlordGO.GetComponent<Overlord>();

		GameObject watchGO = GameObject.FindGameObjectWithTag("Watch");
		
		if (watchGO != null)
			watch = watchGO.GetComponent<Watch>();
		else
			Debug.LogError("No GameObject with the tag \"Watch\" could be found.");

		money = initialPay;
		timeRemaining = timeLimit;
		watch.SetMode(WatchMode.manual);
	}

	private void Update()
	{
		if(timeRemaining <= 0)
		{
			overlord.AddMoney(money);
			overlord.ReturnToOverWorld();
		}
		timeRemaining -= Time.deltaTime;

		watch.SetText(string.Format("Time: {0:n2}\nPay: {1:c2}",
			timeRemaining, money));
	}

	public void DockPay(float amount)
	{
		money -= amount;
	}
}
