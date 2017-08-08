using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public GameObject textBox;

	public Text theText;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endLine;

	public PlayerController player;

	[SerializeField]
	private bool speaking;

	// Use this for initialization
	void Start()
	{
		player = FindObjectOfType<PlayerController>();

		if (textFile != null)
		{
			textLines = (textFile.text.Split('\n'));
		}

		//EventManager.TriggerEvent("uvulaInjure");

		//speaking = false;

	}

	private void Update()
	{
		if (speaking)
		{
			// TODO: Pause game

			theText.text = textLines[currentLine];

			if (Input.GetMouseButtonDown(0))
			{
				currentLine++;
			}

			if (currentLine > endLine)
			{
				//textBox.SetActive(false);
				currentLine = 0;
				endLine = 0;
				speaking = false;
			}
		}
	}

	public void SpeakDialogue(int start, int end)
	{
		if (start == 0)
		{
			Debug.Log("just recieved the goods my dude");
		}
		Debug.Log("started dialogue with " + start + " and " + end);
		speaking = true;
		currentLine = start;
		endLine = end;
	}

}
