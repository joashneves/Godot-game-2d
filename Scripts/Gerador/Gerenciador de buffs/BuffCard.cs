using Godot;
using System;

// Este script deve ser anexado a um nó Button.
public partial class BuffCard : Button
{
    // Sinal personalizado que esta carta emitirá quando for escolhida.
    // Ele enviará o nome do buff como argumento.
    [Signal]
    public delegate void BuffSelectedEventHandler(string buffName);

    private string _buffName;

    // Use este método para configurar a carta com os detalhes do buff
    public void Configure(string buffName, string description)
    {
        _buffName = buffName;
        // Assumindo que a carta (o nó Button) tem um filho Label para mostrar a descrição
        var descriptionLabel = GetNode<Label>("DescriptionLabel");
        descriptionLabel.Text = description;
    }

    public override void _Ready()
    {
        GD.Print("Carta criada");
        // Conecta o sinal "pressed" do próprio botão a um método neste script.
        this.Pressed += OnCardPressed;
    }

    // Este método é chamado quando o botão/carta é clicado.
    private void OnCardPressed()
    {
        // Emite o nosso sinal personalizado, dizendo a quem estiver ouvindo qual buff foi escolhido.
        EmitSignal(SignalName.BuffSelected, _buffName);
    }
}
