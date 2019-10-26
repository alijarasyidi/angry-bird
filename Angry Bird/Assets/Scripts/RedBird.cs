using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBird : Bird {

	public float _boostForce = 50f;
	public bool _hasBoost = false;

	[SerializeField] private GameObject bouncerWalls;

	public void Boost ()
	{
		if (State == BirdState.Thrown && !_hasBoost)
		{
			bouncerWalls.SetActive (true);

			boostTrail.enabled = true;

			PhysicsMaterial2D bounce = new PhysicsMaterial2D("Bouncy2");
			bounce.bounciness = 1f;
			bounce.friction = 0f;
			gameObject.GetComponent<Collider2D> ().sharedMaterial = bounce;

			rigidBody.gravityScale = 0f;

			rigidBody.AddForce (rigidBody.velocity * _boostForce);
			_hasBoost = true;

			Invoke ("BounceOver", 3f);
		}
	}

	public override void OnTap ()
	{
		Boost ();
	}

	void BounceOver ()
	{
		boostTrail.enabled = false;

		PhysicsMaterial2D bounce = new PhysicsMaterial2D("Bouncy2");
		bounce.bounciness = 0.2f;
		bounce.friction = 0.4f;
		gameObject.GetComponent<Collider2D> ().sharedMaterial = bounce;

		rigidBody.gravityScale = 1f;

		bouncerWalls.SetActive (false);
	}
}
