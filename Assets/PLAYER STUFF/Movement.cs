using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //The camera is inside the player
    public Camera Eyes;
    public Rigidbody RB;
    
    //Player stats
    public float MouseSensitivity = 3;
    public float WalkSpeed = 10;
    public float SprintMultiplier = 2f;  // hold shift to multiply speed by this
    public float OrbSpeedBonus = 0.5f;   // how much faster player gets per orb
    private int lastScore = 0;           // tracks when score changes

    
    public List<GameObject> Floors;

    void Start()
    {
        //Turn off my mouse and lock it to center screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //If my mouse goes left/right my body moves left/right
        float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.Rotate(0, xRot, 0);
        //If my mouse goes up/down my aim (but not body) go up/down
        float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;
        Eyes.transform.Rotate(yRot, 0, 0);

        // Check if score changed and boost player speed
        if (Enemy.Score != lastScore)
        {
            lastScore = Enemy.Score;
            WalkSpeed += OrbSpeedBonus;
        }

        //Movement code
        if (WalkSpeed > 0)
        {
            //My temp velocity variable
            Vector3 move = Vector3.zero;
            //transform.forward/right are relative to the direction my body is facing
            if (Input.GetKey(KeyCode.W))
                move += transform.forward;
            if (Input.GetKey(KeyCode.S))
                move -= transform.forward;
            if (Input.GetKey(KeyCode.A))
                move -= transform.right;
            if (Input.GetKey(KeyCode.D))
                move += transform.right;

            // Hold shift to sprint
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? WalkSpeed * SprintMultiplier : WalkSpeed;

            
            move = move.normalized * currentSpeed;
            move.y = RB.linearVelocity.y;
            //adding the line above makes it so the player falls normally, not very slowly
            RB.linearVelocity = move;
        }
    }

    public bool OnGround()
    {
        return Floors.Count > 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (!Floors.Contains(other.gameObject))
            Floors.Add(other.gameObject);
    }

    private void OnCollisionExit(Collision other)
    {
        
        Floors.Remove(other.gameObject);
    }
}
