using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;
//-----------------------------------------------------------------------------
// Copyright 2015-2016 RenderHeads Ltd.  All rights reserverd.
//-----------------------------------------------------------------------------

public class VideoController : MonoBehaviour
{
	//public MediaPlayer	_mediaPlayer;
	public Slider videoSlider;
	//private bool _wasPlayingOnScrub;
	//private float _setVideoSeekSliderValue;
	public static VideoController instant;
	private string prevPath;
    public VideoPlayer videoPlayer;
	public AudioSource audioSource;
	private bool isDown = false;
	//private bool isClick = false;
	//private int _currentFrame;
	//private bool frameChanged = false;
	private long _prevFrame;
	private long _nextFrame = -1;
	public int minVideoFrameUnit = 10;
	//private long _prevFrame;

    //		public void OnMuteChange()
    //		{
    //			if (_mediaPlayer)
    //			{
    //				_mediaPlayer.Control.MuteAudio(_MuteToggle.isOn);
    //			}
    //		}

	public void Play(GameObject obj, string path)
    {
		isDown = false;
		obj.SetActive (false);
        //videoPlayer = obj.GetComponent<VideoPlayer>();
		StartCoroutine(LoadAndPlay(obj, path));
    }


//	public void BindToGameobject(GameObject obj){
//		if (obj != null) {
//			MeshRenderer renderer = obj.GetComponent<MeshRenderer> ();
//			if (renderer != null) {
//				videoPlayer.targetMaterialRenderer = renderer;
//			}
//		}
//	}

	public void Stop(){
		videoSlider.gameObject.SetActive (false);
		//bottomText.gameObject.SetActive (true);
		videoSlider.value = 0;
		videoPlayer.frame = 0;
		videoPlayer.Pause ();
	}

	private IEnumerator LoadAndPlay(GameObject obj, string path){
		videoSlider.value = 0;
		if (path != prevPath)
		{
			//obj.AddComponent<VideoPlayer>();
			//videoPlayer = obj.GetComponent<VideoPlayer>();
			//videoPlayer.playOnAwake = false;
			//audioSource.playOnAwake = false;
			//audioSource.Pause();
			videoPlayer.source = VideoSource.Url;
			videoPlayer.url = path;

			//videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
			//videoPlayer.controlledAudioTrackCount = 1;
			//videoPlayer.EnableAudioTrack (0, true);
			//videoPlayer.SetTargetAudioSource (0, audioSource);
			//obj.AddComponent<AudioSource>();
			//AudioSource audio = obj.GetComponent<AudioSource>();
			//videoPlayer.SetTargetAudioSource (0, audio);
			videoPlayer.Prepare();
			while (!videoPlayer.isPrepared)
			{
				//Logger.Log("Preparing Video " + path);
				yield return null;
			}
			videoPlayer.targetMaterialRenderer = obj.GetComponent<MeshRenderer>();
			audioSource.Play ();
			videoPlayer.Play ();
		}
		else
		{
			VideoController.instant.videoPlayer.targetMaterialRenderer = obj.GetComponent<MeshRenderer>();
			videoPlayer.Play();
		}
		obj.gameObject.SetActive (true);
		videoSlider.gameObject.SetActive(true);
		prevPath = path;
	}


	public void OnVideoSeekSlider ()
	{
        //if (_mediaPlayer && _videoSeekSlider && _videoSeekSlider.value != _setVideoSeekSliderValue) {
        //	_mediaPlayer.Control.Seek (_videoSeekSlider.value * _mediaPlayer.Info.GetDurationMs ());
        //}
		//if (!isDown)
		//	return;
			//isClick = true;
		int frame = Mathf.FloorToInt(videoSlider.value * videoPlayer.frameCount);
		if (Mathf.Abs (videoPlayer.frame - frame) > minVideoFrameUnit * 2) {
			Logger.Log ("OnVideoSeekSlider" + frame.ToString());
			videoPlayer.frame = frame;
			_nextFrame = frame;
		}
		//Logger.Log("slide " + _videoSeekSlider.value.ToString(),"blue");
	}

	public void OnVideoSliderDown ()
	{
		//videoPlayer.Pause();
		isDown = true;
		//Logger.Log("down","blue");
	}

	public void OnVideoSliderUp ()
	{
		//videoPlayer.Play();
		isDown = false;
	}

	void Awake ()
	{
		VideoController.instant = this;
		//if (_mediaPlayer) {
		//	_mediaPlayer.Events.AddListener (OnVideoEvent);
		//}
	}

	void Update ()
	{
        //if (_mediaPlayer && _mediaPlayer.Info != null && _mediaPlayer.Info.GetDurationMs () > 0f) {
        //	float time = _mediaPlayer.Control.GetCurrentTimeMs ();
        //	float d = time / _mediaPlayer.Info.GetDurationMs ();
        //	_setVideoSeekSliderValue = d;
        //	_videoSeekSlider.value = d;
        //}
		if (!videoSlider.gameObject.activeSelf)
			return;

		//Debug.Log("update");


		if (videoPlayer && !isDown) {
			float value = (float)videoPlayer.frame / videoPlayer.frameCount;
			if (Mathf.Abs(videoPlayer.frame - _prevFrame) <= minVideoFrameUnit){
				Logger.Log ("Update " + videoPlayer.frame);
				//if (!frameChanged)

				
				if (_nextFrame != -1 && videoPlayer.frame > _nextFrame && videoPlayer.frame < _nextFrame + 2 * minVideoFrameUnit)
					_nextFrame = -1;
				else
					videoSlider.value = value;
			}
		}
		Logger.Log ("Update ++++++ " + videoPlayer.frame.ToString() + " " +  _prevFrame + " " + videoSlider.value);
		_prevFrame = videoPlayer.frame;
    }

}
