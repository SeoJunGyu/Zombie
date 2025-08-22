using UnityEngine;

public static class Tags
{
    private static readonly string PlayerTag = "Player";
}

public class PlayerMovement : MonoBehaviour
{
    private static readonly int hashMove = Animator.StringToHash("Move"); //Move 파라미터 id를 얻게된다.

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
        //회전
        var rotation = Quaternion.Euler(0f, input.Rot * rotateSpeed * Time.deltaTime, 0f); //000에서의 회전
        rb.MoveRotation(rb.rotation * rotation); //현재 회전량 * 회전하고싶은 회전량

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

        //이동
        var distance = input.Move * moveSpeed * Time.deltaTime; //forward 방향 이동량
        rb.MovePosition(transform.position + distance * transform.forward);

        //애니메이션
        //animator.SetFloat("Move", input.Move);
        animator.SetFloat(hashMove, input.Move);
    }
}
