using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class EnvironmentManager : MonoBehaviour {

    public Camera FirstPersonCamera;

    public GameObject DetectedPlanePrefab;

    public GameObject AndyPlanePrefab;
    
    public GameObject AndyPointPrefab;

    private const float k_ModelRotation = 180.0f;

    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();

    private bool m_IsQuitting = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    Session.GetTrackables<DetectedPlane>(m_AllPlanes);
        
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began) return;

        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon | TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)) {
            if ((hit.Trackable is DetectedPlane) && Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0) {
                Debug.Log("Hit at back of the current DetectedPlane");
            } else {
                GameObject prefab;
                if (hit.Trackable is FeaturePoint) {
                    prefab = AndyPointPrefab;
                } else {
                    prefab = AndyPlanePrefab;
                }

                var andyObject = Instantiate(prefab, hit.Pose.position, hit.Pose.rotation);

                andyObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                andyObject.transform.parent = anchor.transform;
            }
        }
	}
}
