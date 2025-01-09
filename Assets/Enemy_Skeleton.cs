using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
	bool isAttacking;

	[Header("Movement info")]
	[SerializeField] private float moveSpeed;
	[Header("Player detection")]
	[SerializeField] private float playerCheckDistance;
	[SerializeField] private LayerMask playerLayer;

	private RaycastHit2D isPlayerDetected;




	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		base.Update();


		if (isPlayerDetected)
		{
			if (isPlayerDetected.distance > 1)
			{
				rb.velocity = new Vector2(facingDirection * 1.5f * moveSpeed, rb.velocity.y);
			}
			else
			{
				isAttacking = true;
			}

		}
		else
		{
			isAttacking = false;
		}

		if (!isGrounded || isWallDetected)
		{
			Flip();
		}
		Movement();
	}

	private void Movement()
	{
		if (!isAttacking)
		{
			rb.velocity = new Vector2(facingDirection * moveSpeed, rb.velocity.y);
		}

	}
	protected override void CollisionChecks()
	{
		base.CollisionChecks();

		isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDirection, playerLayer);
	}

	protected override void OnDrawGizmos()
	{
		base.OnDrawGizmos();

		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDirection, transform.position.y));
	}


}
