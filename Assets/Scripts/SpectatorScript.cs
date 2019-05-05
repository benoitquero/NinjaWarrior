using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorScript : MonoBehaviour
{
    //private Transform spectator;
    public float speed=5f;
    public float amplitude = 0.2f;
    private float generatedRandom;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        generatedRandom = Random.Range(1f,100f);
        startPosition = transform.position;
        //spectator = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 newPos = new Vector3(transform.position.x, startPosition.y + amplitude * Mathf.Cos(speed*Time.time + generatedRandom), transform.position.z);

        transform.position = newPos;

    }
}
