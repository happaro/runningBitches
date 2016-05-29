using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum ButtonNum {Button0 = 0, Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9}
public class PlayerController : MonoBehaviour 
{

	public float speed = 10;
	public float rotateSpeed = 5;
	public float jumpForce = 200;
	public int currentPlayerNum = 1;
	public int currentJoyNum = 1;
	private bool grounded;

	bool GetCurrentJoyButton(ButtonNum buttonNum)
	{
		return Input.GetButtonDown(string.Format("joy{0}_{1}", currentJoyNum, buttonNum.ToString().ToLower()));
	}

	void OnCollisionEnter(Collision coll)
	{
		grounded = coll.gameObject.tag == "Ground" ? true : grounded;
	}

	void OnCollisionExit(Collision coll)
	{
		grounded = coll.gameObject.tag == "Ground" ? false : grounded;
	}
		
	void Update()
	{	
		Movement(grounded);
		Rotating();
		if (GetCurrentJoyButton(ButtonNum.Button0) && grounded)
			Jump();
		if (GetCurrentJoyButton(ButtonNum.Button6))
			SceneManager.LoadScene(0);
	}

	void Movement(bool grounded)
	{
		Vector3 movement = this.transform.rotation * Vector3.forward;
		var v = movement / (100f / (grounded ? speed : speed / 2));
		transform.GetComponent<Rigidbody>().MovePosition(this.transform.position + v);
	}
	void Jump()
	{
		transform.GetComponent<Rigidbody>().AddForce(0, jumpForce, 0);
	}

	void Rotating()
	{
		transform.Rotate(0, rotateSpeed * Input.GetAxis(string.Format("joy{0}_horizontal", currentJoyNum)), 0);
	}
}
