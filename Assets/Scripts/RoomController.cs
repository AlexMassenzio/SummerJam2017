using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour {

	private UnityAction deathListener;
	private UnityAction leaveListener;

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
		gs = GameState.Init;

		gui = GameObject.FindGameObjectWithTag("GUI");
		gui.SetActive(false);
	}

	void OnEnable()
	{
		EventManager.StartListening("MackDeath", deathListener);
		EventManager.StartListening("EnterGate", leaveListener);
	}

	void OnDisable()
	{
		EventManager.StopListening("MackDeath", deathListener);
		EventManager.StopListening("EnterGate", leaveListener);
	}

	// Update is called once per frame
	void Update () {
		if (gs == GameState.Death)
		{
			if(firstFrameInState)
			{
				Debug.Log("Entered State: Death");
				gui.SetActive(true);
			}
		}
		else if(gs == GameState.Leave)
		{
			if(firstFrameInState)
			{
				Debug.Log("Entered State: Leave");
				//TODO: Add level switch info here
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
	#endregion
}
