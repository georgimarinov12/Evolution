﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
	public GameObject point;
	public GameObject enemy;
	public GameObject[] prefabs;
	public Vector2 max;
	public Vector2 min;

	void Start()
	{
		for (int i = 0; i < 100; i++)
		{
			Spawn(i);
		}
	}


	public void Spawn(int num)
	{
		Vector2 pos1;
		pos1.x = Random.Range(min.x, max.x);
		pos1.y = Random.Range(min.y, max.y);
		GameObject point1 = Instantiate(point, pos1, Quaternion.identity);
		point1.name = "E" + num + "PS1";

		Vector2 pos2;
		pos2.x = Random.Range(min.x, max.x);
		pos2.y = Random.Range(min.y, max.y);
		GameObject point2 = Instantiate(point, pos2, Quaternion.identity);
		point2.name = "E" + num + "PS2";

		int i = Random.Range(0, prefabs.Length);

		if (pos1.x <= pos2.x)
		{
			var angle = Mathf.Atan2(pos2.y, pos2.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			GameObject en = Instantiate(prefabs[i], pos1, Quaternion.AngleAxis(angle, Vector3.forward));
			en.GetComponent<MovingDog>().moveSpots[0] = point1.transform;
			en.GetComponent<MovingDog>().moveSpots[1] = point2.transform;
		}
		
		else
		{
			Vector3 dir = (pos2 - pos1).normalized;
			if (dir.x < 0)
				dir = -dir;
			var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			GameObject en = Instantiate(prefabs[i], pos2, Quaternion.AngleAxis(angle, Vector3.forward));
			en.GetComponent<MovingDog>().moveSpots[0] = point2.transform;
			en.GetComponent<MovingDog>().moveSpots[1] = point1.transform;
		}

		Debug.Log("Spawned with " + num);
	}
}

