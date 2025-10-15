using Godot;
using System;

public partial class GerenciadorDeBuffs : Node2D
{
    // Arraste a cena da sua carta de buff (BuffCard.tscn) para esta variável no editor do Godot.
    [Export]
    public PackedScene BuffCardScene { get; set; }

    // Nó que vai conter as cartas (ex: um HBoxContainer ou VBoxContainer)
    private Control _buffContainer;

    // Este método inicia o processo de seleção de buffs
    public void StartBuffSelection()
    {
        // Verifica se a cena da carta foi definida no editor
        if (BuffCardScene == null)
        {
            GD.PrintErr("A cena da carta de buff (BuffCardScene) não foi definida no GerenciadorDeBuffs!");
            return;
        }

        // Pausa o jogo
        GetTree().Paused = true;
        GD.Print("Jogo pausado para seleção de buff.");

        // Encontra o container para as cartas
        _buffContainer = GetNode<Control>("BuffContainer");
        if (_buffContainer == null)
        {
            GD.PrintErr("O nó 'BuffContainer' não foi encontrado como filho do GerenciadorDeBuffs!");
            GetTree().Paused = false; // Despausa o jogo se não puder continuar
            return;
        }
        _buffContainer.Visible = true;

        // Limpa cartas antigas, se houver
        foreach (Node child in _buffContainer.GetChildren())
        {
            child.QueueFree();
        }

        // Criar 3 cartas com buffs diferentes
        CreateBuffCard("Ataque Rápido", "Aumenta a velocidade de ataque.");
        CreateBuffCard("Vida Extra", "Aumenta a vida máxima.");
        CreateBuffCard("Mais Dano", "Aumenta o dano da arma.");
    }

    private void CreateBuffCard(string buffName, string description)
    {
        GD.Print($"Criando carta: {buffName}");
        var card = (BuffCard)BuffCardScene.Instantiate();
        card.Configure(buffName, description);
        card.BuffSelected += OnBuffSelected;
        _buffContainer.AddChild(card);
    }

    private void OnBuffSelected(string buffName)
    {   
        ApplyBuff(buffName);

        // Esconde e limpa o container
        if (_buffContainer != null)
        {
            foreach (Node child in _buffContainer.GetChildren())
            {
                child.QueueFree();
            }
            _buffContainer.Visible = false;
        }

        // Despausa o jogo
        GetTree().Paused = false;
        GD.Print("Jogo despausado.");
    }

    public void ApplyBuff(string buffName)
    {
        GD.Print($"Buff '{buffName}' selecionado e aplicado!");
        // Coloque aqui a lógica para aplicar o buff
    }
}