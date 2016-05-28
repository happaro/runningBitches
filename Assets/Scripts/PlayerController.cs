using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{
	public float speed = 10;
	public float rotateSpeed = 5;
	public float jumpForce = 200;
	public int currentPlayerNum = 1;

	private float h = 0, v = 0;
	private bool grounded;

	void OnCollisionEnter(Collision coll)
	{
		grounded = coll.gameObject.tag == "Ground" ? true : grounded;
	}

	void OnCollisionExit(Collision coll)
	{
		grounded = coll.gameObject.tag == "Ground" ? false : grounded;
	}

	void Start()
	{
		foreach (var joy in Input.GetJoystickNames())
			Debug.LogWarning(joy);
		
	}
		
	void Update()
	{	
		Movement();
		Rotating();
		if (Input.GetButtonDown (string.Format("joy{0}_button0", currentPlayerNum)) && grounded)
			Jump();
		if (Input.GetButtonDown ("best"))
			SceneManager.LoadScene(0);
	}

	void Movement()
	{
		Vector3 movement = this.transform.rotation * Vector3.forward;
		transform.GetComponent<Rigidbody>().MovePosition(this.transform.position + movement / (100f / speed));
	}

	void Jump()
	{
		transform.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
	}

	void Rotating()
	{
		transform.Rotate(0, rotateSpeed * Input.GetAxis(string.Format("joy{0}_horizontal", currentPlayerNum)), 0);
	}
}
