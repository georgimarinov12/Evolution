using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To do:
/// - make rotation of enemy sprite work when reaching the end of patrol area
/// - create attack function
/// </summary>


public class MovingDog : MonoBehaviour
{
	public GameObject spawner;
	public GameObject bloodEffect;
	public float speed;
	public int size;
	public int food;
	public float startWaitTime;  //start countdown till move to next spot


	public Transform[] moveSpots = new Transform[2];               //patrol spots
	private Rigidbody2D rb;
	//private Animator anim;
	private int randomSpot;                     //number of patrol spots 
	private float waitTime;                     //how long enemy stays at patrol spot for



	// Start is called before the first frame update
	void Start()
	{
		waitTime = startWaitTime; //make waittime equal to startwaittime
								  //anim = GetComponent<Animator>();
		randomSpot = Random.Range(0, moveSpots.Length); //choose a random first spot 
	}

	// Update is called once per frame
	void Update()
	{

		var difference = moveSpots[randomSpot].transform.position - transform.position;
		var isFacingRight = difference.x > 0 ? true : false;
		if (isFacingRight && transform.localScale.x < 0
		|| !isFacingRight && transform.localScale.x > 0)
		{
			FlipSprite();
		}

		transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime); //move toward first patrol area


		if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.5f) //asks if patrol point is further that .5f away from enemy
		{
			if (waitTime <= 0) //if waitTime less than or equal to 0
			{

				randomSpot = Random.Range(0, moveSpots.Length); //picks new patrol point
				waitTime = startWaitTime; //restarts countdown clock
			}
			else
			{
				waitTime -= Time.deltaTime; //counts down clock till next point
			}
		}

	}

	private void FlipSprite()
	{
		// invert the local X-axis scale
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}


	public void TakeDamage()
	{
		Destroy(gameObject);
		//Instantiate(bloodEffect[randomEnemy], transform.position, Quaternion.identity);
		spawner.GetComponent<RandomEnemySpawner>().Spawn(Random.Range(100, 1000));
		Debug.Log("Damage Taken");
	}
}
