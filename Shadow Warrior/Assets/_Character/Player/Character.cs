using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Character
{
	[SelectionBase]
	[RequireComponent (typeof (NavMeshAgent))]
	public class Character : MonoBehaviour
		{
			[Header("Animator")]
			[SerializeField] RuntimeAnimatorController animatorController;
			[SerializeField] AnimatorOverrideController animatorOverrideController;
			[SerializeField] Avatar characterAvatar;

			[Header("Audio")]
			[SerializeField] float audioSourceSpatialBlend = 0.5f;

			[Header("Capsule Collider")]
			[SerializeField] Vector3 colliderCenter = new Vector3(0, 1.03f, 0);
			[SerializeField] float colliderRadius = 0.2f;
			[SerializeField] float colliderHeight = 2.03f;

			[Header("Movement")]
			[SerializeField] float moveSpeedMultiplier = .7f;
			[SerializeField] float movingTurnSpeed = 360;
			[SerializeField] float stationaryTurnSpeed = 180;
			[SerializeField] float moveThreshold =1f;
			[SerializeField] float animationSpeedMultiplier = 1.5f;
	
			[Header("Nav Mesh Agent")]
			[SerializeField] float navMeshAgentSteeringSpeed = 1.0f;
			[SerializeField] float navMeshAgentStoppingDistance = 1.3f;

			Animator animator;
			NavMeshAgent navMeshAgent;
			Rigidbody myRigidbody;
			float turnAmount;
			float forwardAmount;
			bool isAlive = true;

			void Awake()
			{
				AddRequiredComponents ();
			}

			private void AddRequiredComponents()
			{
				var capsuleCollider = gameObject.AddComponent<CapsuleCollider> ();
				capsuleCollider.center = colliderCenter;
				capsuleCollider.radius = colliderRadius;
				capsuleCollider.height = colliderHeight;

				myRigidbody = GetComponent<Rigidbody> ();
				myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

				navMeshAgent = gameObject.AddComponent<NavMeshAgent> ();
				navMeshAgent.speed = navMeshAgentSteeringSpeed;
				navMeshAgent.stoppingDistance = navMeshAgentStoppingDistance;
				navMeshAgent.autoBraking = false;
				navMeshAgent.updateRotation = false;
				navMeshAgent.updatePosition = true;

				var audioSource = gameObject.AddComponent<AudioSource> ();
				audioSource.spatialBlend = audioSourceSpatialBlend;

				animator = gameObject.AddComponent<Animator>();
				animator.runtimeAnimatorController = animatorController;
				animator.avatar = characterAvatar;
			}
	
			void Update()
			{
				if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance && isAlive) 
				{
					Move(navMeshAgent.desiredVelocity);
				}
				else
				{
				Move(Vector3.zero);
				}
			}

			public void Kill()
			{
				isAlive = false;
			}

			public void SetDestination(Vector3 worldPos)
			{
				navMeshAgent.destination = worldPos;
			}

			public void Move(Vector3 movement)
			{
				SetForwardAndTurn(movement);
				ApplyExtraTurnRotation();
				UpdateAnimator(movement);
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