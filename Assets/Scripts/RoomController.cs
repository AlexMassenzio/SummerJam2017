using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour {

	private UnityAction deathListener;
	private UnityAction leaveListener;
	private UnityAction loadLevelListener;

	private bool firstFrameInState;

	enum GameState { Init, Play, Death, Leave, Pause};
	private GameState gs;

	private GameObject gui;

	void Start()
	{
		
	}

	void Awake () {
		deathListener = new UnityAction(Death);
		leaveListener = new UnityAction(Leave);
		loadLevelListener = new UnityAction(LoadNextLevel);
		gs = GameState.Init;

		gui = GameObject.FindGameObjectWithTag("GUI");
	}

	void OnEnable()
	{
		EventManager.StartListening("MackDeath", deathListener);
		EventManager.StartListening("EnterGate", leaveListener);
		EventManager.StartListening("LoadNextLevel", loadLevelListener);
	}

	void OnDisable()
	{
		EventManager.StopListening("MackDeath", deathListener);
		EventManager.StopListening("EnterGate", leaveListener);
		EventManager.StopListening("LoadNextLevel", loadLevelListener);
	}

	// Update is called once per frame
	void Update () {
		if (gs == GameState.Death)
		{
			if(firstFrameInState)
			{
				Debug.Log("Entered State: Death");
                StartCoroutine(DeathSequence());
				//DEPRECATED: gui.GetComponent<GUIController>().enableDeathGUI();
			}
		}
		else if(gs == GameState.Leave)
		{
			if (firstFrameInState)
			{
				Debug.Log("Entered State: Leave");
				EventManager.TriggerEvent("FadeToBlack");
			}
		}

		//KEEP AT BOTTOM OF UPDATE
		firstFrameInState = false;
	}

	#region GameState Functions
	private void Death()
	{
		if (gs != GameState.Death)
		{
			firstFrameInState = true;
		}
		gs = GameState.Death;
	}

	private void Leave()
	{
		if (gs != GameState.Leave)
		{
			firstFrameInState = true;
		}
		gs = GameState.Leave;
	}
	#endregion

	#region Misc Functions
	public void Respawn()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

    IEnumerator DeathSequence()
    {
        EventManager.TriggerEvent("FadeInDeath");
        yield return new WaitForSeconds(4f);
        Respawn();
    }
	#endregion
}
