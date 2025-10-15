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
  public void Setup(Node2D playerNode)
  {
    player = playerNode;
  }
  public override void _Ready()
  {
    if (player == null && PlayerPath != null && !PlayerPath.IsEmpty)
    {
      player = GetNode<Node2D>(PlayerPath);
    }
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
  public override void _ExitTree()
  {
    base._ExitTree();
    GD.Print("Deletado");
    var armas = GetTree().GetNodesInGroup("arma");
    if (armas.Count > 0)
    {
      var arma = (Arma)armas[0];
      double seconds = 0.2;
      //arma.SetFireUpdated(seconds); // diminui 0.2 segundos
    }
  }
}
