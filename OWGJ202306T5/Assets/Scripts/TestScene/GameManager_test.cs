using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_test : MonoBehaviour
{
    public int player_Life = 15;//�v���C���[�c�@

    public Vector2 save_tmp;//�Z�[�u�I�u�W�F�N�g�ʒu�ۑ��p

    public bool Is_SaveObjectActive;//�Z�[�u�I�u�W�F�N�g�͗L�����ǂ���

    
    private void Player_Action_Reset()//���Z�b�g����
    {

    }

    private void Start()
    {
        
    }

    private void Awake()
    {
        
    }

    private void Update()
    {
        save_tmp = GameObject.FindWithTag("SaveObject").transform.position;//�Z�[�u�I�u�W�F�N�g�̈ʒu���擾�G
        //Debug.Log("tmp.x;" + Save_tmp.x);
        //Debug.Log("tmp.y;" + Save_tmp.y);

    }
}
