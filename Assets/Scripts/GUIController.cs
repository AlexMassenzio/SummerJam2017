using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class GUIController : MonoBehaviour {

	private Canvas canvas;
	private GameObject respawnButton;
	private RectTransform healthBarRT, staminaBarRT;
	private RawImage blackMask;
    private GameObject youDied;

	private CharacterStats cs;

	float maxHealthLength, maxStaminaLength;

	private UnityAction fadeToBlackListener;
    private UnityAction youDiedListener;

	// Use this for initialization
	void Start () {
		canvas = GetComponent<Canvas>();

		respawnButton = transform.Find("RespawnButton").gameObject;
		respawnButton.SetActive(false);

		healthBarRT = transform.Find("Status").transform.Find("HealthBar").gameObject.GetComponent<RectTransform>();
		staminaBarRT = transform.Find("Status").transform.Find("StaminaBar").gameObject.GetComponent<RectTransform>();
		maxHealthLength = healthBarRT.rect.width * canvas.scaleFactor;
		maxStaminaLength = staminaBarRT.rect.width * canvas.scaleFactor;

        youDied = transform.Find("YouDied").gameObject;
        youDied.SetActive(false);

		blackMask = transform.Find("BlackMask").gameObject.GetComponent<RawImage>();

		cs = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CharacterStats>();
		Debug.Log(cs.gameObject.name);
	}

	void Awake()
	{
		fadeToBlackListener = new UnityAction(FadeToBlack);
        youDiedListener = new UnityAction(FadeInDeath);
	}

	void OnEnable()
	{
		EventManager.StartListening("FadeToBlack", fadeToBlackListener);
        EventManager.StartListening("FadeInDeath", youDiedListener);
	}

	void OnDisable()
	{
		EventManager.StopListening("FadeToBlack", fadeToBlackListener);
        EventManager.StopListening("FadeInDeath", youDiedListener);
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

	private void FadeToBlack()
	{
		StartCoroutine(FadeToBlackHelper(true));
	}

	IEnumerator FadeToBlackHelper(bool goToNextLevel)
    {
        Color placeholderColor = blackMask.color;
        float progress = 0;

        while (progress < 1)
        {
            placeholderColor.a = LeanTween.linear(0, 1, progress);
            blackMask.color = placeholderColor;

            progress += Time.deltaTime;

            yield return null;
        }

        if (goToNextLevel)
        {
            EventManager.TriggerEvent("LoadNextLevel");
        }
	}

    private void FadeInDeath() 
    {
        youDied.SetActive(true);
        StartCoroutine(FadeInDeathHelper());
    }

    IEnumerator FadeInDeathHelper()
    {
        Image deathImage = youDied.GetComponent<Image>();
        Color placeholderColor = deathImage.color;
        float progress = 0;

        while(progress < 1)
        {
            placeholderColor.a = LeanTween.linear(0, 1, progress);
            deathImage.color = placeholderColor;

            progress += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeToBlackHelper(false));
    }
}
