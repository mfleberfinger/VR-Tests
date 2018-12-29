using UnityEngine;
using UnityEngine.SceneManagement;

public class Overlord : Singleton<Overlord>
{
	[Tooltip("The overworld scene's number in the build order.")]
	[SerializeField]
	private int overworldScene = 0;

	public float m_money { private set; get; }
	public float m_AYW { private set; get; }



    private void Awake()
	{
		InitializeSingleton(this);
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		m_money = 0;
		m_AYW = 50;
		Random.InitState(System.DateTime.Now.GetHashCode());
	}

	/// <summary>
	/// Add money to the player's overworld score. May be negative.
	/// </summary>
	/// <param name="amount">Amount of money to add. May be negative.</param>
	public void AddMoney(float amount)
	{
		m_money += amount;
		m_AYW -= amount * 0.0001f * Random.value;
	}

	/// <summary>
	/// Returns player to overworld scene.
	/// </summary>
	public void ReturnToOverWorld()
	{
		SceneManager.LoadScene(overworldScene);
	}
}
