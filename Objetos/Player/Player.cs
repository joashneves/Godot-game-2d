using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 400;
	public Vector2 ScreenSize;

  public override void _Ready()
  {
		ScreenSize = GetViewportRect().Size;
  }
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
		}
		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
		}
		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}
		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
		}
		Velocity = velocity;
		MoveAndSlide();
  }
}
