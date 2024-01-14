using System;
using Godot;

namespace Godotshaders.computer_shaders.example;

public partial class ExampleRenderingDevice : Node
{
    public override void _Ready() {
        RunShader();
    }

    private void RunShader() {
        //load shader
        RenderingDevice rd = RenderingServer.CreateLocalRenderingDevice();
        RDShaderFile shaderFile = GD.Load<RDShaderFile>("res://compute_shaders/example/compute_example.glsl");
        RDShaderSpirV shaderBytecode = shaderFile.GetSpirV();
        Rid shader = rd.ShaderCreateFromSpirV(shaderBytecode);
        
        //prepare input data
        float[] input = new float[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        byte[] inputBytes = new byte[input.Length * sizeof(float)];
        Buffer.BlockCopy(input, 0, inputBytes, 0, inputBytes.Length);
        // create a storage buffer for float values
        // each float is 4 bytes (32  bit) so 10x4 = 40 bytes
        Rid buffer = rd.StorageBufferCreate((uint) inputBytes.Length, inputBytes);

        // Create a uniform to assign buffer to rendering device
        RDUniform uniform = new RDUniform()
        {
            UniformType = RenderingDevice.UniformType.StorageBuffer,
            Binding = 0
        };
        uniform.AddId(buffer);
        Rid uniformSet = rd.UniformSetCreate(
            new Godot.Collections.Array<RDUniform>() { uniform },
            shader,
            0);
        
        //create a compute pipeline
        Rid pipeline = rd.ComputePipelineCreate(shader);
        long computeList = rd.ComputeListBegin();
        rd.ComputeListBindComputePipeline(computeList, pipeline);
        rd.ComputeListBindUniformSet(computeList, uniformSet, 0);
        rd.ComputeListDispatch(computeList, xGroups: 5, yGroups:1, zGroups:1);
        rd.ComputeListEnd();
        
        //submit to GPU and wait for sync note. normally wait a few frames...
        rd.Submit();
        rd.Sync();
        
        // read back data from buffers
        byte[] outputBytes = rd.BufferGetData(buffer);
        float[] output = new float[input.Length];
        Buffer.BlockCopy(outputBytes, 0, output, 0,outputBytes.Length);
        GD.Print("Input: ", string.Join(", ", input));
        GD.Print("Output: ", string.Join(", ", output));
    }
}