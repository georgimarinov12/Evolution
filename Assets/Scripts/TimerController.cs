using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
	public float timer;
	public Text timeText;
	public Text sumText;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			gameObject.GetComponent<playerController>().Die();
		}
		OnGUI();
	}

	void OnGUI()
	{
		float minutes = Mathf.Floor(timer / 60);
		float seconds = Mathf.RoundToInt(timer % 60);

		string min = minutes.ToString();
		string sec = seconds.ToString();

		if (minutes < 10)
		{
			min = "0" + minutes.ToString();
		}
		if (seconds < 10)
		{
			sec = "0" + Mathf.RoundToInt(seconds).ToString();
		}
		timeText.text = min + ":" + sec;
	}
	public void SumWith(int num)
	{
		sumText.text = "+" + num.ToString();
		sumText.gameObject.SetActive(true);
		Invoke("SetFalse", 2);
	}
	void SetFalse()
	{
		sumText.gameObject.SetActive(false);
	}
}