using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Transform tip;
    public LineRenderer line;
    public Intuneric intuneric;
    public ParticleSystem scantei;
    public ImpactFrame impact;
    public float durata = 1.5f;
    public float lungime = 80f;
    public float raza = 0.3f;
    public float fortaImpingere = 60f;
    public float latimeMaxima = 0.4f;
    public float timpCrestere = 0.3f;

    bool activ = false;
    float timp = 0f;

    void Start()
    {
        line.enabled = false;
    }

    public void Fire()
    {
        activ = true;
        timp = 0f;
        line.enabled = true;
        intuneric.Porneste();
        scantei.Play();
        impact.Play();
    }

    void FixedUpdate()
    {
        if (activ == false)
        {
            return;
        }

        Vector3 impingere = (tip.forward + Vector3.up * 0.3f) * fortaImpingere;

        RaycastHit[] lovituri = Physics.SphereCastAll(tip.position, raza, tip.forward, lungime);
        for (int i = 0; i < lovituri.Length; i++)
        {
            RaycastHit hit = lovituri[i];
            Boulder bolovan = hit.collider.GetComponent<Boulder>();
            if (bolovan != null)
            {
                Vector3 spreCentru = bolovan.transform.position - tip.position;
                float distantaDeCentru = Vector3.Cross(tip.forward, spreCentru).magnitude;
                bolovan.Loveste(impingere, hit.point, distantaDeCentru, hit.distance);
            }
        }
    }

    void Update()
    {
        if (activ == false)
        {
            return;
        }

        timp = timp + Time.deltaTime;

        float crestere = timp / timpCrestere;
        if (crestere > 1f)
        {
            crestere = 1f;
        }
        float puls = 1f + Mathf.Sin(timp * 30f) * 0.25f;
        float latime = latimeMaxima * crestere * puls;

        line.startWidth = latime;
        line.endWidth = latime;
        line.SetPosition(0, tip.position);
        line.SetPosition(1, tip.position + tip.forward * lungime);

        if (timp >= durata)
        {
            Opreste();
        }
    }

    void Opreste()
    {
        activ = false;
        line.enabled = false;
        intuneric.Opreste();
        scantei.Stop();
    }
}
