using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    //GameObject player_Prefab;
    //[Header("������GameManager�I�u�W�F�N�g������")]
    //public GameObject gameManagerObject;//GameManager�ۑ��p

    private Vector2 pos;
    private float horizontal;

    public bool Is_player_canmove = true;//�v���C���[�͈ړ��\���ǂ���

    [SerializeField]public float player_Speed = 1.0f;//�v���C���[�X�s�[�h

    public void Player_Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector2(horizontal, 0)*player_Speed * Time.deltaTime);


    }

    private void Start()
    {
        pos = transform.position;//�V�[���Ăяo�������݈ʒu�擾
    }

    private void FixedUpdate()
    {
        //�ړ�
        if (Is_player_canmove == true)
        {
            Player_Move();
        }
    }
}
