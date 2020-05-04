using UnityEngine;
using System.Collections;
 
public class Cross : MonoBehaviour {
 
    private float avgR = 0;
    private float avgG = 0;
    private float avgB = 0;
    private float avgA = 0;
    private float blurPixelCount = 0;
    public GameObject verticalHand,GlassSlab;
 
    public int radius =2;
    public int iterations =2;
 
    private Texture2D tex;
 
    // Use this for initialization
    void Start () {
        // tex =ToTexture2D(GetComponent<Renderer>().material.mainTexture);
        Texture mainTexture = GetComponent<Renderer>().material.mainTexture;
            Texture2D texture2D = new Texture2D(mainTexture.width, mainTexture.height, TextureFormat.RGBA32, false);
  
              RenderTexture currentRT = RenderTexture.active;
  
              RenderTexture renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 32);
              Graphics.Blit(mainTexture, renderTexture);
  
              RenderTexture.active = renderTexture;
              texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
              texture2D.Apply();
        tex = texture2D;
     
    }
 
    // Update is called once per frame
    void Update () {
        
        if (Input.GetKeyDown(KeyCode.Space)){
            Debug.LogError("HERE");
            GetComponent<Renderer>().material.mainTexture = (Texture)FastBlur( tex, radius, iterations);
        }
    }

    public void updateBlur(float n){
        if(n<=0.1f && n>=-0.1f){
            radius = 1;
            Debug.LogError("FOCUS");

            iterations = 1;
            GetComponent<Renderer>().material.mainTexture = (Texture)FastBlur( tex, radius, iterations);
        }
        else{
            Debug.LogError("NOT FOCUS");
            radius = 6;
            iterations = 1;
            GetComponent<Renderer>().material.mainTexture = (Texture)FastBlur( tex, radius, iterations);
        }
    }

 
  public  Texture2D ToTexture2D(Texture texture)
         {
             return Texture2D.CreateExternalTexture(
                 texture.width,
                 texture.height,
                 TextureFormat.RGB24,
                 false, false,
                 texture.GetNativeTexturePtr());
         }
 
    Texture2D FastBlur(Texture2D image, int radius, int iterations){
        Texture2D tex = image;
     
        for (var i = 0; i < iterations; i++) {
         
            tex = BlurImage(tex, radius, true);
            tex = BlurImage(tex, radius, false);
         
        }
     
        return tex;
    }
 
 
 
    Texture2D BlurImage(Texture2D image, int blurSize, bool horizontal){
     
        Texture2D blurred = new Texture2D(image.width, image.height);
        int _W = image.width;
        int _H = image.height;
        int xx, yy, x, y;
     
        if (horizontal) {
            for (yy = 0; yy < _H; yy++) {
                for (xx = 0; xx < _W; xx++) {
                    ResetPixel();
                 
                    //Right side of pixel
                 
                    for (x = xx; (x < xx + blurSize && x < _W); x++) {
                        AddPixel(image.GetPixel(x, yy));
                    }
                 
                    //Left side of pixel
                 
                    for (x = xx; (x > xx - blurSize && x > 0); x--) {
                        AddPixel(image.GetPixel(x, yy));
                     
                    }
                 
                 
                    CalcPixel();
                 
                    for (x = xx; x < xx + blurSize && x < _W; x++) {
                        blurred.SetPixel(x, yy, new Color(avgR, avgG, avgB, 1.0f));
                     
                    }
                }
            }
        }
     
        else {
            for (xx = 0; xx < _W; xx++) {
                for (yy = 0; yy < _H; yy++) {
                    ResetPixel();
                 
                    //Over pixel
                 
                    for (y = yy; (y < yy + blurSize && y < _H); y++) {
                        AddPixel(image.GetPixel(xx, y));
                    }
                    //Under pixel
                 
                    for (y = yy; (y > yy - blurSize && y > 0); y--) {
                        AddPixel(image.GetPixel(xx, y));
                    }
                    CalcPixel();
                    for (y = yy; y < yy + blurSize && y < _H; y++) {
                        blurred.SetPixel(xx, y, new Color(avgR, avgG, avgB, 1.0f));
                     
                    }
                }
            }
        }
     
        blurred.Apply();
        return blurred;
    }
    void AddPixel(Color pixel) {
        avgR += pixel.r;
        avgG += pixel.g;
        avgB += pixel.b;
        blurPixelCount++;
    }
 
    void ResetPixel() {
        avgR = 0.0f;
        avgG = 0.0f;
        avgB = 0.0f;
        blurPixelCount = 0;
    }
 
    void CalcPixel() {
        avgR = avgR / blurPixelCount;
        avgG = avgG / blurPixelCount;
        avgB = avgB / blurPixelCount;
    }
}