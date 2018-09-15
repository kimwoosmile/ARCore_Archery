using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidCameraTilt : MonoBehaviour {

    Quaternion cameraBase = Quaternion.Euler(new Vector3(3f, 180f, 0f));
    Quaternion referenceRotation = Quaternion.identity;
    const float lowPassFilterFactor = 0.2f;

    
	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            cameraBase * (ConvertRotation(referenceRotation * Input.gyro.attitude) * GetRotFix()), 
            lowPassFilterFactor);
    }

    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
    private Quaternion GetRotFix()
    {
        if (Screen.orientation == ScreenOrientation.Portrait) return Quaternion.identity;
        if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.Landscape) return Quaternion.Euler(0, 0, -90);
        if (Screen.orientation == ScreenOrientation.LandscapeRight) return Quaternion.Euler(0, 0, 90);
        if (Screen.orientation == ScreenOrientation.PortraitUpsideDown) return Quaternion.Euler(0, 0, 180);
        return Quaternion.identity;
    }
}
