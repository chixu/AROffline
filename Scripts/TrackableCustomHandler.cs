using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;


public class TrackableCustomHandler : MonoBehaviour, ITrackableEventHandler {

	public string type;
	public string url;

	protected TrackableBehaviour mTrackableBehaviour;
	//public VideoPlayer player;
	// Use this for initialization
	protected virtual void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	/// <summary>
	/// Implementation of the ITrackableEventHandler function called when the
	/// tracking state changes.
	/// </summary>
	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
		else
		{
			OnTrackingLost();
		}
	}


	protected virtual void OnTrackingFound()
	{
		ScanSceneController.instant.OnTrackingFound (this);
	}


	protected virtual void OnTrackingLost()
	{
		ScanSceneController.instant.OnTrackingLost (this);
	}
}
