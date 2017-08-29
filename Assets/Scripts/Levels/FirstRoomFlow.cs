using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstRoomFlow : MonoBehaviour {

	DialogueManager dm;
	private UnityAction uvulaListener;

	public GameObject slime;
	public GameObject harpoonPickup;
	
	private enum FirstRoomState { Start, HarpoonGet, UvulaHit, SlimeKilled };
	private FirstRoomState state = FirstRoomState.Start;
	private bool harpoonGet = false;

	private void Start()
	{
		dm = FindObjectOfType<DialogueManager>();
		dm.SpeakDialogue(0, 1);
	}

	void Awake()
	{
		uvulaListener = new UnityAction(WhaleSpeak);	
	}

	void WhaleSpeak()
	{

		switch(state)
		{
			case FirstRoomState.Start:
				dm.SpeakDialogue(2, 2);
				state = FirstRoomState.UvulaHit;
				break;
			case FirstRoomState.UvulaHit:
				dm.SpeakDialogue(3, 3);
				slime.SetActive(true);
				state = FirstRoomState.HarpoonGet;
				break;
			case FirstRoomState.HarpoonGet:
				dm.SpeakDialogue(4, 5);
				state = FirstRoomState.SlimeKilled;
				break;
		}

		/*
		if (!harpoonGet && !slimeKilled)
		{
			harpoonGet = true;
			dm.SpeakDialogue(2, 3);
		}
		else if (harpoonGet && !slimeKilled)
		{
			slimeKilled = true;
			dm.SpeakDialogue(4, 5);
		}
		*/
	}

	void OnEnable()
	{
		EventManager.StartListening("harpoonGetEvent", uvulaListener);
	}

	void OnDisable()
	{
		EventManager.StopListening("harpoonGetEvent", uvulaListener);
	}

	void Update()
	{
		if (!harpoonGet)
		{
			if (harpoonPickup == null)
			{
				WhaleSpeak();
				harpoonGet = true;
			}
		}
		if(slime == null && state == FirstRoomState.HarpoonGet)
		{
			WhaleSpeak();
		}
	}
}
