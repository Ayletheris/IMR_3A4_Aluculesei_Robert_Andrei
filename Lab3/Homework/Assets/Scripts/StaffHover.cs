using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StaffHover : MonoBehaviour
{
    public float vitezaRotatie = 40f;
    public float vitezaRidicare = 3f;

    XRGrabInteractable grab;
    Rigidbody rb;
    bool pluteste = false;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
        grab.throwOnDetach = false;
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnDrop);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        pluteste = false;
    }

    void OnDrop(SelectExitEventArgs args)
    {
        if (grab.isSelected == true)
        {
            return;
        }
        pluteste = true;
    }

    void Update()
    {
        if (pluteste == false)
        {
            return;
        }

        rb.isKinematic = true;

        Quaternion indreptare = Quaternion.FromToRotation(transform.forward, Vector3.up);
        Quaternion pas = Quaternion.Slerp(Quaternion.identity, indreptare, vitezaRidicare * Time.deltaTime);
        transform.rotation = pas * transform.rotation;

        transform.Rotate(Vector3.up, vitezaRotatie * Time.deltaTime, Space.World);
    }
}
