using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Character
{

	[RequireComponent (typeof (NavMeshAgent))]
	public class CharacterMovement : MonoBehaviour
		{
			[SerializeField] float stoppingDistance = 1f;
			[SerializeField] float moveSpeedMultiplier = .7f;
			[SerializeField] float movingTurnSpeed = 360;
			[SerializeField] float stationaryTurnSpeed = 180;
			[SerializeField] float moveThreshold =1f;
			[SerializeField] float animationSpeedMultiplier = 1.5f;
	
			Animator animator;
		    Vector3 clickPoint;
			NavMeshAgent agent;
			Rigidbody myRigidbody;
			float turnAmount;
			float forwardAmount;


		    void Start()
		    {
		        CameraRaycaster cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
				animator = GetComponent<Animator>();
				myRigidbody = GetComponent<Rigidbody> ();
				myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

				agent = GetComponent<NavMeshAgent> ();
				agent.updateRotation = false;
				agent.updatePosition = true;
				agent.stoppingDistance = stoppingDistance;

				cameraRaycaster.onMouseOverPotentiallyWalkable += OnMouseOverPotentiallyWalkable;
				cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
		    }
				
			void Update()
			{
				if (agent.remainingDistance > agent.stoppingDistance) 
				{
					Move(agent.desiredVelocity);
				}
				else
				{
				Move(Vector3.zero);
				}
			}

			void OnMouseOverPotentiallyWalkable(Vector3 destination)
			{
				if (Input.GetMouseButton (0)) 
				{
					agent.SetDestination (destination);
				}

			}

			void OnMouseOverEnemy (Enemy enemy)
			{
				if (Input.GetMouseButton (0) || Input.GetMouseButtonDown(1))
				{
					agent.SetDestination (enemy.transform.position);
				}
			}
			
			public void Move(Vector3 movement)
			{
				SetForwardAndTurn(movement);
				ApplyExtraTurnRotation();
				UpdateAnimator(movement);
			}

			public void Kill()
			{
				// to allow death signaling	
			}

			void SetForwardAndTurn (Vector3 movement)
			{
				if (movement.magnitude > moveThreshold) 
				{
					movement.Normalize ();
				}
				var localMove = transform.InverseTransformDirection (movement);
				turnAmount = Mathf.Atan2 (localMove.x, localMove.z);
				forwardAmount = localMove.z;
			}			

			void UpdateAnimator(Vector3 move)
			{
				// update the animator parameters
				animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
				animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
				animator.speed = animationSpeedMultiplier;
				
			}
			

			void ApplyExtraTurnRotation()
			{
				// help the character turn faster (this is in addition to root rotation in the animation)
				float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
				transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
			}
	}
}