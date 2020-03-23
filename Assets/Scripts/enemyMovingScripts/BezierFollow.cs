using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
	[SerializeField]
	private Transform[] routes;

	private int routeToGo;

	private float tParam;

	private Vector2 dogPosition;

	private float speedModifier;

	private bool coroutineAlowed;

	void Start()
	{
		routeToGo = 0;
		tParam = 0f;
		speedModifier = RandomNumber(0.1f, 0.3f);
		coroutineAlowed = true;
	}

	void Update()
	{
		if (coroutineAlowed)
		{			
			StartCoroutine(GoByTheRoute(routeToGo));
		}
	}

	private IEnumerator GoByTheRoute(int routeNumber)
	{
		coroutineAlowed = false;

		Vector2 p0 = routes[routeNumber].GetChild(0).position;
		Vector2 p1 = routes[routeNumber].GetChild(1).position;
		Vector2 p2 = routes[routeNumber].GetChild(2).position;
		Vector2 p3 = routes[routeNumber].GetChild(3).position;


		while(tParam < 1)
		{
			tParam += Time.deltaTime * speedModifier;

			dogPosition = Mathf.Pow(1 - tParam, 3) * p0 +
			3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
			3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
			Mathf.Pow(tParam, 3) * p3;

			transform.position = dogPosition;
			
			yield return new WaitForEndOfFrame();
		}

		tParam = 0f;

		routeToGo += 1;

		if(routeToGo > routes.Length - 1)
		{
			routeToGo = 0;
		}

		coroutineAlowed = true;
	}
	public float RandomNumber(float min, float max)
	{
		return Random.Range(min, max);
	}
}
