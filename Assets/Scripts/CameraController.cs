using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public Transform playerTransform;
	
	private float snapForce = 10f;
	
	void LateUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, playerTransform.position, snapForce * Time.deltaTime);
	}
	
}