using UnityEngine;

public class EarthBending : MonoBehaviour
{
    public Transform cap;
    public MainaAnimata mainaStanga;
    public MainaAnimata mainaDreapta;
    public GameObject piatraPrefab;
    public float distantaPiept = 0.35f;
    public float distantaInFata = 0.8f;

    bool asteaptaEliberare = false;

    void Update()
    {
        if (mainaStanga.valoareGrip < 0.8f)
        {
            asteaptaEliberare = false;
            return;
        }
        if (mainaDreapta.valoareGrip < 0.8f)
        {
            asteaptaEliberare = false;
            return;
        }
        if (asteaptaEliberare == true)
        {
            return;
        }

        Vector3 piept = cap.position + Vector3.down * 0.35f;
        if (Vector3.Distance(mainaStanga.transform.position, piept) > distantaPiept)
        {
            return;
        }
        if (Vector3.Distance(mainaDreapta.transform.position, piept) > distantaPiept)
        {
            return;
        }

        asteaptaEliberare = true;

        Vector3 inainte = cap.forward;
        inainte.y = 0f;
        inainte.Normalize();
        Vector3 tinta = cap.position + inainte * distantaInFata;
        Vector3 start = new Vector3(tinta.x, -1f, tinta.z);

        GameObject piatra = Instantiate(piatraPrefab, start, Quaternion.identity);
        piatra.GetComponent<Piatra>().Ridica(tinta);
    }
}
