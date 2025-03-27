using UnityEngine;

public class DotBullet : MonoBehaviour
{

	void Update()
	{

		transform.Translate(Vector2.left * speed * Time.deltaTime);
	}

	private void OnBecameInvisible()
	{
		Destroy(gameObject);
	}

	[SerializeField] private float speed = 5f;
	
}
