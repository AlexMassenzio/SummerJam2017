/*
 * NPC.cs
 * 
 * This is the base class for all Non-Player Characters in the game.
 *	You can customize each NPC to do something different with the following functions:
 *		bool IsInRange() - add logic that returns whether or not the target is in range.
 *		void ComputeVelocity() - Add any physics related to the character here. Use the velocity and targetVelocity variables to update.
 *		void Action() - When the player is both in range and actionable, the NPC will preform this function every frame for as long as those conditions are met.
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : PhysicsObject {

	protected GameObject target;

	protected DamageInfo di;
	public int damage = 0;
	public int health = 0;
	public float maxSpeed = 0;
	public float crouchSpeed = 0;
	public float jumpTakeOffSpeed = 0;
	public float cooldown = 0;
	public bool actionable; // Will the npc attack it's target if in range.

	void OnEnable()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D>();
	}
    
    protected override void Start ()
	{

        base.Start();
        
		di = new DamageInfo(damage);

        cs.health = health;
        cs.myDamageInfo = di;
        cs.maxSpeed = maxSpeed;
        cs.crouchSpeed = crouchSpeed;
        cs.jumpTakeOffSpeed = jumpTakeOffSpeed;
        cs.cooldown = cooldown;
	}
	
	protected override void Update ()
	{
		base.Update();

		if(IsInRange() && actionable)
		{
			Action();
		}
	}

	/// <summary>
	/// The code to handle attacking/interacting with the target. Default implementation does nothing.
	/// </summary>
	protected virtual void Action() {}

	/// <summary>
	/// Finds out if the target is in range of the NPC. Default implementation checks if the target is within 5 units of the NPC.
	/// </summary>
	/// <returns></returns>
	protected virtual bool IsInRange()
	{
        //return Vector3.Distance(this.transform.position, target.transform.position) < 5;
        // TODO: get target correctly
        return true;
    }

	/// <summary>
	/// Changes the object in which the NPC applies its AI to.
	/// </summary>
	/// <param name="target">The object that will be targeted</param>
	protected void SetTarget(GameObject target)
	{
		this.target = target;
	}
}