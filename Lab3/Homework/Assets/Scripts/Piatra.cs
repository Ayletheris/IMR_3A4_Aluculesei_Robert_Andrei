using UnityEngine;

public class Piatra : MonoBehaviour
{
    public float vitezaRidicare = 3f;
    public float vitezaZbor = 25f;

    Rigidbody rb;
    Vector3 tinta;
    bool seRidica = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Ridica(Vector3 pozitie)
    {
        tinta = pozitie;
        seRidica = true;
        rb.isKinematic = true;
    }

    void Update()
    {
        if (seRidica == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, tinta, vitezaRidicare * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (seRidica == false)
        {
            return;
        }
        if (other.GetComponent<MainaAnimata>() == null)
        {
            return;
        }

        Vector3 directie = (transform.position - other.transform.position).normalized;
        seRidica = false;
        rb.isKinematic = false;
        rb.linearVelocity = directie * vitezaZbor;
    }
}
