using Godot;
using System;

public partial class OvRuins : Area2D
{
	AnimationPlayer anim;
	Label labelRU;
	Label labelOV;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim = GetNode<AnimationPlayer>("/root/World/UI/Control/Ruins/ZoneAnim");
		labelRU = GetNode<Label>("/root/World/UI/Control/Ruins");
		labelOV = GetNode<Label>("/root/World/UI/Control/Overworld");
		GD.Print("Ruins anim" + anim);GD.Print(labelRU);GD.Print(labelOV);
	}
	
	private void _OnEnter(Node2D body)
	{
		if (body.Name == "Player")
		{
			labelRU.Visible = true;
			labelOV.Visible = false;
			anim.Play("zoneChange");
		}
	}
}
