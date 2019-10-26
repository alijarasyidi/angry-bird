using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird {

	public float _boostForce = 100f;
	public bool _hasBoost = false;

	public void Boost ()
	{
		if (State == BirdState.Thrown && !_hasBoost)
		{
			boostTrail.enabled = true;
			
			rigidBody.AddForce (rigidBody.velocity * _boostForce);
			_hasBoost = true;
		}
	}

	public override void OnTap ()
	{
		Boost ();
	}
	
}