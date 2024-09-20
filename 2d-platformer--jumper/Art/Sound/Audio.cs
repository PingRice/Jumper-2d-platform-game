using Godot;
using System;

public partial class Audio : AudioStreamPlayer
{
	public override void _Ready()
	{
		if (Input.IsActionJustPressed("Jump")) {
			Stream = GD.Load<AudioStream>("res://Art/Sound/JumpBoing.mp3");
			Play();
		}
	}
}
