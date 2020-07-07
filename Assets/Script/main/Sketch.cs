using UnityEngine;

// Shader Loade
public class Sketch : MonoBehaviour
{
    [SerializeField]
    Material mat;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, mat);
    }
}
