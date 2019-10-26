using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour {

	public enum BirdState { Idle, Thrown, HitSomething }
	public GameObject parent;
	public Rigidbody2D rigidBody;
	public CircleCollider2D collider;
	public UnityAction OnBirdDestroyed = delegate { };
	public UnityAction<Bird> OnBirdShot = delegate { };
	public BirdState State { get { return _state; } }
	public TrailRenderer boostTrail;

	[SerializeField] private BirdState _state;

	private float _minVelocity = 0.05f;
	private bool _flagDestroy;

	// Use this for initialization
	void Start ()
	{
		rigidBody.bodyType = RigidbodyType2D.Kinematic;
		collider.enabled = false;
		_state = BirdState.Idle;
	}

	void FixedUpdate ()
	{
		if (_state == BirdState.Idle && rigidBody.velocity.sqrMagnitude >= _minVelocity)
		{
			_state = BirdState.Thrown;
		}

		if ((_state == BirdState.Thrown || _state == BirdState.HitSomething) && rigidBody.velocity.sqrMagnitude < _minVelocity && !_flagDestroy)
		{
			_flagDestroy = true;
			StartCoroutine(DestroyAfter(2));
		}
	}

	private IEnumerator DestroyAfter(float second)
	{
		yield return new WaitForSeconds(second);
		Destroy(gameObject);
	}

	public void MoveTo(Vector2 target, GameObject parent)
	{
		gameObject.transform.SetParent(parent.transform);
		gameObject.transform.position = target;
	}

	public void Shoot(Vector2 velocity, float distance, float speed)
	{
		collider.enabled = true;
		rigidBody.bodyType = RigidbodyType2D.Dynamic;
		rigidBody.velocity = velocity * distance * speed;
		OnBirdShot (this);
	}

	void OnDestroy ()
	{
		if ((_state == BirdState.Thrown || _state == BirdState.HitSomething) || _state == BirdState.HitSomething)
		{
			OnBirdDestroyed ();
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		_state = BirdState.HitSomething;
	}

	public virtual void OnTap ()
	{

	}
}
