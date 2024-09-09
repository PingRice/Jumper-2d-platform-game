using Godot;
using System;

public partial class globalAudioPlayer : AudioStreamPlayer
{
	public override void _Ready()
	{
		if (Input.IsActionJustPressed("Jump")) {
			Stream = GD.Load<AudioStream>("res://jump_06.wav");
			Play();
		}
	}
}
