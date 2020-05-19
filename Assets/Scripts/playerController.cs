using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
	public float speed;
	public int size;
	public int food;
	public GameObject deadFish;
	public int[] neededFood;
	private Animator animator;
	public Text sizeText;
	public Text nowFoodText;
	public Text maxFoodText;


	// Use this for initialization
	void Start()
	{
		animator = this.GetComponent<Animator>();
	}

	public void LoadGameOverScene()
	{
		Debug.Log("Game Over called");
		SceneManager.LoadScene("Game Over");
	}

	public void LoadWinScene()
	{
		Debug.Log("Win called");
		SceneManager.LoadScene("WinScene");
	}

	public void Die()
	{
		gameObject.SetActive(false);
		Instantiate(deadFish, transform.position, Quaternion.identity);
		Invoke("LoadGameOverScene", 3);
	}

	// Update is called once per frame
	void Update()
	{
		if (size <= 0)
		{
			Die();
		}
		sizeText.text = size.ToString();
		nowFoodText.text = food.ToString();
		maxFoodText.text = "/ " + neededFood[size-1].ToString();
		Move();
		if (food >= neededFood[size-1])
		{
			Debug.Log("Size = " + size);
			size++;
			speed++;
			animator.SetInteger("Size", (animator.GetInteger("Size") + 1));
		}

		if (size >= neededFood.Length + 1)
		{
			Invoke("LoadWinScene", 2);
		}

	}


	private IEnumerator Pause(int p)
	{
		Time.timeScale = 0.1f;
		yield return new WaitForSeconds(p);
		Time.timeScale = 1;
	}

	void Move()
	{
		Vector2 moveDirection = Vector2.zero;
		var vertical = Input.GetAxis("Vertical");
		var horizontal = Input.GetAxis("Horizontal");
		//Up
		if (horizontal == 0 && vertical > 0)
		{
			moveDirection.y = 1;
			animator.SetInteger("Direction", 0);
		}
		//Up Right
		if (horizontal > 0 && vertical > 0)
		{
			moveDirection.x = 1;
			moveDirection.y = 1;
			animator.SetInteger("Direction", 1);
		}
		//Right
		if (horizontal > 0 && vertical == 0)
		{
			moveDirection.x = 1;
			animator.SetInteger("Direction", 2);
		}
		//Down Right
		if (horizontal > 0 && vertical < 0)
		{
			moveDirection.x = 1;
			moveDirection.y = -1;
			animator.SetInteger("Direction", 3);
		}
		//Down
		if (horizontal == 0 && vertical < 0)
		{
			moveDirection.y = -1;
			animator.SetInteger("Direction", 4);
		}
		//Down Left
		if (horizontal < 0 && vertical < 0)
		{
			moveDirection.x = -1;
			moveDirection.y = -1;
			animator.SetInteger("Direction", 5);
		}
		//Left
		if (horizontal < 0 && vertical == 0)
		{
			moveDirection.x = -1;
			animator.SetInteger("Direction", 6);
		}
		//Up Left
		if (horizontal < 0 && vertical > 0)
		{
			moveDirection.x = -1;
			moveDirection.y = 1;
			animator.SetInteger("Direction", 7);
		}

		transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Hit");
		if(collision.gameObject.GetComponent<MovingDog>().size > size)
		{
			size--;
			speed--;
			food = neededFood[size-1] - 1;
			animator.SetInteger("Size", (animator.GetInteger("Size") - 1));
			
			Debug.Log("Lower Size");
		}
		else
		{
			food += collision.gameObject.GetComponent<MovingDog>().food;
			Debug.Log("Food = " + food);
			gameObject.GetComponent<TimerController>().timer += 10;
			gameObject.GetComponent<TimerController>().SumWith(10);

			collision.gameObject.GetComponent<MovingDog>().TakeDamage();
			
		}
	}

}
