using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public enum PlayerState_Movement
    {
        Stationary,
        Moving
    }

    public LayerMask ClickLayer;
    public float MoveSpeed;
    public Vector3 TargetPosition;
    public PlayerState_Movement StateMovement;
    public float PositionClosityThreshold;


	// Use this for initialization
	void Start () {
        StateMovement = PlayerState_Movement.Stationary;

	}
		// Update is called once per frame
	void Update (){
        if (Input.GetMouseButtonDown(0))
            HandleLeftClick();

        Update_Movement();
    }


    private void HandleLeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, ClickLayer.value))
            if (hitInfo.collider.gameObject.name.ToLower() == "ground")
                SetMoveTarget(hitInfo.point);
    }

    private void SetMoveTarget(Vector3 point)
    {
        TargetPosition = point;
        StateMovement = PlayerState_Movement.Moving;
    }

    private void Update_Movement()
    {
        switch (StateMovement)
        {
            case PlayerState_Movement.Stationary:
                //Basically stand there and chill
                break;
            case PlayerState_Movement.Moving:
                MoveToTarget();

                break;

        }
    }

    private void MoveToTarget()
    {
        // set facing direction
        Vector3 lookAtPosition = new Vector3(TargetPosition.x,
                                           transform.position.y,
                                           TargetPosition.z);
        transform.LookAt(lookAtPosition);

        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, MoveSpeed * Time.deltaTime);

        // close enough to taret?
        if ((transform.position - TargetPosition).magnitude < PositionClosityThreshold)
        {
            transform.position = TargetPosition;
            StateMovement = PlayerState_Movement.Stationary;
        }
    }
}

    

