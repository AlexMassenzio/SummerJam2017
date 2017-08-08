using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHarpoonPosition : MonoBehaviour
{

    public GameObject character;
    private Vector2 newPosition;
    public float xOffset;
    public float yOffset;

    private void Start()
    {
        newPosition = transform.position;
        xOffset = -3.9f;
        yOffset = 3.92f;
    }

    void Update()
    {
        if (character.GetComponent<PlayerManager>().changedDirection)
        {
            // Facing left
            if (character.GetComponent<SpriteRenderer>().flipX)
            {
                xOffset = -9.14f;
            }
            // Facing right
            else
            {
                xOffset = -3.9f;
            }
        }
        newPosition.x = character.transform.position.x + xOffset;
        newPosition.y = character.transform.position.y + yOffset;
        transform.position = newPosition;
    }
}
