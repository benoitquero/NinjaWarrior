using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationSound : MonoBehaviour
{
    public Transform playerPosition;
    private AudioSource audio;
    private bool finished;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        finished = false;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dist = transform.position - playerPosition.position; 
        if(!finished && dist.sqrMagnitude < 2.0f) {
            finished = true;
            audio.Play();
        }
    }
}
