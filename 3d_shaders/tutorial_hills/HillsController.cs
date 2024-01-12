using Godot;
using System;

[Tool]
public partial class HillsController : MeshInstance3D
{
	private float _scale = 0.5f;

	[Export]
	public float Scale
	{
		get => _scale;
		set => _scale = value;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ShaderMaterial shader = Mesh.SurfaceGetMaterial(0) as ShaderMaterial;
		shader.SetShaderParameter("height_scale", _scale);
	}
}
