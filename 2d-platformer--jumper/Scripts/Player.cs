using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Basic Movement Values
	public const float Speed = 250.0f;
	public const float fallSpeed = 175;
	public const float JumpVelocity = -500.0f;
	public const float des_rate = Speed*0.2f;
	public const float noclipSpeed = 200f;
	
	// Timers
	public double jumpTimer = 0.0d;
	public double coyoteTimer = 0.0d;

	// Bool's
	public bool jumpAvailable = true;
	public bool isFalling = false;
	private bool isJumping = false;
	public bool devMode = false;
	
	// Define Character
	CharacterBody2D player;
	AnimatedSprite2D aniSprite;
	AudioStreamPlayer AudioJump;
	// Startup
	public override void _Ready()
	{
		GD.Print("Player is Ready!");
		aniSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		AudioJump = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}

	// Main Physics
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		Vector2 platVel = new Vector2(0,0);

		if (Input.IsActionJustPressed("Noclip"))
		{
			if (devMode == false)
			{
				GD.Print("true");
				GetNode<CollisionShape2D>("CollisionShape2D").Disabled = true;
				devMode = true;
			}
			else
			{
				GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
				GD.Print("false");
				devMode = false;
			}
		}

		// Add the gravity.
		if (!IsOnFloor())
		{
			if (devMode == false) 
			{
				velocity += GetGravity() * (float)delta;
			}
			else
			{
				velocity.Y +=  3 * (float)delta;
			}
		}

		
		
		// Handle Jump.
		if (Input.IsActionJustPressed("Jump"))
		{
			if (devMode == false)
			{
				if (IsOnFloor())
				{
					velocity.Y = JumpVelocity;
				}
			}
			else
			{
				velocity.Y -= noclipSpeed;
			}
		}

		// Start Coyote Timer
		if (IsOnFloor()) 
		{
			coyoteTimer = 0.1d;
		}
		
		// Handle Coyote Jump
		if (Input.IsActionJustPressed("Jump")) 	
		{
			if (coyoteTimer >= 0d) 
			{
				velocity.Y = JumpVelocity;
			}
			// Start Jump Buffering Timer
			jumpTimer = 0.1d;
		}
		
		// Handle Coyote, Jump Buffering Timers
		jumpTimer -= delta;
		coyoteTimer -= delta;
		
		// Execute Jump Buffering
		if (jumpTimer > 0d && IsOnFloor()) 
		{
			velocity.Y = JumpVelocity;
		}
		
		// Handle Slowmove while Falling
		if (velocity.Y > 100)
		{
				isFalling = true;
		}
		else
		{
			isFalling = false;
		}

// Handle Jump SFX
{
	if (Input.IsActionJustPressed("Jump") && (IsOnFloor() || coyoteTimer > 0f))
	{
		isJumping = true;
		AudioJump.Play();
	}
	else if (!IsOnFloor()) // Check if no longer on floor (i.e. jumping)
	{
	//  Handle jump logic (if needed)
	}
	else // Reset isJumping when back on floor (after jump)
	{
	isJumping = false;
	}
}
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("Left", "Right", "Up", "Down");
		if (Input.IsActionJustPressed("Down"))
		{
			velocity.Y += noclipSpeed;
		}
		// Handle Movement
		if (direction != Vector2.Zero)
		{
			if (devMode == true)
			{
				velocity.X = direction.X * noclipSpeed;
			}
			else
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
			
		}
		else
		{
			if (devMode == false)
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, des_rate);
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, 150);
			}
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
		platVel = GetPlatformVelocity();
	}
}
