using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public enum EnemyState_Movement
    {
        Stationary,
        Patrolling
    }
    public enum EnemyState_Patrol
    {
        Scanning,
        MovingToPatrolPoint
    }
    public EnemyState_Movement StateMovement;
    public EnemyState_Patrol StatePatrol;
    public List<GameObject> PatrolPoints;

    public float MoveSpeed;
    public Vector3 TargetPosition;
    public float PositionClosityThreshold;

    public int PatrolPoints_Index;
    public float Time_PatrolWaitTime_Total;
    public float Time_PatrolWaitTime_Current;
    public bool PlayerInView;

    private void Start()
    {
        SetMoveTarget(PatrolPoints[PatrolPoints_Index].transform.position);
        StatePatrol = EnemyState_Patrol.MovingToPatrolPoint;

    }

    // Update is called once per frame
    void Update () {
        switch(StateMovement)
        {
            case EnemyState_Movement.Stationary:
                break;
            case EnemyState_Movement.Patrolling:
                UpdateMovement();
                break;
        }

	}
    private void ScanArea()
    {
        Time_PatrolWaitTime_Current += Time.deltaTime;
        if (Time_PatrolWaitTime_Current >= Time_PatrolWaitTime_Total)
        {
            PatrolPoints_Index++;
            if (PatrolPoints_Index >= PatrolPoints.Count)
                PatrolPoints_Index = 0;

            SetMoveTarget(PatrolPoints[PatrolPoints_Index].transform.position);
            StatePatrol = EnemyState_Patrol.MovingToPatrolPoint;
            Time_PatrolWaitTime_Current = 0;

        }
    }

    private void SetMoveTarget(Vector3 newTargetPosition)
    {
        TargetPosition = newTargetPosition;
    }
        private void MoveToPatrolPoint()
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
            StatePatrol = EnemyState_Patrol.Scanning;

        }
    }
    private void UpdateMovement()
    {
        switch(StatePatrol)
        {
            case EnemyState_Patrol.Scanning:
                ScanArea();
                break;

            case EnemyState_Patrol.MovingToPatrolPoint:
                MoveToPatrolPoint();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(string.Format("{0} collided with {1}", this.name, other.gameObject.name));
        if(other.gameObject.name.ToLower() == "player")
            PlayerInView = true;
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(string.Format("{0} no longer colliding with {1}", this.name, other.gameObject.name));
        if (other.gameObject.name.ToLower() == "player")
            PlayerInView = false;
    }
}
