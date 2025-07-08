using Godot;
using System;

public partial class Inimigo : CharacterBody2D
{
  [Export]
  public int Speed { get; set; } = 150;
  [Export]
  public NodePath PlayerPath;
  [Export]
  public int Life { get; set; } = 5;
  private Node2D player;
  private Vector2 knockback = Vector2.Zero;

  public void TakeDamage(int damege, Vector2 pushDirection)
  {
    Life -= damege;
    knockback = pushDirection.Normalized() * 200;
    if (Life <= 0)
    {
      QueueFree(); // Destroi o inimigo
    } 
  }
  public override void _Ready()
  {
    player = GetNode<Node2D>(PlayerPath);
  }

  public override void _PhysicsProcess(double delta)
  {
    if (player == null) return;

    Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
    Vector2 moveDir = direction * Speed;

    moveDir += knockback;
    Velocity = moveDir;

    MoveAndSlide();

    knockback = knockback.MoveToward(Vector2.Zero, 50 * (float)delta);

    
  }
}
