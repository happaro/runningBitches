using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum ButtonNum {Button0 = 0, Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9}

public class PlayerController : MonoBehaviour 
{

	public int currentPlayerNum = 1;
	public int currentJoyNum = 1;
	private bool grounded;
	public HeroBase hb;
	public bool pickUped;
	public bool pickUper;

	void Start()
	{
		hb = this.GetComponent<HeroBase> ();
	}

	void OnCollisionEnter(Collision coll)
	{
		grounded = coll.gameObject.tag == "Ground" ? true : grounded;
	}

	void OnCollisionExit(Collision coll)
	{
		grounded = coll.gameObject.tag == "Ground" ? false : grounded;
	}

	public PlayerController playerBehind;

	void OnTriggerEnter(Collider coll)
	{
		if (coll.tag == "Player" && !pickUper)
		{
			playerBehind = coll.GetComponent<PlayerController>();
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "Player" && !pickUper)
		{
			playerBehind = null;
		}
	}

	void TryPickUp()
	{
		if (playerBehind && !pickUper)
		{
			pickUper = true;
			playerBehind.pickUped = true;
			playerBehind.transform.position = this.transform.position;
			playerBehind.transform.Translate(0, 1.5f, 0);
			playerBehind.transform.parent = this.transform;
			playerBehind.hb.rigid.isKinematic = true;
		}
		else if (pickUper)
		{
			pickUper = false;
			playerBehind.pickUped = false;
			playerBehind.transform.parent = null;
			playerBehind.hb.rigid.isKinematic = false;
		}
	}

	void Update()
	{	
		if (!pickUped)
		{
			Movement(grounded);
			if (InputManager.GetCurrentJoyButton(currentJoyNum, ButtonNum.Button2) && grounded)
				Jump();
			if (InputManager.GetCurrentJoyButton(currentJoyNum, ButtonNum.Button3))
				TryPickUp();
		}
		Rotating();
		if (InputManager.GetCurrentJoyButton(currentJoyNum, ButtonNum.Button6))
			SceneManager.LoadScene(0);
	}
		
	float h, v;
	public float maxSpeed;

	void Movement(bool grounded)
	{
		    hb.rigid.AddForce(Mathf.Abs(hb.rigid.velocity.x) < maxSpeed ? InputManager.GetCurrentJoyAxis(currentJoyNum, AxisType.Horizontal) * hb.moveSpeed : 0, 0, Mathf.Abs(hb.rigid.velocity.z) < hb.maxSpeed ? InputManager.GetCurrentJoyAxis(currentJoyNum, AxisType.Vertical) * hb.moveSpeed : 0);
			if (Mathf.Abs(InputManager.GetCurrentJoyAxis(currentJoyNum, AxisType.Horizontal)) < 0.1f && grounded)
			hb.rigid.velocity = new Vector3(hb.rigid.velocity.x / 1.3f, hb.rigid.velocity.y, hb.rigid.velocity.z);
			if (Mathf.Abs(InputManager.GetCurrentJoyAxis(currentJoyNum, AxisType.Vertical)) < 0.1f && grounded)
			hb.rigid.velocity = new Vector3(hb.rigid.velocity.x, hb.rigid.velocity.y, hb.rigid.velocity.z / 1.3f);
	}

	void Jump()
	{
		Debug.Log ("Ok");
		transform.GetComponent<Rigidbody>().AddForce(0, hb.jumpPower, 0);
	}

	void Rotating()
	{
	  if (!(Mathf.Abs(InputManager.GetCurrentJoyAxis(currentJoyNum, AxisType.Vertical)) < 0.1f && Mathf.Abs(InputManager.GetCurrentJoyAxis(currentJoyNum, AxisType.Horizontal)) < 0.1f ))
		{
			h = InputManager.GetCurrentJoyAxis(currentJoyNum, AxisType.Horizontal);
			v = InputManager.GetCurrentJoyAxis(currentJoyNum, AxisType.Vertical);
		}
		var targetObjPos = new Vector3(this.transform.position.x + h, this.transform.position.y, this.transform.position.z + v);
		var targetRotation = Quaternion.LookRotation(targetObjPos - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
		 
	}
}