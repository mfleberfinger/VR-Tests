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
	

	private void Start()
	{
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
		timeRemaining -= Time.deltaTime;

		watch.SetText(string.Format("Time: {0:n0}\nPay: {1:c2}",
			timeRemaining, money));
	}

	public void DockPay(float amount)
	{
		money -= amount;
	}
}
