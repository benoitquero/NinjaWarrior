using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winning : MonoBehaviour
{
	public Animator animator;
	public Transform player_position;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
			animator.SetBool("Win", player_position.position.x == this.transform.position.x && player_position.position.z == this.transform.position.z);
		
    }
}
