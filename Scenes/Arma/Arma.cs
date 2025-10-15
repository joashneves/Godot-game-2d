using Godot;
using System;

public partial class Arma : Node2D
{
  [Export]
  public PackedScene BulletScene;
  private Timer shootTimer;
	public void SetFireUpdated(double seconds)
	{
		shootTimer.WaitTime -= Math.Max(0.02, shootTimer.WaitTime - seconds); 
		shootTimer.Start();
	}

  private void OnShootTimeout()
	{
		Node2D nearestEnemy = GetNearestEnemy();
		if (nearestEnemy != null)
		{
			var bullet = (Bala)BulletScene.Instantiate();
			GetTree().CurrentScene.AddChild(bullet);
			var dir = nearestEnemy.GlobalPosition - GlobalPosition;
			bullet.GlobalPosition = GetParent<Node2D>().GlobalPosition;
			bullet.Initilize(dir);
		}
		return;
	}

  private Node2D GetNearestEnemy()
  {
	var enemies = GetTree().GetNodesInGroup("inimigo");
	Node2D nearest = null;
	float nearestDist = float.MaxValue;

	foreach (Node node in enemies)
	{
	  if (node is Node2D enemy)
	  {
		float dist = GlobalPosition.DistanceTo(enemy.GlobalPosition);
		if (dist < nearestDist)
		{
		  nearestDist = dist;
		  nearest = enemy;
		}
	  }
	}
	return nearest;
  }

  public override void _Process(double delta)
  {
	Node2D nearestEnemy = GetNearestEnemy();
	if (nearestEnemy != null)
	  LookAt(nearestEnemy.GlobalPosition);
  }
  public override void _Ready()
  {
	shootTimer = GetNode<Timer>("Timer");
	shootTimer.Timeout += OnShootTimeout;
	shootTimer.Start();
  }


}
