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
	public int Life { get; set; } = 500;

	// A cena da bala agora é uma variável privada que será carregada no código.
	private PackedScene _bossBalaScene;

	[Export]
	public double ShootCooldown = 1.0; // Tempo em segundos entre cada tiro
	private double _shootTimer;

	private Player _player;
	private Vector2 _dashDirection;
	private bool _isDashing = false;
	private double _dashTimer;
	private double _cooldownTimer;

	public override void _Ready()
	{
		base._Ready();

		// --- MUDANÇA: Carrega a cena da bala usando o caminho dela, igual ao preload() ---
		_bossBalaScene = GD.Load<PackedScene>("res://Scenes/Boss/BossBala.tscn");

		_player = GetTree().GetFirstNodeInGroup("player") as Player;
		_cooldownTimer = DashCooldown;
		_shootTimer = ShootCooldown; // Garante que o boss possa atirar assim que o jogo começa
		GD.Print(Life);
	}

	private void StartDash()
	{
		_isDashing = true;
		_dashTimer = 0;
		_cooldownTimer = 0;

		if (_player != null)
		{
			_dashDirection = (_player.GlobalPosition - GlobalPosition).Normalized();
		}
		else
		{
			_dashDirection = Vector2.Right.Rotated((float)GD.RandRange(0, 2 * Math.PI));
		}
	}

	public void TakeDamage(int damege)
	{
		Life -= damege;
		GD.Print("VIda boss : ",Life);
		if (Life <= 0)
		{
			QueueFree(); // Destroi o inimigo
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		_cooldownTimer += delta;
		_shootTimer += delta;

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
				Vector2 posicaoPlayer = _player.GlobalPosition;
				LookAt(posicaoPlayer);
				float distancia = GlobalPosition.DistanceTo(posicaoPlayer);

				if (distancia < 250 && _shootTimer >= ShootCooldown)
				{
					_shootTimer = 0; // Reinicia o contador do tiro
					Shoot();
				}
			}

			// Verifica se pode dar o dash novamente
			if (_cooldownTimer >= DashCooldown && _player != null)
			{
				StartDash();
			}
		}

		MoveAndSlide();
	}

	private void Shoot()
	{
		if (_bossBalaScene == null)
		{
			GD.PrintErr("Não foi possível carregar a cena da bala. Verifique o caminho em _Ready().");
			return;
		}

		BossBala bullet = _bossBalaScene.Instantiate<BossBala>();
		
		GetTree().Root.AddChild(bullet);

		Vector2 directionToPlayer = (_player.GlobalPosition - GlobalPosition).Normalized();
		bullet.GlobalPosition = this.GlobalPosition;
		bullet.Initilize(directionToPlayer);
	}
}

