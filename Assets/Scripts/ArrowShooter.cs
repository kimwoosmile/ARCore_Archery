using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour {

    public float speed = 20;

    GameObject arrowPrefab;
    

	// Use this for initialization
	void Start () {
        arrowPrefab = Resources.Load("Prefabs/Arrow") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            GameObject newArrow = Instantiate(arrowPrefab) as GameObject;

            newArrow.transform.position = Camera.main.transform.position;// + Camera.main.transform.forward * 2;
            Rigidbody rb = newArrow.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * speed;

        }
	}
}
