using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour 
{
	public float speed = 10;
	public float rotateSpeed = 5;
	public float jumpForce = 200;
	public bool fourDir;
	public int currentPlayerNum = 1;

	private float h = 0, v = 0;

	void Update()
	{
		if (Input.GetButtonDown ("Cancel"))
			SceneManager.LoadScene(0);
		Movement();
		Rotating();

		/*if (Mathf.Abs(Input.GetAxis("joy1_horizontal")) > 0.1f)
			Debug.LogWarning("Joy 1 : " + Input.GetAxis("joy1_horizontal"));
		if (Mathf.Abs(Input.GetAxis("joy2_horizontal")) > 0.1f)
			Debug.LogWarning("Joy 2 : " + Input.GetAxis("joy2_horizontal"));*/
		if (Input.GetButtonDown ("Jump") && this.transform.position.y < 26)
			Jump();
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
		if (fourDir)
		{
			if (!(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0))
			{
				h = Input.GetAxis("Horizontal");
				v = Input.GetAxis("Vertical");
			}
			var targetObjPos = new Vector3(this.transform.position.x + h, this.transform.position.y, this.transform.position.z + v);
			var targetRotation = Quaternion.LookRotation(targetObjPos - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
		}
		else
		{
			transform.Rotate(0, rotateSpeed * Input.GetAxis(string.Format("joy{0}_horizontal", currentPlayerNum)), 0);

			//var newRotation = new Quaternion(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 10 * Input.GetAxis("Horizontal"), transform.rotation.eulerAngles.z, 0);
			//transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, speed * Time.deltaTime);
		}
	}
}
