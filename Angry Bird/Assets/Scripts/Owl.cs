using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Enemy {
	
	[SerializeField] private Transform[] patrolPoint;
	[SerializeField] private int currentPatrolIndex;
	[SerializeField] private int prevPatrolIndex;
	[SerializeField] private bool isDelay = true;
	[SerializeField] private float delayTime = 2f;
	[SerializeField] private float speed = 5f;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (Delay());
		currentPatrolIndex = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x < patrolPoint[currentPatrolIndex].position.x && !isDelay)
		{
			transform.position = Vector3.Lerp (transform.position, patrolPoint[currentPatrolIndex].position, speed * Time.deltaTime);
		}

		if (transform.position.x > patrolPoint[currentPatrolIndex].position.x && !isDelay)
		{
			transform.position = Vector3.Lerp (transform.position, patrolPoint[currentPatrolIndex].position, speed * Time.deltaTime);
		}

		if (Vector3.Distance (transform.position, patrolPoint[currentPatrolIndex].position) < .1f && !isDelay)
		{
			isDelay = true;

			if (currentPatrolIndex == 1)
			{
				if (prevPatrolIndex == 0)
				{
					currentPatrolIndex = 2;
				} else // prevPatrolIndexnya = 2
				{
					currentPatrolIndex = 0;
				}
			} else // Selain dari index 1 pasti menuju 1
			{
				prevPatrolIndex = currentPatrolIndex;
				currentPatrolIndex = 1;
			}
		}
	}

	private IEnumerator Delay ()
	{
		while (true)
		{
			if (isDelay)
			{
				yield return new WaitForSeconds(delayTime);
				isDelay = false;
			}

			yield return null;

		}
	}
}
