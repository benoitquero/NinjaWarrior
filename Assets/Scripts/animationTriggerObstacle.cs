using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationTriggerObstacle : MonoBehaviour
{
	public Animator _animator;
	public Rigidbody rb;

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Entre");
		_animator.SetBool("isObs", true);
		//rb.AddForce(Vector3.up*5);
	}
	void OnTriggerExit(Collider other)
	{
		Debug.Log("Quitte");
		//_animator.SetBool("isJumping", true);
		_animator.SetBool("isObs", false);
	}
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
