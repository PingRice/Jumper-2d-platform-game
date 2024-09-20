using Godot;
using System;

public partial class zoneRuins : Area2D
{
	AnimationPlayer anim;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim = GetNode<AnimationPlayer>("/root/World/UI/Control/Ruins/ZoneAnim");
		GD.Print(anim);
	}
	
	public void _OnEnter(Node2D body) 
	{
		if (body.Name == "Player")
		{
			GD.Print("x");
			GD.Print(anim.Name);
			anim.Play("zoneChange");
		}
	}
}
