using Godot;
using System;

public partial class Enemy2 : AnimatedSprite3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		public delegate void OnTimeoutEventHandler();
		_model = GetNode<Node3D>("Enemy1");
		_animation = GetNode<AnimationPlayer>("Enemy1/AnimationPlayer");
		if (Player != isOnFloor){
			_animation.Play("idle", 0.1f);
		}
		else if (Player == isOnFloor){
			_animation.Play("attack", 0.1f);
			_sound.Play("gun_shots", 0.1f);
		}
		else {
			_animation.Play("turnoff", 0.1f);
		}
		public void OnTimeout()
	{
		GD.Print("cd");
	}
	}

	/* Called every frame. 'delta' is the elapsed time since the previous frame (Idk how to do this part, I just did the top part bc I'm familiar with it).*/
	public override void _Process(double delta)
	{
	}
}
