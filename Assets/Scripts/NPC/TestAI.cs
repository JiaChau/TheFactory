using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.AI;


public class TestAI : MonoBehaviour
{

 public Transform[] waypoints;

 private NavMeshAgent priestAgent;

 private float normalSpeed = 1f;

 private int waypointIndex;

 private Vector3 target;




    // Update is called once per frame
void Start()
{
    priestAgent = GetComponent<NavMeshAgent>();
    UpdateDestination();
}

void Update()

{



            //We want to continue patrol

Patrol();


}



    //This will set the destination of the navmesh agent to current waypoint index

void UpdateDestination()

{

target = waypoints[waypointIndex].position;

priestAgent.SetDestination(target);



}


    //This increases the index, taking into account the size of the array and setting it to "0" if its equal to it

void IterateWaypointIndex()
{

++waypointIndex;

if (waypointIndex == waypoints.Length)
{
    waypointIndex = 0;
    Debug.Log("Test2");
}

Debug.Log(waypointIndex);
}



    //Stops scary music, resets slower speed, returns to slow walking animation

private void Patrol()

{

priestAgent.speed = normalSpeed;

        //Also, handles the waypoints of the arrays/patrol points

if (Vector3.Distance(transform.position, target) < 5)

{

IterateWaypointIndex();

UpdateDestination();

}

}

}