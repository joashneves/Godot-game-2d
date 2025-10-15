using Godot;
using System;

public partial class GeradorInimigo : Node2D
{
  [Export]
  public PackedScene EnemyScene;
  private Timer spawnTimer;

  private Vector2 GetRandomEdgePosition(Vector2 screenSize)
  {
	Random rand = new();
	int side = rand.Next(4); // 0=top, 1=right, 2=bottom, 3=left

	float x = 0;
	float y = 0;

	switch (side)
	{
	  case 0: // Top
		x = (float)rand.NextDouble() * screenSize.X;
		y = -50;
		break;
	  case 1: // Right
		x = screenSize.X + 50;
		y = (float)rand.NextDouble() * screenSize.Y;
		break;
	  case 2: // Bottom
		x = (float)rand.NextDouble() * screenSize.X;
		y = screenSize.Y + 50;
		break;
	  case 3: // Left
		x = -50;
		y = (float)rand.NextDouble() * screenSize.Y;
		break;
	}
	return new Vector2(x, y);
  }

  private void CreatedEnemy()
  {
	// Pega o tamanho da tela
	var viewportSize = GetViewport().GetVisibleRect().Size;

	// Gera uma posição fora da tela (nas bordas)
	Vector2 spawnPos = GetRandomEdgePosition(viewportSize);

	var inimigo = (Inimigo)EnemyScene.Instantiate();
	inimigo.GlobalPosition = spawnPos;
	GetTree().CurrentScene.AddChild(inimigo);
	var players = GetTree().GetNodesInGroup("player");
	if (players.Count > 0)
	{
	  Node2D player = (Node2D)players[0];
	  NodePath pathToPlayer = inimigo.GetPathTo(player);
	  inimigo.PlayerPath = pathToPlayer;
	  inimigo.Setup((Node2D)players[0]);
	}

	// Define um novo tempo de espera aleatório entre 5 e 20 segundos para o próximo inimigo
	Random rand = new();
	spawnTimer.WaitTime = (float)(rand.NextDouble() * 15.0 + 5.0);
	spawnTimer.Start(); // Reinicia o timer com o novo tempo
  }
  public override void _Ready()
  {
	spawnTimer = GetNode<Timer>("Timer");
	spawnTimer.Timeout += CreatedEnemy;
	spawnTimer.Start();
  }

}
