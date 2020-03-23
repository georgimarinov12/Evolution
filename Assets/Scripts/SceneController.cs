using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKey("r"))
		{
			StartMenu();
		}
	}

	public void StartMenu()
	{
		SceneManager.LoadScene("SampleScene");
	}
	public void Exit()
	{
		Application.Quit();
	}
}
