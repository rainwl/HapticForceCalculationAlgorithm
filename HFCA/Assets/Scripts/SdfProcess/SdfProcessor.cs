using System;
using UnityEngine;
using System.IO;
public class SdfProcessor : MonoBehaviour
{
    public ComputeShader computeShader;
    private ComputeBuffer _sdfBuffer;
    private ComputeBuffer _outputBuffer;

    private void Start()
    {
        //Read SDF data
        var sdfData = ReadSdfDataFromFile(Application.dataPath+"/SDF/"+"sphereSDF.asset");
        
        //Create ComputeBuffer and pass SDF data to it
        _sdfBuffer = new ComputeBuffer(sdfData.Length, sizeof(float));
        _sdfBuffer.SetData(sdfData);
        
        //Create Output Buffer
        _outputBuffer = new ComputeBuffer(sdfData.Length, sizeof(float));
        
        //Set the parameters in ComputeShader
        computeShader.SetBuffer(0,"sdfBuffer",_sdfBuffer);
        computeShader.SetBuffer(0,"outputBuffer",_outputBuffer);
        
        //Call ComputeShader
        computeShader.SetInt("Result",42);// 42 is arbitrary
        computeShader.Dispatch(0,sdfData.Length/64,1,1);
        
        //Get data from output buffer
        var outputData = new float[sdfData.Length];
        _outputBuffer.GetData(outputData);
        
        //Save output
        SaveSdfDataToFile(Application.dataPath+"/SDF/"+"output.asset" ,outputData);
        
        //Dispose compute buffer
        _sdfBuffer.Release();
        _outputBuffer.Release();
    }

    private static float[] ReadSdfDataFromFile(string path)
    {
        var fileBytes = File.ReadAllBytes(path);
        // because fileBytes 's length is "(Multiple of 4) + 1",so I subtract the last element
        var sdfData = new float[(fileBytes.Length - 1) / sizeof(float)];
        var newSdfBytes = new byte[fileBytes.Length - 1];
        //
        Array.Copy(fileBytes,newSdfBytes,newSdfBytes.Length);
        Buffer.BlockCopy(newSdfBytes, 0, sdfData, 0, newSdfBytes.Length);
        return sdfData;
    }

    private static void SaveSdfDataToFile(string path, float[] data)
    {
        var fileBytes = new byte[data.Length * sizeof(float)];
        Buffer.BlockCopy(data,0,fileBytes,0,fileBytes.Length);
        File.WriteAllBytes(path,fileBytes);
    }

}
