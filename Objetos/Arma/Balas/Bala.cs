using Godot;
using System;

public partial class Bala : Area2D
{
  [Export]
  public float Speed { get; set; } = 500;
  private Vector2 direction;
  // Se auto destroi quando toca no inimigo
  private void OnBodyEntered(Node body)
  {
	if (body is Inimigo inimigo)
	{
	  GD.Print("Encostou");
	  inimigo.TakeDamage(1, -direction);
	  QueueFree();
	}else if(body is Boss boss){
	  GD.Print("Encostou no boss");
	  boss.TakeDamage(1);
	  QueueFree();
	}
  }
  public void Initilize(Vector2 dir)
  {
	direction = dir.Normalized();
  }
  public override void _Ready()
  {
	base._Ready();

	BodyEntered += OnBodyEntered; // Conecta o sinal programaticamente
  }
  public override void _PhysicsProcess(double delta)
  {
	GlobalPosition += direction * Speed * (float)delta;
	// Destruir se sair da tela
	if (!GetViewportRect().HasPoint(GlobalPosition))
	{
	  QueueFree();
	}
  }

}
