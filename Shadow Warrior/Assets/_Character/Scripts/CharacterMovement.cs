using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Character
{
[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (ThirdPersonCharacter))]
public class CharacterMovement : MonoBehaviour
	{
		[SerializeField] float stoppingDistance = 1f;
		[SerializeField] float moveSpeedMultiplier = .7f;

		Animator animator;
	    ThirdPersonCharacter character; 
	    Vector3 clickPoint;
		GameObject walkTarget = null;
		NavMeshAgent agent;
		Rigidbody myRigidbody;
		bool IsGrounded;


	    void Start()
	    {
	        CameraRaycaster cameraRaycaster = Camera.main.GetComponent<CameraUI.CameraRaycaster>();
	        character = GetComponent<ThirdPersonCharacter>();
			walkTarget = new GameObject ("walkTarget");
			animator = GetComponent<Animator>();
			myRigidbody = GetComponent<Rigidbody> ();

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
				character.Move(agent.desiredVelocity, false);
			}
			else
			{
				character.Move(Vector3.zero, false);
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

		void OnAnimatorMove()
		{
			// we implement this function to override the default root motion.
			// this allows us to modify the positional speed before it's applied.
			if (IsGrounded && Time.deltaTime > 0)
			{
				Vector3 velocity = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

				// we preserve the existing y part of the current velocity.
				velocity.y = myRigidbody.velocity.y;
				myRigidbody.velocity = velocity;
			}
		}
	}
}