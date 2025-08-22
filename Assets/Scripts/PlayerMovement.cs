using UnityEngine;

public static class Tags
{
    private static readonly string PlayerTag = "Player";
}

public class PlayerMovement : MonoBehaviour
{
    private static readonly int hashMove = Animator.StringToHash("Move"); //Move �Ķ���� id�� ��Եȴ�.

    public float moveSpeed = 5f;
    public float rotateSpeed = 60f;

    private PlayerInput input;
    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //ȸ��
        var rotation = Quaternion.Euler(0f, input.Rot * rotateSpeed * Time.deltaTime, 0f); //000������ ȸ��
        rb.MoveRotation(rb.rotation * rotation); //���� ȸ���� * ȸ���ϰ���� ȸ����

        if (input.Fire)
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;
            if (GroupPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointTolook = cameraRay.GetPoint(rayLength);
                transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
            }
        }

        //�̵�
        var distance = input.Move * moveSpeed * Time.deltaTime; //forward ���� �̵���
        rb.MovePosition(transform.position + distance * transform.forward);

        //�ִϸ��̼�
        //animator.SetFloat("Move", input.Move);
        animator.SetFloat(hashMove, input.Move);
    }
}
