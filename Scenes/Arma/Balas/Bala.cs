using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Bala : Area2D
{
    [Export]
    public float Speed { get; set; } = 500;
    private Vector2 direction;
    private Camera2D playerCamera;
    private Timer timer;
    public override void _Ready()
    {
        base._Ready();
        timer = GetNode<Timer>("Timer");

        // Pega a câmera do jogador do grupo. Certifique-se de que sua câmera está neste grupo.
        playerCamera = GetTree().GetFirstNodeInGroup("playerCamera") as Camera2D;
        BodyEntered += OnBodyEntered;
        //timer.Start();
    }
        private void _on_delete_bala()
    {
        GD.Print("Entrou no timer");
        QueueFree();
  }

  public override void _ExitTree()
  {
        base._ExitTree();
        GD.Print("bala deletado");
        timer.Stop();
  }
    public void Initilize(Vector2 dir)
    {
        direction = dir.Normalized();
    }

    private void OnBodyEntered(Node body)
    {
        if (body is Inimigo inimigo)
        {
            inimigo.TakeDamage(1, -direction);
            QueueFree();
        }
        else if (body is Boss boss)
        {
            boss.TakeDamage(1);
            QueueFree();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += direction * Speed * (float)delta;

        // Se a câmera do jogador foi encontrada, use-a para verificar os limites.
        if (playerCamera != null)
        {

            // Transforma a posição global da bala para a coordenada da câmera
            var cameraSpacePos = playerCamera.GetCanvasTransform() * GlobalPosition;

            // Pega o retângulo visível da câmera
            Rect2 cameraRect = playerCamera.GetViewportRect();

            // Se a bala estiver fora do retângulo da câmera, destrói a bala
            if (!cameraRect.HasPoint(cameraSpacePos))
            {
                //GD.Print("Camera encontrada e destruido bala");
                 timer.Start();
            }
        }
        else
        {
            GD.Print("Camera nao encontrada");
            // Se não encontrar a câmera, volta a usar o viewport principal.
            if (!GetViewportRect().HasPoint(GlobalPosition))
            {
                timer.Start();
            }
        }
    }
    

}