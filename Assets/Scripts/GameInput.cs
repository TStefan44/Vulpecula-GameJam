using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
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

        horizontal = Input.GetAxisRaw("Horizontal");

        return horizontal;
    }

    public bool PlayerWantsToJump()
    {
        bool playerJump = false;
        playerJump = Input.GetButtonDown("Jump");
        return playerJump;
    }

    public bool PlayerJumped()
    {
        bool playerJump = false;
        playerJump = Input.GetButtonUp("Jump");
        return playerJump;
    }

}
