using UnityEngine;

public class Player : MonoBehaviour
{
	public Joystick joystick;

	public float speed;

	private void Update()
	{
		Vector2 deltaPos = joystick.InputVector * speed * joystick.Value * Time.deltaTime;

		transform.position += new Vector3(deltaPos.x, 0, deltaPos.y);
	}
}
