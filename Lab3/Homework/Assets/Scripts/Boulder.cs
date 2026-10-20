using UnityEngine;

public class Boulder : MonoBehaviour
{
    public ScoreManager scor;
    public GameObject exploziePrefab;
    public Color culoareLovit = Color.white;
    public float timpRespawn = 4f;
    public string suprafataExplozie = "BackWall";

    Rigidbody rb;
    Renderer rend;
    Collider col;
    Color culoareInitiala;
    Vector3 startPosition;
    Quaternion startRotation;
    bool lovit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        culoareInitiala = rend.material.color;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void Loveste(Vector3 forta, Vector3 punct, float distantaDeCentru, float distantaDeJucator)
    {
        rb.AddForceAtPosition(forta, punct);

        if (lovit == true)
        {
            return;
        }
        lovit = true;

        rend.material.color = culoareLovit;

        int puncte = 100 - Mathf.RoundToInt(distantaDeCentru * 150f);
        if (puncte < 10)
        {
            puncte = 10;
        }
        puncte = puncte + Mathf.RoundToInt(distantaDeJucator * 5f);
        scor.AdaugaPuncte(puncte);
    }

    void Update()
    {
        if (lovit == false)
        {
            return;
        }

        if (transform.position.y < -30f)
        {
            Explodeaza();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (lovit == false)
        {
            return;
        }
        if (collision.gameObject.name != suprafataExplozie)
        {
            return;
        }
        Explodeaza();
    }

    void Explodeaza()
    {
        Instantiate(exploziePrefab, transform.position, Quaternion.identity);

        lovit = false;
        rb.isKinematic = true;
        rend.enabled = false;
        col.enabled = false;
        Invoke("Respawn", timpRespawn);
    }

    void Respawn()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
        rend.material.color = culoareInitiala;
        rend.enabled = true;
        col.enabled = true;
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
