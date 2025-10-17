using Godot;
using System;

public partial class BossBala : Area2D
{
  [Export]
  public float Speed { get; set; } = 500;
  private Vector2 direction;
  private Timer _timer;

  public override void _Ready()
  {
    base._Ready();
    _timer = GetNode<Timer>("Timer");
    _timer.Start();

  }
  private void _on_timeout()
  {
    QueueFree();
  }
  private void Initilize(Vector2 dir)
  {
    direction = dir.Normalized();
  }
  private void OnBodyEntered(Node body)
  {
    if (body is Player player)
    {
      GD.Print("Encostrou no player : ", player);
      QueueFree();
    }
  }
}
