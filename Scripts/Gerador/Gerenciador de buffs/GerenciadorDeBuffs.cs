using Godot;
using System;

public partial class GerenciadorDeBuffs : Control
{
  [Export]
  private Button _buffCardButton;


  public override void _Ready()
  {
    GD.Print("Mostra carta");
    _buffCardButton.Visible = true;
  }
  public void _on_click_buff()
  {
    GD.Print("Buff");
    _buffCardButton.Visible = false;
  }
}
