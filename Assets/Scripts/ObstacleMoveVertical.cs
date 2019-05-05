using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoveVertical : MonoBehaviour
{
	public Transform _transform;

	int sens = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		_transform.Translate(0, sens * 2 * Time.deltaTime, 0);
		if (_transform.position.y>11)
		{
			sens = -1; 
		}
		if (_transform.position.y < 6)
		{
			sens = 1;
		}

    }
}
