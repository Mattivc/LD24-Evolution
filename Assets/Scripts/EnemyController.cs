using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	private float detectRange = 14f;
	private float attackRange = 3f;
	
	private enum EnemyState {IDLE, AWARE, ATTACKING};
	private EnemyState enemyState = EnemyState.IDLE;
	
	private float speed = 8.0f;
	private float rotationSpeed = 180.0f;
	private float gravity = 9.8f;
	
	private Vector3 moveDirection = Vector3.zero;
	private float rotation = 0f;
	
	private CharacterController controller;
	private Animation enemyAnimation;
	
	private float lastAttackTime = Mathf.NegativeInfinity;
	private float attackSpeed = 1.5f;
	
	void Awake()
	{
		controller = GetComponent<CharacterController>();
		enemyAnimation = GetComponentInChildren<Animation>();
	}
	
	void Update()
	{
		
		switch (enemyState)
		{
		case EnemyState.IDLE:
			if (enemyAnimation.isPlaying)
				enemyAnimation.Stop();
			
			moveDirection = Vector3.zero;
			
			if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.z)) <= detectRange)
				enemyState = EnemyState.AWARE;
			
			break;
		case EnemyState.AWARE:
			if (!enemyAnimation.isPlaying)
				enemyAnimation.Play();
			
			moveDirection = transform.forward * speed;
			
			Vector3 playerVector = PlayerController.Instance.transform.position - transform.position;
			float playerAngle = Mathf.Atan2(playerVector.x, playerVector.z) * Mathf.Rad2Deg;
			
			rotation = Mathf.MoveTowardsAngle(rotation, playerAngle, rotationSpeed * Time.deltaTime);
			
			if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.z)) <= attackRange-1f)
				enemyState = EnemyState.ATTACKING;
			
			break;
		case EnemyState.ATTACKING:
			if (enemyAnimation.isPlaying)
				enemyAnimation.Stop();
			
			moveDirection = Vector3.zero;
			
			if (Time.timeSinceLevelLoad > lastAttackTime + attackSpeed)
			{
				Vector3 playerDamageVector = PlayerController.Instance.transform.position - transform.position;
				
				PlayerController.Instance.Damage(10f, new Vector2(playerDamageVector.x, playerDamageVector.z));
				lastAttackTime = Time.timeSinceLevelLoad;
			}
			
			if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.z)) > attackRange+1f)
				enemyState = EnemyState.AWARE;
			
			break;
		}
		
		if(!controller.isGrounded)
			moveDirection.y -= gravity;
		
		transform.localEulerAngles = new Vector3(0f, rotation, 0f);
		controller.Move(moveDirection * Time.deltaTime);
	}
	
	void OnDrawGizmos()
	{
		if (enemyState == EnemyState.AWARE)
			Gizmos.color = Color.red;
		else
			Gizmos.color = Color.green;
		
		Gizmos.DrawWireSphere(transform.position, detectRange);
		
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}
	
}
