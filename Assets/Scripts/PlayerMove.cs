using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerMove : MonoBehaviour
{


	public Transform destination;

	NavMeshAgent agent;

	public ThirdPersonCharacter character;


	// Start is called before the first frame update
	void Start()
    {
		agent = this.GetComponent<NavMeshAgent>();
	

		if (agent == null)
		{
			Debug.LogError("No nav mesh attached to " + gameObject.name);
		}
		else
			SetDestination();

		agent.updateRotation = false;
		
    }

	private void SetDestination()
	{
		if(destination != null)
		{
			Vector3 targetVector = destination.transform.position;
			agent.SetDestination(targetVector);
		}
		
	}

	private void Update()
	{
		if (agent.remainingDistance > agent.stoppingDistance)
		{
			character.Move(agent.desiredVelocity, false, false);
		}
		else
		{
			character.Move(Vector3.zero, false, false);
		}
	}


}
