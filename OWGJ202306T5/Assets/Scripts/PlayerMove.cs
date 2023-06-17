using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// �v���C���[�̑���(�v���C���[�ɃA�^�b�`)
/// ���R�R��
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // �ړ�
    [SerializeField] GameObject player;        // �v���C���[�̃I�u�W�F�N�g
    private Vector3 playerPos;                 // �v���C���[�̈ʒu
    [SerializeField] float amoMove = 1.0f;     // �ړ���
    [SerializeField] float gravity = -9.81f;     // �d��
    private float xPos;

    // �W�����v
    private Rigidbody2D playerRb;          // �v���C���[�̓����蔻��
    private float jumpForce = 350f;        // �W�����v��
    private bool jump = false;             // �W�����v�����ǂ���

    // ������
    // �Z�[�u�|�C���g
    [SerializeField] GameObject savePoint;          // �Z�[�u�|�C���g�̃v���n�u
    private const string saveTag = "SaveObject";    // �Z�[�u�|�C���g�̃^�O
    private GameObject savePointObj;                // ���������Z�[�u�|�C���g
    private Rigidbody2D savePointRd;                // ���������Z�[�u�|�C���g��RigidBody
    // �p�x
    private bool nowThrow = false;      // �����悤�Ƃ��Ă��邩
    Vector3 startPosition;              // �����n�߂�ʒu
    private Vector3 mousePosition;      // �}�E�X�̍��W
    private Vector3 worldTarget;        // �}�E�X�̃��[���h���W
    private float throwAngle = 0.0f;    // ������p�x
    // ����
    private bool throwing = false; // �����Ă��邩
    float throwPower = 1.0f;    // �����鋭��
    Vector3 angle; // ���������
    float high;    // �ō��_�̍���

    //test


    void Start()
    {
        // �v���C���[�̈ʒu���擾
        playerPos = player.transform.position;

        // �v���C���[��RigidBody���擾
        playerRb = player.GetComponent<Rigidbody2D>();

        startPosition = savePoint.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // ���E�ړ�
        xPos = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(xPos, 0) * amoMove * Time.deltaTime);

        // �W�����v
        if (!jump && Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            playerRb.AddForce(transform.up * jumpForce);
        }
        if (jump) playerRb.AddForce(new Vector3(0, gravity, 0), (ForceMode2D)ForceMode.Force);

        // ��������������߂�
        if (!nowThrow) mousePosition = Input.mousePosition;

        // ������
        if (Input.GetMouseButtonDown(0)) ThrowPoint();
        if (Input.GetMouseButton(0)) throwPower += Time.deltaTime + 1.0f;
        if (Input.GetMouseButtonUp(0)) SaveThrow();
        //if (throwing) Throwing();

    }

    
    private void FixedUpdate()
    {
        if (throwing && savePointRd.transform.position.y >= high) GravityUp();
    }
    

    /// <summary>
    /// ���ɂ��Ă�Ƃ��̂݃W�����v�ɂ���
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        jump = false;
    }

    /// <summary>
    /// ����������̌���
    /// </summary>
    private void ThrowPoint()
    {
        // �����鋭�������Z�b�g
        throwPower = 0;

        // �v���C���[�̈ʒu���Œ�
        playerRb = player.GetComponent<Rigidbody2D>();
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;

        // �N���b�N�������W���擾
        worldTarget = Camera.main.ScreenToWorldPoint(mousePosition);
        worldTarget.z = 0;

        // �w��q�I�u�W�F�N�g�Ɠ����ʒu�ɃZ�[�u�|�C���g�𐶐�
        startPosition = GameObject.FindWithTag(saveTag).transform.position;
        savePointObj = Instantiate(savePoint, startPosition, Quaternion.identity);
        savePointRd = savePointObj.GetComponent<Rigidbody2D>();

        // ���������������
        angle = (worldTarget - playerPos).normalized;

        Debug.Log(angle);

        nowThrow = true;
    }//ThrowPoint()


    /// <summary>
    /// �����鋭��������
    /// </summary>
    private void SaveThrow()
    {
        savePointRd.bodyType = RigidbodyType2D.Dynamic;

        // ������
        savePointRd.AddForce(angle * throwPower);

        // �ō����B�_�ɒB����܂ł̎���
        float t = angle.y / gravity;

        // �ō��_�̍���
        high = angle.y * t - 0.5f * gravity * Mathf.Pow(t, 2);

        nowThrow = false;
        throwing = true;
        playerRb.constraints = RigidbodyConstraints2D.None;
    }

    /// <summary>
    /// �O���̒���
    /// </summary>
    private void GravityUp()
    {
        savePointRd.AddForce(new Vector2(1.0f, -4.0f), (ForceMode2D)ForceMode.Acceleration);
    }
}
    
