using Godot;
using System;

public partial class Boss : CharacterBody2D
{
	[Export]
	public float Speed = 100.0f;
	[Export]
	public float DashSpeed = 500.0f;
	[Export]
	public double DashDuration = 0.5; // seconds
	[Export]
	public double DashCooldown = 3.0; // seconds
  [Export]
  public int Life { get; set; } = 50;

	private Node2D _player;
	private Vector2 _dashDirection;
	private bool _isDashing = false;
	private double _dashTimer;
	private double _cooldownTimer;

  private void StartDash()
	{
		GD.Print("Boss iniciando dash!");
		_isDashing = true;
		_dashTimer = 0;
		_cooldownTimer = 0;

		if (_player != null)
		{
			_dashDirection = (_player.GlobalPosition - GlobalPosition).Normalized();
		}
		else
		{
			// Caso o player não seja encontrado
			_dashDirection = Vector2.Right.Rotated((float)GD.RandRange(0, 2 * Math.PI));
		}
	}
  public void TakeDamage(int damege)
  {
	Life -= damege;
	
	if (Life <= 0)
	{
	  QueueFree(); // Destroi o inimigo
	}
  }

	public override void _Ready()
	{
		// Busca pelo nó do player no grupo "player"
		_player = GetTree().GetFirstNodeInGroup("player") as Node2D;
		_cooldownTimer = DashCooldown; 
	}

	public override void _PhysicsProcess(double delta)
	{
		_cooldownTimer += delta;

		if (_isDashing)
		{
			_dashTimer += delta;
			if (_dashTimer >= DashDuration)
			{
				_isDashing = false;
				Velocity = Vector2.Zero;
			}
			else
			{
				Velocity = _dashDirection * DashSpeed;
			}
		}
		else
		{
			if (_player != null)
			{
				LookAt(_player.GlobalPosition);
			}

			// Verifica se pode dar o dash novamente
			if (_cooldownTimer >= DashCooldown && _player != null)
			{
				StartDash();
			}
		}

		MoveAndSlide();
	}

	
}
