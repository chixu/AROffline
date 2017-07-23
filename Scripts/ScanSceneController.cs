using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanSceneController : MonoBehaviour {

	public static ScanSceneController instant;
	//public Slider videoSlider;
	public VideoController videoController;
	public Text bottomText;
	public string remoteUrl;

	// Use this for initialization
	void Awake () {
		instant = this;
	}

	public void OnTrackingFound(TrackableCustomHandler objHandler)
	{
		if (objHandler.type == "video") {
			videoController.Play (objHandler.gameObject.transform.GetChild(0).gameObject, GetUrl(objHandler.url));
			bottomText.gameObject.SetActive (false);
		}
	}


	public void OnTrackingLost(TrackableCustomHandler objHandler)
	{
		if (objHandler.type == "video") {
			videoController.Stop ();
			bottomText.gameObject.SetActive (true);
		}
	}


	public string GetUrl(string url){
		if (string.IsNullOrEmpty (remoteUrl)) {
			return Application.streamingAssetsPath + "/" + url;
		}
		return "";
	}
}
