using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public Renderer disc;
    public Color culoareNormala = new Color(0.6f, 0.3f, 1f, 0.35f);
    public Color culoareIncarcat = new Color(1f, 1f, 1f, 0.6f);
    public Color culoarePuls = new Color(0.2f, 0.5f, 1f, 0.7f);
    public float marimeMinima = 0.1f;
    public float marimeMaxima = 0.8f;
    public float vitezaRotatie = 180f;
    public float vitezaPuls = 6f;
    public float vitezaFadeOut = 0.5f;
    public float vitezaFadeIn = 3f;

    public float alpha = 1f;

    float marime = 0.1f;
    bool incarcat = false;
    bool seStinge = false;

    public void Show()
    {
        gameObject.SetActive(true);
        alpha = 1f;
        seStinge = false;
        SetProgres(0f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void FadeIn()
    {
        gameObject.SetActive(true);
        seStinge = false;
    }

    public void FadeOut()
    {
        seStinge = true;
    }

    public void SetProgres(float progres)
    {
        marime = Mathf.Lerp(marimeMinima, marimeMaxima, progres);

        if (progres >= 1f)
        {
            incarcat = true;
        }
        else
        {
            incarcat = false;
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, vitezaRotatie * Time.deltaTime);

        if (seStinge == true)
        {
            alpha = alpha - vitezaFadeOut * Time.deltaTime;
        }
        else
        {
            alpha = alpha + vitezaFadeIn * Time.deltaTime;
        }
        alpha = Mathf.Clamp01(alpha);

        Color culoare = culoareNormala;
        float marimeAcum = marime;

        if (incarcat == true)
        {
            float puls = (Mathf.Sin(Time.time * vitezaPuls) + 1f) / 2f;
            culoare = Color.Lerp(culoarePuls, culoareIncarcat, puls);
            marimeAcum = marime * (1f + puls * 0.1f);
        }

        culoare.a = culoare.a * alpha;
        disc.material.color = culoare;

        marimeAcum = marimeAcum * alpha;
        transform.localScale = new Vector3(marimeAcum, marimeAcum, marimeAcum);

        if (alpha <= 0f)
        {
            Hide();
        }
    }
}
