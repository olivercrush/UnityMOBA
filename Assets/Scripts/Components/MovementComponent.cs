using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementComponent : BaseComponent
{
    // PRIVATE
    private NavMeshAgent agent;
    private GameObject[] waypoints;
    private int currentWaypointId = 0;

    public void InitializeMovementComponent(NavMeshAgent agent, Transform transform, GameObject[] waypoints)
    {
        this.agent = agent;
        this.agent.Warp(transform.position);
        this.waypoints = waypoints;
    }

    public void GoToDestination(Vector3 destinationPos)
    {
        agent.isStopped = false;
        agent.SetDestination(destinationPos);
    }

    public void GoToTarget(GameObject target)
    {
        GoToDestination(target.transform.position);
    }

    public void GoToWaypoint()
    {
        if (currentWaypointId >= waypoints.Length)
        {
            NotificationFactory.Instance.CreateNotificationEndOfWaypoints(controller.gameObject, controller.gameObject);
        }
        else
        {
            Vector3 destinationPos = waypoints[currentWaypointId].transform.position;
            if (agent.destination != destinationPos)
            {
                agent.isStopped = false;
                agent.SetDestination(destinationPos);
            }
        }
    }

    public void StopMovement()
    {
        agent.SetDestination(transform.position);
        agent.isStopped = true;
        agent.enabled = false;
        agent.enabled = true;
    }

    private void Update()
    {
        UnitController unitController = (UnitController) controller;
        if (unitController.state == UnitState.LANING && currentWaypointId < waypoints.Length)
        {
            bool withinDetectionRange = GeometryMethods.IsWithinRange(waypoints[currentWaypointId].transform, transform, 2);
            if (withinDetectionRange)
            {
                currentWaypointId++;
                GoToWaypoint();
            }
        }
    }

}
