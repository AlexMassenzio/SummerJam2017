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
        xOffset = 2f;
        yOffset = 0.227f;
    }

    void Update () {
        if (character.GetComponent<PlayerManager>().changedDirection)
        {
            // Facing left
            if (character.GetComponent<SpriteRenderer>().flipX)
            {
                xOffset = -2f;
                yOffset = 0.227f;
            }
            // Facing right
            else
            {
                xOffset = 2f;
                yOffset = 0.227f;
            }
        }
        newPosition.x = character.transform.position.x + xOffset;
        newPosition.y = character.transform.position.y + yOffset;
        transform.position = newPosition;
	}
}
