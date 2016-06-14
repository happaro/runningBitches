using UnityEngine;
using System.Collections;

public class HeroBase: MonoBehaviour {

	public bool canPunch = true;
	public bool canJump = true;
	public bool canRun = true;
	public bool canUlty = true;
	public bool canGrab = true;
	public bool canRoll = true;

	public float moveSpeed;
	public float maxSpeed;
	public float rotateSpeed;
	public float jumpPower;
	public float punchPower;

	public Rigidbody rigid;

	void Awake()
	{
		rigid = this.GetComponent<Rigidbody>();
	}

}