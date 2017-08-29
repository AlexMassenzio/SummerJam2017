using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAttackPosition : MonoBehaviour {

    public GameObject character;
    private PlayerController pc;
    private Vector2 newPosition;
    public float xOffset;
    public float yOffset;

    private void Start()
    {
        pc = transform.parent.gameObject.GetComponent<PlayerController>();
        newPosition = transform.position;
        xOffset = 2f;
        yOffset = 0.227f;
    }

    void Update () {
        if (character.GetComponent<PlayerManager>().changedDirection)
        {
            Debug.Log("crouching: " + pc.crouching);
            if (pc.crouching)
            {
                // Facing left
                if (character.GetComponent<SpriteRenderer>().flipX)
                {
                    xOffset = -1.1f;
                    yOffset = -1;
                }
                // Facing right
                else
                {
                    xOffset = 2f;
                    yOffset = -1;
                }
            }
            else
            {
                // Facing left
                if (character.GetComponent<SpriteRenderer>().flipX)
                {
                    xOffset = -1.1f;
                    yOffset = 0.227f;
                }
                // Facing right
                else
                {
                    xOffset = 2f;
                    yOffset = 0.227f;
                }
            }
            
        }
        newPosition.x = character.transform.position.x + xOffset;
        newPosition.y = character.transform.position.y + yOffset;
        transform.position = newPosition;
	}
}
