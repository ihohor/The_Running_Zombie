using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(VideoPlayer))]
public class Intro : MonoBehaviour
{
	private VideoPlayer _videoPlayer;
	
	private bool _isFinished;
	
	private void Awake()
	{
		_videoPlayer = GetComponent<VideoPlayer>();
	}

	private void Update()
	{
		if(!_videoPlayer.isPlaying && !_isFinished && _videoPlayer.time > 0)
		{
			_isFinished = true;
			print("Finish");
            SceneManager.LoadScene("MainMenu");
        }
	}
}