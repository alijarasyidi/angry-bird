using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public SlingShooter slingShooter;
	public TrailController trailController;
	public List<Bird> birds;
	public List<Enemy> enemies;
	public BoxCollider2D tapCollider;
	public GameObject explosion;

	private bool _isGameEnded = false;
	private Bird _shotBird;

	[SerializeField] private MenuUI menu;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < birds.Count; i++)
		{
			birds[i].OnBirdDestroyed += ChangeBird;
			birds[i].OnBirdShot += AssignTrail;
		}

		for (int i = 0; i < enemies.Count; i++)
		{
			enemies[i].OnEnemyDestroyed += CheckGameEnd;
		}

		tapCollider.enabled = false;
		slingShooter.InitiateBird(birds[0]);
		_shotBird = birds[0];
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void ChangeBird ()
	{
		tapCollider.enabled = false;

		if (_isGameEnded)
		{
			return;
		}

		birds.RemoveAt (0);

		if (birds.Count > 0)
		{
			slingShooter.InitiateBird(birds[0]);
			_shotBird = birds [0];
		} else
		{
			menu.GameOver ();
		}
	}

	public void CheckGameEnd (GameObject destroyedEnemy)
	{
		for (int i = 0; i < enemies.Count; i++)
		{
			if (enemies[i].gameObject == destroyedEnemy)
			{
				GameObject newExplosion = Instantiate (explosion, destroyedEnemy.transform.position + new Vector3 (0f, 0f, -3f), destroyedEnemy.transform.rotation);
				Destroy (newExplosion, 1f);
				enemies.RemoveAt (i);
				break;
			}
		}

		if (enemies.Count == 0)
		{
			_isGameEnded = true;
			menu.Finish ();
		}
	}

	public void AssignTrail (Bird bird)
	{
		trailController.SetBird (bird);
		StartCoroutine (trailController.SpawnTrail());
		tapCollider.enabled = true;
	}

	void OnMouseUp ()
	{
		if (_shotBird != null)
		{
			_shotBird.OnTap ();
		}
	}
}
