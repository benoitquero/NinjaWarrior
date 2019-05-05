using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
	Text text;
	float time;
	bool play = true;

	public Transform player;
	public Transform destination;

    // Start is called before the first frame update
    void Start()
    {
		text = GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
		if (player.transform.position.x== destination.transform.position.x)
		{
			play = false;
		}

		if (play)
		{
			time += 100 * Time.deltaTime;
			string cent = Mathf.Floor(time % 100).ToString("00");
			string seconds = (Mathf.Floor(time / 100) % 60).ToString("00");
			string minutes = Mathf.Floor((time/100 % 3600)/60).ToString("00");
			text.text = minutes + ":" + seconds + ":" + cent;
		}

    }
}
