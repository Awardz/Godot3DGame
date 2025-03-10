using Godot;

public partial class ReturnButton : Button
{
	private PackedScene mainMenu = GD.Load<PackedScene>("res://scenes/main_menu.tscn");

	public override void _Ready()
	{
		Connect("pressed", new Callable(this, nameof(OnReturnPressed)));
		GD.Print("Return button initialized!");
	}

	private void OnReturnPressed()
	{
		GD.Print("Return button pressed!");
		GetTree().ChangeSceneToPacked(mainMenu);
	}
}
