using UnityEngine;
using Vuforia;

public class CactusProximityCombat : MonoBehaviour
{
    public ImageTargetBehaviour targetA;
    public ImageTargetBehaviour targetB;
    public Animator cactusA;
    public Animator cactusB;
    public float attackDistance = 0.25f;

    void Update()
    {
        if (targetA == null || targetB == null || cactusA == null || cactusB == null)
            return;

        bool bothTracked = targetA.TargetStatus.Status == Status.TRACKED
            && targetB.TargetStatus.Status == Status.TRACKED;

        float distance = Vector3.Distance(targetA.transform.position, targetB.transform.position);
        bool isAttacking = bothTracked && distance <= attackDistance;

        cactusA.SetBool("IsAttacking", isAttacking);
        cactusB.SetBool("IsAttacking", isAttacking);
    }
}
