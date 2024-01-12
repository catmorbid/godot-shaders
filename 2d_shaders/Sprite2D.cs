using Godot;
using System;

[Tool]
public partial class Sprite2D : Godot.Sprite2D
{
	private float _speed;

	[Export]
	public float Speed
	{
		get => _speed;
		set => _speed = value;
	}
	private float blue;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Rotation += (float)delta * _speed;
		blue = blue += (float)delta * _speed;
		blue = blue % 1.0f;
		((ShaderMaterial)Material).SetShaderParameter("blue", blue);
	}
}
