using UnityEngine;

public class Intuneric : MonoBehaviour
{
    public Light lumina;
    public Camera cam;
    public Color cerIntunecat = Color.black;
    public float luminaMinima = 0.05f;
    public float viteza = 5f;

    float luminaNormala;
    Color cerNormal;
    Color ambientNormal;
    bool intunecat = false;
    float nivel = 0f;

    void Start()
    {
        luminaNormala = lumina.intensity;
        cerNormal = cam.backgroundColor;
        ambientNormal = RenderSettings.ambientLight;
    }

    public void Porneste()
    {
        intunecat = true;
    }

    public void Opreste()
    {
        intunecat = false;
    }

    void Update()
    {
        if (intunecat == true)
        {
            nivel = nivel + viteza * Time.deltaTime;
        }
        else
        {
            nivel = nivel - viteza * Time.deltaTime;
        }
        nivel = Mathf.Clamp01(nivel);

        lumina.intensity = Mathf.Lerp(luminaNormala, luminaMinima, nivel);
        cam.backgroundColor = Color.Lerp(cerNormal, cerIntunecat, nivel);
        RenderSettings.ambientLight = Color.Lerp(ambientNormal, Color.black, nivel);
    }
}
