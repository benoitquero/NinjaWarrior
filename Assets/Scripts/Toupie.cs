﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toupie : MonoBehaviour
{
	public Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		_transform.Rotate(0, 20*Time.deltaTime, 0);
    }
}