using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public float getHorizontalMovement()
    {
        float horizontal = 0f;
        //Debug.Log(playerInputActions.Player.HorizontalMove.ReadValue<Vector2>().x);

        //float horizontal = 0f;

        // Check if A key is pressed
        if (Input.GetKey(KeyCode.A))
        {
            horizontal -= 1f; // Move left
        }

        // Check if D key is pressed
        if (Input.GetKey(KeyCode.D))
        {
            horizontal += 1f; // Move right
        }

        return horizontal;
    }

    public bool PlayerWantsToJump()
    {
        bool playerJump = false;
        playerJump = Input.GetKeyDown(KeyCode.Space);
        return playerJump;
    }

    public bool PlayerJumped()
    {
        bool playerJump = false;
        playerJump = Input.GetKeyUp(KeyCode.Space);
        return playerJump;
    }

}
