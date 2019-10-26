using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Enemy : MonoBehaviour {

	public float health = 50f;
	public UnityAction<GameObject> OnEnemyDestroyed = delegate { };

	private bool _isHit = false;
	private Camera m_camera;

	void Awake ()
	{
		m_camera = Camera.main;
	}

	void OnDestroy ()
	{
		if (_isHit)
		{
			OnEnemyDestroyed (gameObject);
		}

		m_camera.DOShakePosition (0.2f, 0.5f, 5, 90f, true).OnComplete (RecoverCamera);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.GetComponent<Rigidbody2D> () == null) return;

		if (col.gameObject.tag == "Bird")
		{
			_isHit = true;
			Destroy (gameObject);
		}

		if (col.gameObject.tag == "Obstacle")
		{
			float damage = col.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude * 10;
			health -= damage;

			if (health <= 0)
			{
				_isHit = true;
				Destroy (gameObject);
			}
		}
	}

	void RecoverCamera ()
    {
        m_camera.transform.position = new Vector3 (0f, 0f, -15f);
    }
}
