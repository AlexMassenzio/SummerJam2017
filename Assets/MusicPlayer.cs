using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	private static MusicPlayer instance = null;
	public static MusicPlayer Instance
	{
		get { return instance; }
	}

	public AudioClip backgroundMusic;
	public AudioClip bossMusic;

	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);
        transform.parent = null;
	}

	private void Update()
	{
		if(SceneManager.GetActiveScene().name == "BossLevel" && GetComponent<AudioSource>().clip != bossMusic)
		{
			GetComponent<AudioSource>().clip = bossMusic;
            GetComponent<AudioSource>().Play();

        }
	}
}
