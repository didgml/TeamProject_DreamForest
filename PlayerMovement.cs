using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;  // �̵� �ӵ�
    public float gravityScale = 100f;  // �߷� ����

    private float hAxis, vAxis;  // �̵� ����
    private Vector3 moveVec;  // �̵� ����

    private Animator anim;  // �ִϸ�����
    private Rigidbody rb;  // ������ٵ�

    private AudioSource playerEff;  // �÷��̾� ȿ����
    public AudioClip walkClip;  // �߰��� �Ҹ�

    void Awake()
    {
        playerEff = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        // Rigidbody�� �߷� ��� ����
        rb.useGravity = false;
    }

    void Update()
    {
        // �÷��̾��� �̵� �Է°� �޾ƿ���
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        // �Ȱ� �ִ��� üũ�Ͽ� �ִϸ��̼� ���� ������Ʈ
        anim.SetBool("IsWalk", moveVec != Vector3.zero);

        // �̵� �������� ȸ��
        if (moveVec != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVec);
        }

        // �߰��� �Ҹ� ó��
        HandleFootsteps();
    }

    private void HandleFootsteps()
    {
        // �÷��̾ �Ȱ� ���� �� �߰��� �Ҹ� ���
        if (moveVec != Vector3.zero && !playerEff.isPlaying)
        {
            playerEff.PlayOneShot(walkClip);
        }
        // �÷��̾ ���߸� �߰��� �Ҹ� ����
        else if (moveVec == Vector3.zero && playerEff.isPlaying)
        {
            playerEff.Stop();
        }
    }

    void ApplyGravity()
    {
        // ���������� ������ �߷� ����
        Vector3 gravity = Vector3.down * gravityScale;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void FixedUpdate()
    {
        // �̵� �ӵ� �� �߷� ����
        MovePlayer();
        ApplyGravity();
    }

    private void MovePlayer()
    {
        // Rigidbody�� �ӵ��� ����Ͽ� �̵�
        Vector3 velocity = moveVec * speed * 0.5f;
        rb.velocity = velocity;
    }
}
