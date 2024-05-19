using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	[SerializeField]
	public GameObject pointA, pointB;

	[SerializeField]
	public GameObject playerObject;

	
	private Rigidbody2D rb;
	private Animator animator;
	private Transform thisPoint;
	public float velocity;
	public BoxCollider2D playercollider { get; set; }

	public bool isAttacking;

	public void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		thisPoint = pointA.transform;
		animator.SetBool("walk", true);
		
		playercollider = playerObject.GetComponent<BoxCollider2D>();
		isAttacking = false;
		//rb.velocity = new Vector2(velocity, 0);
	}

	public void Update()
	{
		if (!isAttacking)
		{
			if (thisPoint == pointA.transform)
			{

				changeSpeed(5f);
			}
			else
			{

				changeSpeed(-5f);
			}
		}

		if (Vector2.Distance(transform.position, thisPoint.position) < 0.5f && thisPoint == pointA.transform)
		{
			thisPoint = pointB.transform;
			//animator.SetBool("idle", true);

			//rb.velocity = new Vector2(0, 0);
			StartCoroutine(waiter(3));
			Flip();
			//animator.SetBool("idle", false);
		}
		else if (Vector2.Distance(transform.position, thisPoint.position) < 0.5f && thisPoint == pointB.transform)
		{
			thisPoint = pointA.transform;
			//animator.SetBool("idle", true);
			//rb.velocity = new Vector2(0, 0);
			StartCoroutine(waiter(3));
			Flip();
			//animator.SetBool("idle", false);
		}
	}

	private void changeSpeed(float velocity)
	{
		rb.velocity = new Vector2(velocity, 0);
	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
		Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
		Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
	}

	private void Flip()
	{
		transform.Rotate(0f, 180.0f, 0f);
	}

	IEnumerator waiter(int waittime)
	{
		yield return new WaitForSeconds(waittime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision == playercollider)
		{
			Debug.Log("enemy enter collision");
			isAttacking = true;
			animator.SetBool("walk", false);
			animator.SetBool("attack", true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision == playercollider)
		{
			isAttacking = false;
			animator.SetBool("attack", false);
			animator.SetBool("walk", true);
			if (thisPoint == pointA)
				changeSpeed(5f);
			else
				changeSpeed(-5f);
		}
		
	}
}
