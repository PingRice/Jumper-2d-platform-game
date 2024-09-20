using Godot;
using System;

public partial class OvSky : Area2D
{
	AnimationPlayer anim;
	Label labelOV;
	Label labelSK;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		anim = GetNode<AnimationPlayer>("/root/World/UI/Control/Sky/ZoneAnim");
		labelSK = GetNode<Label>("/root/World/UI/Control/Sky");
		labelOV = GetNode<Label>("/root/World/UI/Control/Overworld");
		GD.Print("sky anim" + anim);GD.Print(labelSK);GD.Print(labelOV);
	}

	private void _OnEnter(Node2D body)
	{
		if (body.Name == "Player")
		{
			labelOV.Visible = false;
			labelSK.Visible = true;
			anim.Play("zoneChange");
		}
	}
}
