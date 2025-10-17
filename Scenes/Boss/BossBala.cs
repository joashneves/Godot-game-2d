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

	// --- MUDANÇA 1: Adiciona o método para mover a bala --- 
	public override void _PhysicsProcess(double delta)
	{
		Position += direction * Speed * (float)delta;
	}

	private void _on_timeout()
	{
		QueueFree();
	}

	public void Initilize(Vector2 dir)
	{
		direction = dir.Normalized();
		// --- MUDANÇA 2: Rotaciona o sprite da bala para apontar para onde está indo ---
		Rotation = direction.Angle();
	}

	private void OnBodyEntered(Node body)
	{
		if (body is Player player)
		{
			GD.Print("Encostrou no player : ", player);
			// Se você tiver um método TakeDamage() no player, pode chamá-lo aqui
			// player.TakeDamage(10);
			QueueFree();
		}
		// --- MUDANÇA 3 (Opcional): Destroi a bala se ela bater em uma parede ---
		else if (body is StaticBody2D) // Assumindo que suas paredes são StaticBody2D
        {
            QueueFree();
        }
	}
}
