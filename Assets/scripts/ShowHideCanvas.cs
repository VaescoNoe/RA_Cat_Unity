using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ShowHideCanvas : MonoBehaviour ,ITrackableEventHandler{

    Canvas targetCanvas;
    TrackableBehaviour tb;

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
      if(newStatus == TrackableBehaviour.Status.DETECTED || newStatus == TrackableBehaviour.Status.TRACKED || newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            targetCanvas.enabled = true;
        }
        else
        {
            targetCanvas.enabled = false;
        }
    }


    // Use this for initialization
    void Start () {
        targetCanvas = GetComponentInChildren<Canvas>();
        tb = GetComponent<TrackableBehaviour>();
        if (tb)
            tb.RegisterTrackableEventHandler(this);
	}
	
	
}
