using UnityEngine;
using UnityEngine.InputSystem;

public class MainaAnimata : MonoBehaviour
{
    public InputActionProperty grip;
    public InputActionProperty trigger;
    public Animator animator;

    public float valoareGrip = 0f;
    public float valoareTrigger = 0f;

    void Update()
    {
        valoareGrip = grip.action.ReadValue<float>();
        valoareTrigger = trigger.action.ReadValue<float>();

        animator.SetFloat("Grip", valoareGrip);
        animator.SetFloat("Trigger", valoareTrigger);
    }
}
