using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstRoomFlow : MonoBehaviour {

	DialogueManager dm;
	private UnityAction uvulaListener;

	//[SerializeField]
	public GameObject slime;
	public GameObject harpoonPickup;
	
	private bool harpoonGet = false;
	private bool slimeKilled = false;

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
			}
		}
	}
}
