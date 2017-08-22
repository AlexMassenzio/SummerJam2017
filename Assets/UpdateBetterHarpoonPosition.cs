using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBetterHarpoonPosition : MonoBehaviour
{

    public GameObject mack;
    public GameObject character;
    private Vector2 newPosition;
    public float xOffset;
    public float yOffset;

    public float Y_STANDING_OFFSET = 3.8f;
    public float Y_CROUCHING_OFFSET = 3.25f;

    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
        mack = character.transform.parent.gameObject;

        newPosition = transform.position;
        xOffset = -3.9f;
        yOffset = Y_STANDING_OFFSET;
    }

    void Update()
    {
        if (character.GetComponent<PlayerManager>().changedDirection)
        {
            // Facing left
            if (character.GetComponent<SpriteRenderer>().flipX)
            {
                xOffset = -11.6f;
            }
            // Facing right
            else
            {
                xOffset = -3.9f;
            }
        }

        if (!mack.GetComponent<MackAttack>().attacking)
        {
            if (mack.GetComponent<PlayerController>().crouching)
            {
                yOffset = Y_CROUCHING_OFFSET;
            }
            else
            {
                yOffset = Y_STANDING_OFFSET;
            }
        }
        

        newPosition.x = character.transform.position.x + xOffset;
        newPosition.y = character.transform.position.y + yOffset;
        transform.position = newPosition;
    }
}
