using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_test : MonoBehaviour
{
    //GameObject player_Prefab;

    private Vector2 pos;
    private float horizontal;

    public bool Is_player_canmove = true;//�v���C���[�͈ړ��\���ǂ���

    public float player_speed = 1.0f;//�v���C���[�X�s�[�h

    public void Player_Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector2(horizontal, 0)*player_speed * Time.deltaTime);

        


    }

    private void Start()
    {
        pos = transform.position;//�V�[���Ăяo�������݈ʒu�擾
    }

    private void Update()
    {
        //�ړ�
        if (Is_player_canmove == true)
        {
            Player_Move();
        }
    }
}
