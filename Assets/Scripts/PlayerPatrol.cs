using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerPatrol : MonoBehaviour
{
	public bool _patrolWaiting;
	public float _totalWaitTime = 3f;
	public float _switchProbability = 0.2f;
	public List<Waypoint> _patrolPoints;

	NavMeshAgent _navMeshAgent;
	int _currentPatrolIndex;
	bool _travelling;
	bool _waiting;
	bool _patrolForward;
	float _waitTimer;


    // Start is called before the first frame update
    void Start()
    {
		_navMeshAgent = this.GetComponent<NavMeshAgent>();

		if (_navMeshAgent==null)
		{
			Debug.LogError("no mesh agent");
		}
		else
		{
			if (_patrolPoints != null && _patrolPoints.Count >= 2)
			{
				_currentPatrolIndex = 0;
				SetDestination();
			}
			else
			{
				Debug.Log("Insufficient patrol points for basic patrolling behaviour");
			}
		}
    }


	// Update is called once per frame
	void Update()
    {
		//check if we're close to the destination
		if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
		{
			_travelling = false;

			//if we're going to wait, then wait
			if (_patrolWaiting)
			{
				_waiting = true;
				_waitTimer = 0f;
			}
			else
			{
				ChangePatrolPoint();
				SetDestination();
			}
		}

		//Instead if we're waiting
		if (_waiting)
		{
			_waitTimer += Time.deltaTime;
			if (_waitTimer >= _totalWaitTime)
			{
				_waiting = false;

				ChangePatrolPoint();
				SetDestination();
			}
		}
    }

	private void SetDestination()
	{
		if (_patrolPoints != null)
		{
			Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
			_navMeshAgent.SetDestination(targetVector);
			_travelling = true;
		}
	}


	private void ChangePatrolPoint()
	{
		if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
		{
			_patrolForward = !_patrolForward;
		}
		if (_patrolForward)
		{
			_currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
		}
		else
		{
			if (--_currentPatrolIndex < 0)
			{
				_currentPatrolIndex = _patrolPoints.Count - 1;
			}
		}
	}
}
