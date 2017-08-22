using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

	private Canvas canvas;
	private GameObject respawnButton;
	private RectTransform healthBarRT, staminaBarRT;

	[SerializeField]
	private CharacterStats cs;

	float maxHealthLength, maxStaminaLength;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();

		respawnButton = transform.Find("RespawnButton").gameObject;
		respawnButton.SetActive(false);

		healthBarRT = transform.Find("Status").transform.Find("HealthBar").gameObject.GetComponent<RectTransform>();
		staminaBarRT = transform.Find("Status").transform.Find("StaminaBar").gameObject.GetComponent<RectTransform>();
		maxHealthLength = healthBarRT.rect.width * canvas.scaleFactor;
		maxStaminaLength = staminaBarRT.rect.width * canvas.scaleFactor;

		cs = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CharacterStats>();
		Debug.Log(cs.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		float hpPercentage = (float)cs.health / (float)cs.maxHealth;
		healthBarRT.sizeDelta = new Vector2(hpPercentage * maxHealthLength - maxHealthLength, healthBarRT.sizeDelta.y);

		float staminaPercentage = (float)cs.stamina / (float)cs.maxStamina;
		staminaBarRT.sizeDelta = new Vector2(staminaPercentage * maxStaminaLength - maxStaminaLength, staminaBarRT.sizeDelta.y);
	}



	public void enableDeathGUI()
	{
		respawnButton.SetActive(true);
	} 
}
