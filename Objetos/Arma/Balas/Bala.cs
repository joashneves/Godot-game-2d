using Godot;
using System;

public partial class Bala : Node2D
{
  [Export]
  public float Speed { get; set; } = 500;
  private Vector2 direction;
  public void Initilize(Vector2 dir)
  {
    direction = dir.Normalized();
  }
  public override void _Process(double delta)
  {
    Position += direction * Speed * (float)delta;
    if (!GetViewportRect().HasPoint(GlobalPosition))
    {
      QueueFree();
    }

  }
}
