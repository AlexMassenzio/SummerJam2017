using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAttackPosition : MonoBehaviour {

    public GameObject character;
    private Vector2 newPosition;
    public float xOffset;
    public float yOffset;

    private void Start()
    { 
        newPosition = transform.position;
    }
    
    void Update () {
        // TODO change offsets based on character orientation

        newPosition.x = character.transform.position.x + xOffset;
        newPosition.y = character.transform.position.y + yOffset;
        transform.position = newPosition;
	}
}
