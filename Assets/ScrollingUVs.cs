using UnityEngine;
using System.Collections;

using UnityEngine.UI;
 
public class ScrollingUVs : MonoBehaviour 
{
    public int materialIndex = 0;
    public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
    public string textureName = "_MainTex";
 
    Vector2 uvOffset = Vector2.zero;

    Renderer rend;
    Image image;

    void Awake() {
        rend = GetComponent<Renderer>();
        image = GetComponent<Image>();
    }
 
    void LateUpdate() 
    {
        uvOffset += ( uvAnimationRate * Time.deltaTime );
        image.materialForRendering.SetTextureOffset(textureName, uvOffset );
    }
}