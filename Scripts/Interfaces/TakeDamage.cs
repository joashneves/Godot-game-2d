using Godot;
using System;

public interface IDamageable
{
  void TakeDamage(float amount, Vector2 pushDirection);
}