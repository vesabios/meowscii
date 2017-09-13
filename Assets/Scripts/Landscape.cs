using UnityEngine;
using System.Collections;
using System.IO;

public class Landscape : MonoBehaviour {

    public static Vector2 dims = new Vector2(128, 128);

    static public Texture2D texture;
    static Color32[] buffer;

    static Landscape instance;

    static string zoneName;

    void Awake()
    {
        instance = this;

    }

    void Start () {


    }

    public static void LoadStaticZone(string n)
    {
        zoneName = n;
        LoadTexture(zoneName + ".png");
    }

    public static void SaveStaticZone()
    {
        SaveTexture(zoneName + ".png");
    }

    static uint CreateBufferIndex(Vector2 location)
    {
		float x = Mathf.Clamp (location.x, 0, dims.x);
		float y = Mathf.Clamp (location.y, 0, dims.y);
		return CreateBufferIndex((uint)x, (uint)y);
    }

    static uint CreateBufferIndex(uint x, uint y)
    {
        return (((uint)dims.y - 1) - y) * (uint)dims.x + x;
    }

    public static Color32 GetPixel(uint x, uint y)
    {
        uint index = CreateBufferIndex(x, y);
        return buffer[index];
    }

    public static void SetPixel(uint x, uint y, Color32 brush)
    {
        if (x >= 0)
            if (y >= 0)
                if (x < dims.x)
                    if (y < dims.y)
                    {
                        uint index = CreateBufferIndex(x, y);
                        if (index > buffer.Length - 1) return;

                        buffer[index] = brush;
                       
                    }
    }


    public static void UpdateTextureFromBuffer()
    {
        texture.SetPixels32(buffer);
        texture.Apply();
    }
	
    public static void SaveTexture(string filePath)
    {
        texture.SetPixels32(buffer);
        texture.Apply();

        byte[] bytes = texture.EncodeToPNG();
        Debug.Log("Writing to texture: " + filePath);
        File.WriteAllBytes(Application.dataPath + "/Resources/Atlas/" + filePath, bytes);
    }

    public static void Initialize()
    {
        texture = new Texture2D((int)dims.x, (int)dims.y, TextureFormat.ARGB32, false);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;

        buffer = new Color32[(int)(dims.x * dims.y)];
    }

    public static void LoadTexture(string filePath)
    {

        Initialize();

        byte[] fileData;
        if (File.Exists(Application.dataPath + "/Resources/Atlas/" + filePath))
        {
            Debug.Log("found " + Application.dataPath + "/Resources/Atlas/" + filePath);
            fileData = File.ReadAllBytes(Application.dataPath + "/Resources/Atlas/" + filePath);
            texture.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            texture.Apply();
            buffer = texture.GetPixels32();

        }
    }


    public static bool IsLocationTraversable(Vector2 location)
    {
        uint index = CreateBufferIndex(location);
        Color32 brush = buffer[index];

        switch (brush.r)
        {
            case 176:
                {
                    return true;
                }
        }
 
        return false;
    }
}
