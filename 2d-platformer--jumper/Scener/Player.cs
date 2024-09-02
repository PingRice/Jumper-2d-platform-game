using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 250.0f;
public const float JumpVelocity = -500.0f;
public double jumpTimer = 0.0d;
public double coyoteTimer = 0.0d;
public const float des_rate = Speed*0.2f;
public bool jumpAvailable = true;
	

//Erklære 
AnimatedSprite2D aniSprite;
//Skabe reference
public override void _Ready()
{
	GD.Print("Player is ready.");
	aniSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	GD.Print(aniSprite);
}


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		if (IsOnFloor()) 
{
	coyoteTimer = 0.1d;
}			
if (Input.IsActionJustPressed("ui_accept")) 	
{
	if (coyoteTimer >= 0d) 
	{
		velocity.Y = JumpVelocity;
	}
	jumpTimer = 0.1d;
}
jumpTimer -= delta;
coyoteTimer -= delta;
if (jumpTimer > 0d && IsOnFloor()) 
{
	velocity.Y = JumpVelocity;
}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		// direction -1,0,1
		
		if(direction.X > 0) 
		{
			aniSprite.FlipH = false;
		} else if (direction.X < 0 ) 
		{
			aniSprite.FlipH = true;
		}
		
			
		// Hvilken animation afspilles ud fra retning og underlag. 
		if (IsOnFloor() )
		{
			if (direction.X == 0)
			{
				aniSprite.Play("Idle");
			} else {
				aniSprite.Play("Walk");
			} 
		} else {
				aniSprite.Play("Jump");		
		}
		
				
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		
		
	}
}
