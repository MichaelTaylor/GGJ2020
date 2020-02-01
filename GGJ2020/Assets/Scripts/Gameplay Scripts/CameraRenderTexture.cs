using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraRenderTexture : MonoBehaviour
{
    [SerializeField]
    private Material _postProcessingMaterial;

    public void SetFadeValue(float value)
    {
        _postProcessingMaterial.SetFloat("_ColorThreshold", value);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination,_postProcessingMaterial);
    }
}
