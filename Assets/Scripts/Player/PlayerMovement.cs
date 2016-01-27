using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	private Vector3 movement;
	private Animator anim;
	private Rigidbody playerRigibody;

	private int floorMask;
	private float camRayLength=100f;

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigibody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turing ();
		Animating (h, v);
	}

	void Move(float h,float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		playerRigibody.MovePosition (transform.position + movement);
	}

	void Turing()
	{
		//Debug.Log (Input.mousePosition);
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse=floorHit.point-this.transform.position;
			playerToMouse.y=0f;

			Quaternion newRotation=Quaternion.LookRotation(playerToMouse);
			playerRigibody.MoveRotation(newRotation);
		}
	}

	void Animating(float h,float v)
	{
		bool walking = (h != 0f) || (v != 0f);
		anim.SetBool ("IsWalking", walking);
	}
}
