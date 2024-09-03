using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Basic Movement Values
	public const float Speed = 250.0f;
	public const float fallSpeed = 175f;
	public const float JumpVelocity = -500.0f;
	public const float des_rate = Speed*0.2f;
	// Timers
	public double jumpTimer = 0.0d;
	public double coyoteTimer = 0.0d;
	// Bool's
	public bool jumpAvailable = true;
	public bool isFalling = false;
	
	// Define Character
	CharacterBody2D player;
	AnimatedSprite2D aniSprite;

	// Startup
	public override void _Ready()
	{
		GD.Print("Player is Ready!");
		aniSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Main Physics
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
		
		// Start Coyote Timer
		if (IsOnFloor()) 
		{
			coyoteTimer = 0.1d;
		}
		
		// Handle Coyote Jump
		if (Input.IsActionJustPressed("ui_accept")) 	
		{
			if (coyoteTimer >= 0d) 
			{
				velocity.Y = JumpVelocity;
			}
			// Start Jump Buffering Timer
			jumpTimer = 0.1d;
		}
		
		//Handle Coyote and Jump Buffering Timers
		jumpTimer -= delta;
		coyoteTimer -= delta;
		
		//Execute Jump Buffering
		if (jumpTimer > 0d && IsOnFloor()) 
		{
			velocity.Y = JumpVelocity;
		}
		
		//Handle Slowmove while Falling
		if (velocity.Y > 0)
		{
			isFalling = true;
		}
		else
		{
			isFalling = false;
		}


		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		
		// Handle Movement
		if (direction != Vector2.Zero)
		{
			if (isFalling == true)
			{
				velocity.X = direction.X * fallSpeed;
			}
			else
			{
				velocity.X = direction.X * Speed;
			}
		}
		else
		{
			
			velocity.X = Mathf.MoveToward(Velocity.X, 0, des_rate);
		}
		
		// Handle Animations
		
		// direction -1, 0, 1
		if (direction.X > 0) 
		{
			aniSprite.FlipH = false;
		} 
		else if (direction.X < 0) 
		{
			aniSprite.FlipH = true;
		}
			
		// Hvilken animation afspilles ud fra retning og underlag. 
		if (IsOnFloor() )
		{
			if (direction.X == 0)
			{
				aniSprite.Play("Idle");
			} 
			else 
			{
				aniSprite.Play("Walk");
			} 
		} 
		else 
		{
			if (velocity.Y > 0) 
			{
				aniSprite.Play("Fall");
			}
			else 
			{
				aniSprite.Play("Jump");
			}
		}
		

		Velocity = velocity;
		MoveAndSlide();
		
		
	}
}
