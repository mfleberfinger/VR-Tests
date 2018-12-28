using UnityEngine;

public class SoundManager : MonoBehaviour 
{
	[Tooltip("Audio source component for this sound producing object.")]
	[SerializeField]
	private AudioSource efxSource;
	
	//used to produce random variation in pitch of sound effects
	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;

	/// <summary>
	/// Play a single audio clip.
	/// </summary>
	/// <param name="clip">Clip to play.</param>
	public void PlaySingle(AudioClip clip)
	{
		efxSource.clip = clip;
		efxSource.Play();
	}
		
	/// <summary>
	/// Play a random clip from a list of clips.
	/// </summary>
	/// <param name="clips">A list of clips to choose from.</param>
	public void RandomizeSfx(params AudioClip[] clips)
	{
		//choose a random clip from the clips argument
		int randomIndex = Random.Range(0, clips.Length);
		//choose a random pitch
		float randomPitch = Random.Range(lowPitchRange, highPitchRange);

		//set the pitch and clip, then play the clip
		efxSource.pitch = randomPitch;
		efxSource.clip = clips[randomIndex];
		efxSource.Play();
	}
}
