using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	private float speed = 8.0f;
	private float rotationSpeed = 90.0f;
	private float gravity = 2.0f;
	
	private Vector3 moveDirection = Vector3.zero;
	private float rotation = 0f;
	
	private CharacterController controller;
	
	void Awake()
	{
		controller = GetComponent<CharacterController>();
	}
	
	void Update()
	{	
//		if (controller.isGrounded)
		{
			moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;
			rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		}
		
//		moveDirection.y -= gravity;
		transform.Rotate(0f, rotation * Time.deltaTime, 0f);
		controller.Move(moveDirection * Time.deltaTime);
	}
}