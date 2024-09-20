using Godot;
using System;

public partial class END : Area2D
{
	AnimationPlayer anim;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim = GetNode<AnimationPlayer>("/root/World/UI/Control/Slut/EndAnim");
	}

	private void _OnEnter(Node2D body)
	{
		if (body.Name == "Player")
		{
			anim.Play("theEnd");
		}
	}
}
