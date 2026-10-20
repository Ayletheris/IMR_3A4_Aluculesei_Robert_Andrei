using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Staff : MonoBehaviour
{
    public SpinDetector spin;
    public MagicCircle cerc;
    public LaserBeam laser;
    public float rotatieNecesara = 360f;
    public float vitezaMinima = 30f;

    XRGrabInteractable grab;
    bool triggerApasat = false;

    void Start()
    {
        grab = GetComponent<XRGrabInteractable>();
        grab.useDynamicAttach = true;
        grab.selectMode = InteractableSelectMode.Multiple;
        grab.activated.AddListener(OnTriggerDown);
        grab.deactivated.AddListener(OnTriggerUp);
        grab.selectExited.AddListener(OnDrop);
    }

    void OnTriggerDown(ActivateEventArgs args)
    {
        triggerApasat = true;
        CancelInvoke("StingeCerc");
        spin.ResetSpin();
        cerc.Show();
    }

    void OnTriggerUp(DeactivateEventArgs args)
    {
        if (triggerApasat == false)
        {
            return;
        }
        triggerApasat = false;

        if (spin.rotatieTotala >= rotatieNecesara)
        {
            laser.Fire();
            Invoke("StingeCerc", laser.durata);
        }
        else
        {
            cerc.FadeOut();
        }
    }

    void OnDrop(SelectExitEventArgs args)
    {
        if (grab.isSelected == true)
        {
            return;
        }
        triggerApasat = false;
        cerc.Hide();
    }

    void StingeCerc()
    {
        cerc.FadeOut();
    }

    void Update()
    {
        if (triggerApasat == false)
        {
            return;
        }

        float progres = spin.rotatieTotala / rotatieNecesara;
        if (progres > 1f)
        {
            progres = 1f;
        }
        cerc.SetProgres(progres);

        if (progres >= 1f)
        {
            cerc.FadeIn();
        }
        else if (spin.viteza < vitezaMinima)
        {
            cerc.FadeOut();
            if (cerc.alpha <= 0f)
            {
                spin.ResetSpin();
            }
        }
        else
        {
            cerc.FadeIn();
        }
    }
}
