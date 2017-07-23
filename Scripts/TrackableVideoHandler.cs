//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Video;
//using Vuforia;
//
//
//public class TrackableVideoHandler : TrackableHandler {
//	public VideoPlayer player;
//
//	protected override void Start(){
//		base.Start ();
//		//ScanSce
//	}
//
//
//	protected override void OnTrackingFound()
//	{
//		SceneController.instant.videoSlider.gameObject.SetActive (true);
//		SceneController.instant.bottomText.gameObject.SetActive (false);
//		player.frame = 0;
//		player.Play ();
//	}
//
//
//	protected override void OnTrackingLost()
//	{
//		//"请勿忽视小小烟头带来的巨大危害!"
//		SceneController.instant.videoSlider.gameObject.SetActive (false);
//		SceneController.instant.bottomText.gameObject.SetActive (true);
//		SceneController.instant.videoSlider.value = 0;
//		player.frame = 0;
//		player.Pause ();
//	}
//}
