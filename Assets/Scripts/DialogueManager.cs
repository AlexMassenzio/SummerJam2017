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

	// Use this for initialization
	void Start ()
    {
        player = FindObjectOfType<PlayerController>();
        
        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        // If endLine is 0, go through the entire text file
        if (endLine == 0)
        {
            endLine = textLines.Length - 1;
        }

	}

	void Update()
	{

		theText.text = textLines[currentLine];

		if (Input.GetMouseButtonDown(0))
		{
			currentLine++;
		}
		
		if (currentLine > endLine)
		{
			textBox.SetActive(false);
		}	
	}

}
