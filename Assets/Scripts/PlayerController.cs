using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance;
	
	private float speed = 16.0f;
	private float rotationSpeed = 90.0f;
	private float gravity = 9.8f;
	
	private Vector3 moveDirection = Vector3.zero;
	private float rotation = 0f;
	
	private Vector2 pushForce = Vector2.zero;
	
	private CharacterController controller;
	
	void Awake()
	{
		Instance = this;
		controller = GetComponent<CharacterController>();
	}
	
	public void Damage(float amt, Vector2 dir)
	{
		float pushMulti = 3f;
		Debug.Log("Damage");
		pushForce = dir * 4f;
	}
	
	void Update()
	{	
		if (controller.isGrounded)
		{
			moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		}
		
		if (pushForce.magnitude > 0.001f)
		{
			moveDirection += new Vector3(pushForce.x, 0f, pushForce.y);
			
			pushForce = Vector2.Lerp(pushForce, Vector2.zero, 5f * Time.deltaTime);
		}
		
		if(!controller.isGrounded)
			moveDirection.y -= gravity;
		
		transform.Rotate(0f, rotation * Time.deltaTime, 0f);
		controller.Move(moveDirection * Time.deltaTime);
	}
}