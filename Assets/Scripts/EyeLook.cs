using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLook : MonoBehaviour {

    public float speedH = 10.0f;
    public float speedV = 10.0f;

    float yaw = 0.0f;
    float pitch = 0.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch += speedV * -Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

	}
}
