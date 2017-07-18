using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Screen : MonoBehaviour
{

    public enum Layer
    {
        BASE,
        FLOATING
    }

    public static Screen instance;

    public static Vector2 dims = new Vector3(60, 33);
    public Vector2 chardims = new Vector3(16, 32);

    static Material mat;

    [HideInInspector]
    public static Texture2D baseTexture;
    static Color32[] baseBuffer;


    [HideInInspector]
    public static Texture2D floatingTexture;
    static Color32[] floatingBuffer;


    bool primaryDown;
    bool secondaryDown;
    Camera cam;

    [HideInInspector]
    public uint x, y;

    [HideInInspector]
    public static Vector2 pointerPos;
    static Vector2 lastPointerPos;
    static bool bPointerMoved = false;

    List<ScreenComponent> components = new List<ScreenComponent>();

    void Start()
    {
        instance = this;
        cam = Camera.main;
        baseTexture = new Texture2D((int)dims.x, (int)dims.y, TextureFormat.ARGB32, false);
        baseTexture.filterMode = FilterMode.Point;
        baseBuffer = new Color32[(int)(dims.x * dims.y)];

        floatingTexture = new Texture2D((int)dims.x, (int)dims.y, TextureFormat.ARGB32, false);
        floatingTexture.filterMode = FilterMode.Point;
        floatingBuffer = new Color32[(int)(dims.x * dims.y)];

        for (int y = 0; y < dims.y; y++)
        {
            for (int x = 0; x < dims.x; x++)
            {
                Color c = new Color(1, 0.078125f, 0.015625f);
                baseTexture.SetPixel(x, y, c);
            }
        }

        floatingTexture.Apply();
        baseTexture.Apply();

        mat = GetComponent<Renderer>().material;



        //LoadTexture( ".png");

    }



    void GetPointerPosition()
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        Vector2 pixelUV = hit.textureCoord;
        uint x = (uint)(pixelUV.x * dims.x);
        uint y = ((uint)dims.y-1) - (uint)(pixelUV.y * dims.y);

        pointerPos = new Vector2(x, y);
        bPointerMoved = false;
        if (pointerPos != lastPointerPos)
        {
            bPointerMoved = true;
        }
        lastPointerPos = pointerPos;

    }

    public static void ClearLayer()
    {
        //System.Array.Clear(textureBuffer, 0, textureBuffer.Length);
        System.Array.Clear(floatingBuffer, 0, floatingBuffer.Length);
    }


    public static void ClearTexture()
    {
        //System.Array.Clear(textureBuffer, 0, textureBuffer.Length);
        System.Array.Clear(baseBuffer, 0, floatingBuffer.Length);
    }


    public static void PollMouse()
    {
        instance.MouseInput();
    }

    void MouseInput()
    {
        GetPointerPosition();

        ScreenComponent topComponent = ScreenComponent.GetTopComponent();

        if (topComponent != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!primaryDown)
                {
                    topComponent.PrimaryDown(pointerPos);
                    primaryDown = true;
                }
                else
                {
                    if (bPointerMoved) topComponent.PrimaryDrag(pointerPos);
                }

            }
            else if (primaryDown)
            {
                topComponent.PrimaryUp();
                primaryDown = false;
            }

            if (Input.GetMouseButton(1))
            {
                if (!secondaryDown)
                {
                    topComponent.SecondaryDown(pointerPos);
                    secondaryDown = true;
                }
                else
                {
                    if (bPointerMoved) topComponent.SecondaryDrag(pointerPos);
                }

            }
            else if (secondaryDown)
            {
                topComponent.SecondaryUp();
                secondaryDown = false;
            }

        }

    }

    public static void PrepareBuffer()
    {
        ClearLayer();

    }

    public static void DrawBuffer()
    {

        /*
        for (int i = 0; i < components.Count; i++)
        {
            components[i].ScreenUpdate();
        }
        */

        floatingTexture.SetPixels32(floatingBuffer);
        floatingTexture.Apply();

        baseTexture.SetPixels32(baseBuffer);
        baseTexture.Apply();

        instance.UpdateUniforms();
    }

    static uint CreateBufferIndex(uint x, uint y)
    {
        return (((uint)dims.y - 1) - y) * (uint)dims.x + x; 
    }

    public static Color32 GetPixel(Vector2 pos, byte layer = 0)
    {
        return GetPixel((uint)pos.x, (uint)pos.y, layer);
    }

    public static Color32 GetPixel(uint x, uint y, byte layer = 0)
    {
        uint index = CreateBufferIndex(x, y);
        if (index > baseBuffer.Length - 1) return new Color32();

        if (layer==1) return floatingBuffer[index];

        return baseBuffer[index];
    }


    public static void SetWorldPixelInScreenSpace(Vector3 location, Color32 brush, Layer layer = Layer.BASE) {
        SetPixel((Vector2)location - World.view.position, brush, layer);
    }

    public static void SetPixel(Vector2 location, Color32 brush, Layer layer = Layer.BASE)
    {
       SetPixel((uint)location.x, (uint)location.y, brush, layer);
    }

    public static void SetPixel(uint x, uint y, Color32 brush, Layer layer = Layer.BASE)
    {
        if (x >= 0)
            if (y >= 0)
                if (x < dims.x)
                    if (y < dims.y)
                    {
                        uint index = CreateBufferIndex(x, y);
                        if (index > baseBuffer.Length - 1) return;

                        if (layer == Layer.FLOATING)
                        {
                            floatingBuffer[index] = brush;
                        }
                        else
                        {
                            baseBuffer[index] = brush;
                        }
                    }
    }





    void UpdateUniforms()
    {

        //        mat.SetTexture("_BaseLayer", baseTexture);

        mat.SetTexture("_BaseLayer", Landscape.texture);
        mat.SetTexture("_FloatingLayer", floatingTexture);
        mat.SetVector("_Dims", new Vector4(dims.x, dims.y, 1.0f / dims.x, 1.0f / dims.y)); // screen dimensions and pre-computed reciprocal values
        mat.SetVector("_CharDims", new Vector4(chardims.x, chardims.y, 1.0f / chardims.x, 1.0f / chardims.y)); // character map dimensions

        Vector2 viewOffset = new Vector2(World.view.x / Landscape.dims.x, ((Landscape.dims.y - dims.y) - World.view.y) / Landscape.dims.y);

        mat.SetVector("_BaseLayerDims", new Vector4(viewOffset.x, viewOffset.y, dims.x / Landscape.dims.x, dims.y / Landscape.dims.y)); // character map dimensions

    }



    public void SaveTexture(string filePath)
    {
        StartCoroutine(ISaveTexture(filePath));
    }

    IEnumerator ISaveTexture(string filePath)
    {

        yield return new WaitForEndOfFrame();
        byte[] bytes = baseTexture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Resources/"+ filePath, bytes);

        gameObject.BroadcastMessage("OnSaved", SendMessageOptions.DontRequireReceiver);

    }

    public void LoadTexture(string filePath)
    {

        byte[] fileData;
        Debug.Log("loading texture: " + filePath);
        if (File.Exists(Application.dataPath+"/Resources/"+filePath))
        {
            fileData = File.ReadAllBytes(Application.dataPath + "/Resources/" + filePath);
            baseTexture.LoadImage(fileData); //..this will auto-resize the texture dimensions.
            //texture.Resize((int)dims.x, (int)dims.y);
            baseTexture.Apply();

            baseBuffer = baseTexture.GetPixels32();
            mat.SetTexture("_Buffer", baseTexture);

            gameObject.BroadcastMessage("OnLoaded", SendMessageOptions.DontRequireReceiver);

        }
    }


    public static Color32 GenerateBrush(int fg = 63, int bg = 0, int c = 0, int page = 0)
    {
        byte r = (byte)(c);
        byte g = (byte)(fg + (page * 64));
        byte b = (byte)(bg);
        byte a = 255;
        return new Color32(r,g,b,a);
    }


}
