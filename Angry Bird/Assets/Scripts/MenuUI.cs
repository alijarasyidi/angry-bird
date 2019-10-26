using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {

	[SerializeField] private GameObject pausePanel;
	[SerializeField] private GameObject gameOverPanel;
	[SerializeField] private GameObject finishPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Pause ();
		}
		
	}

	void Pause ()
	{
		Time.timeScale = 0f;
		pausePanel.SetActive (true);
	}

	public void Resume ()
	{
		Time.timeScale = 1f;
		pausePanel.SetActive (false);
	}

	public void Restrart ()
	{
		SceneManager.LoadScene ("Main");
	}

	public void GameOver ()
	{
		gameOverPanel.SetActive (true);
	}

	public void Finish ()
	{
		finishPanel.SetActive (true);
	}

	public void Quit ()
	{
		Application.Quit ();
	}
}
