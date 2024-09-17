using Godot;
using System;

public partial class Area2d : Area2D
{
	AnimationPlayer anim;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim = GetNode<AnimationPlayer>("/root/World/UI/Control/Overworld/ZoneAnim");
		GD.Print(anim);
	}

	public void OnEnter(Node2D body) 
	{
		if (body.Name == "Player")
		{
			GD.Print("x");
			GD.Print(anim.Name);
			anim.Play("zoneChange");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
