using Godot;
using System;

public partial class Inimigo : CharacterBody2D
{
  [Export]
  public int Speed { get; set; } = 150;
  [Export]
  public NodePath PlayerPath;
  private Node2D player;

  public override void _Ready()
  {
    player = GetNode<Node2D>(PlayerPath);
  }

  public override void _PhysicsProcess(double delta)
  {
    if (player == null) return;

    Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
    Velocity = direction * Speed;
    MoveAndSlide();
  }
}
