﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D collision)
	{
		EventManager.TriggerEvent("EnterGate");
	}


}
