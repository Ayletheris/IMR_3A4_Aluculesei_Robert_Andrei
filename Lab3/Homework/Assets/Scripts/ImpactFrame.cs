using System.Collections;
using UnityEngine;

public class ImpactFrame : MonoBehaviour
{
    public Intuneric intuneric;
    public Light lumina;
    public Camera cam;
    public float durataCadru = 0.07f;

    public void Play()
    {
        StartCoroutine(Cadre());
    }

    IEnumerator Cadre()
    {
        intuneric.enabled = false;
        Time.timeScale = 0f;

        cam.backgroundColor = Color.white;
        lumina.intensity = 0f;
        RenderSettings.ambientLight = Color.black;
        yield return new WaitForSecondsRealtime(durataCadru);

        cam.backgroundColor = Color.black;
        RenderSettings.ambientLight = Color.white * 4f;
        yield return new WaitForSecondsRealtime(durataCadru);

        Time.timeScale = 1f;
        intuneric.enabled = true;
    }
}
