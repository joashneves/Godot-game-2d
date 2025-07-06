using Godot;
using System;

public partial class Arma : Node2D
{
  
  	public override void _Process(double delta)
	{
		Node2D nearestEnemy = GetNearestEnemy();
		if (nearestEnemy != null)
			LookAt(nearestEnemy.GlobalPosition);
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

}
