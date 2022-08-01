using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlace2 : MonoBehaviour
{

    public GameObject objectToPlace;
    public GameObject placementIndicator;

    private ARRaycastManager RaycastManager;
    private Pose PlacementPose;
    private bool PlacementPoseIsValid = false;
    private bool IsPlaced = false;

    static List<ARRaycastHit> Hits = new List<ARRaycastHit>();


    void Awake()
    {
        RaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        TryUpdatePlacementPose();
        UpdatePlacementIndicator();

        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        //use screenCenter for the ray
        /*if (PlacementPoseIsValid)
        {
            PlaceObject();
        }*/

        //if(placementPoseIsValid && Input.touchCount > 0 
        if (IsPlaced == false && PlacementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            IsPlaced = true;
            placementIndicator.SetActive(false);
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if (PlacementPoseIsValid && IsPlaced == false)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose(Vector3 hitPoint)
    {
        PlacementPose.position = hitPoint;
        var cameraForward = Camera.main.transform.forward;
        var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
        PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);
    }

    private void TryUpdatePlacementPose()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        RaycastManager.Raycast(screenCenter, Hits, TrackableType.Planes); 
        PlacementPoseIsValid = Hits.Count > 0;

        if (PlacementPoseIsValid)
        {
            UpdatePlacementPose(Hits[0].pose.position);
        }
    }

}
