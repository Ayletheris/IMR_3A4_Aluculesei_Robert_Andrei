using UnityEngine;

public class Explozie : MonoBehaviour
{
    public float durata = 0.6f;
    public float marimeFinala = 8f;

    float timp = 0f;

    void Update()
    {
        timp = timp + Time.deltaTime;

        float marime = marimeFinala * timp / durata;
        transform.localScale = new Vector3(marime, marime, marime);

        if (timp >= durata)
        {
            Destroy(gameObject);
        }
    }
}
