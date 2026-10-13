using UnityEngine;

public class SpinDetector : MonoBehaviour
{
    public float rotatieTotala = 0f;
    public float viteza = 0f;

    Quaternion lastRotation;

    void Start()
    {
        lastRotation = transform.rotation;
    }

    void Update()
    {
        float unghi = Quaternion.Angle(lastRotation, transform.rotation);
        rotatieTotala = rotatieTotala + unghi;
        lastRotation = transform.rotation;

        float vitezaAcum = unghi / Time.deltaTime;
        viteza = Mathf.Lerp(viteza, vitezaAcum, 0.2f);
    }

    public void ResetSpin()
    {
        rotatieTotala = 0f;
    }
}
