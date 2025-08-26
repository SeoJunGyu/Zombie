using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public static readonly int IdReload = Animator.StringToHash("Reload");
    public Gun gun;
    private Rigidbody gunRigid;
    private Collider gunCollider;

    private Vector3 gunInitPosition;
    private Quaternion gunInitRotation;

    private PlayerInput input;
    private Animator animator;

    public Transform gunPivot;
    public Transform leftHandMount;
    public Transform rightHandMount;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        gunRigid = gun.GetComponent<Rigidbody>();
        gunCollider = gun.GetComponent<Collider>();
        gunInitPosition = gun.transform.localPosition;
        gunInitRotation = gun.transform.localRotation;
    }

    private void OnEnable()
    {
        gunRigid.isKinematic = true;
        gunCollider.enabled = false;
        gun.transform.localPosition = gunInitPosition;
        gun.transform.localRotation = gunInitRotation;
    }

    private void OnDisable()
    {
        gunRigid.linearVelocity = Vector3.zero;
        gunRigid.angularVelocity = Vector3.zero;
        gunRigid.isKinematic = false;
        gunCollider.enabled = true;
    }

    private void Update()
    {
        if(input.Fire)
        {
            gun.Fire();
        }
        else if(input.Reload)
        {
            if(gun.Reload())
            {
                animator.SetTrigger(IdReload);
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        gunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow); //ÇÇº¿ÀÌ ¿À¸¥ÂÊ ÆÈ²ÞÄ¡¿¡ ÀÖ°ÔµÈ´Ù.

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
    }
}
