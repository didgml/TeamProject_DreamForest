using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;  // 이동 속도
    public float gravityScale = 100f;  // 중력 배율

    private float hAxis, vAxis;  // 이동 방향
    private Vector3 moveVec;  // 이동 벡터

    private Animator anim;  // 애니메이터
    private Rigidbody rb;  // 리지드바디

    private AudioSource playerEff;  // 플레이어 효과음
    public AudioClip walkClip;  // 발걸음 소리

    void Awake()
    {
        playerEff = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        // Rigidbody의 중력 사용 중지
        rb.useGravity = false;
    }

    void Update()
    {
        // 플레이어의 이동 입력값 받아오기
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        // 걷고 있는지 체크하여 애니메이션 상태 업데이트
        anim.SetBool("IsWalk", moveVec != Vector3.zero);

        // 이동 방향으로 회전
        if (moveVec != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVec);
        }

        // 발걸음 소리 처리
        HandleFootsteps();
    }

    private void HandleFootsteps()
    {
        // 플레이어가 걷고 있을 때 발걸음 소리 재생
        if (moveVec != Vector3.zero && !playerEff.isPlaying)
        {
            playerEff.PlayOneShot(walkClip);
        }
        // 플레이어가 멈추면 발걸음 소리 멈춤
        else if (moveVec == Vector3.zero && playerEff.isPlaying)
        {
            playerEff.Stop();
        }
    }

    void ApplyGravity()
    {
        // 지속적으로 일정한 중력 적용
        Vector3 gravity = Vector3.down * gravityScale;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void FixedUpdate()
    {
        // 이동 속도 및 중력 적용
        MovePlayer();
        ApplyGravity();
    }

    private void MovePlayer()
    {
        // Rigidbody의 속도를 사용하여 이동
        Vector3 velocity = moveVec * speed * 0.5f;
        rb.velocity = velocity;
    }
}
