///<summary>
///Ths Script is actuall really cool an funky. This will use the render camera and give us a sprite
///from a 3D model. All we have to do is position it, and press play.
///</summary>
using UnityEngine;
using System.IO;

//this can be taken out but doesn't reall matter
[ExecuteInEditMode]
public class SpriteFrom3DObject : MonoBehaviour
{
    public Camera renderCamera;
    [Tooltip("The object you want rendered as a sprite")]
    public GameObject targetObject;//the object that is to become a sprite
    public int resolution = 512;
    public string fileName = "CapturedSprite";

    void Start()
    {
        if (renderCamera == null || targetObject == null)
        {
            Debug.LogError("Camera or Target Object not assigned.");
            return;
        }

        // Create RenderTexture
        RenderTexture rt = new RenderTexture(resolution, resolution, 24, RenderTextureFormat.ARGB32);
        renderCamera.targetTexture = rt;
        renderCamera.clearFlags = CameraClearFlags.SolidColor;
        renderCamera.backgroundColor = new Color(0, 0, 0, 0); // Transparent

        // Render the camera
        renderCamera.Render();

        // Read from RenderTexture
        RenderTexture.active = rt;
        Texture2D image = new Texture2D(resolution, resolution, TextureFormat.RGBA32, false);
        image.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
        image.Apply();

        // Save PNG
        byte[] bytes = image.EncodeToPNG();
        string dirPath = Application.dataPath + "/Sprites";
        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath);

        File.WriteAllBytes($"{dirPath}/{fileName}.png", bytes);
        Debug.Log("Sprite saved to: " + dirPath + "/" + fileName + ".png");

        // Cleanup
        renderCamera.targetTexture = null;
        RenderTexture.active = null;
        DestroyImmediate(rt);
    }
}

