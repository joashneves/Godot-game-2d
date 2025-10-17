using Godot;
using System;

public partial class BuffCardButton : Button
{
  public override void _Pressed()
  {
    base._Pressed();
    GD.Print("Clicaram em mim");
    Visible = false;
  }
}
