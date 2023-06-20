using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class t_GenerateSaveObj : MonoBehaviour
{
    /// <summary>
    /// �e�X�g�p�ɃZ�[�u�I�u�W�F�N�g���}�E�X�̒n�_�ɐ���
    /// �r���h�p�ɂ͑g�ݍ��܂Ȃ�����
    /// �������A�ua�v�ƃR�����g���t���Ă���s��GameManager�̃Z�[�u�I�u�W�F�N�g�֘A��bool�ϐ���M���Ă�̂�
    /// ��y������Ă��ꂽ�X�N���v�g�̂��������̍s�ɒǉ�����
    /// 
    /// S2G215
    /// </summary>


    Vector2 mousePos;
    [SerializeField] private GameObject saveObject;

    [SerializeField]private GameObject m_gameManagerObject;
    private GameManager_test m_gameManagerScript;

    private void Start()
    {
        m_gameManagerObject = GameObject.FindWithTag("GameManager");
        m_gameManagerScript = m_gameManagerObject.GetComponent<GameManager_test>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && m_gameManagerScript.Is_canSaveObjectGenerate == true) 
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_gameManagerScript.Is_saveObjectActive = true;//a
            m_gameManagerScript.Is_canSaveObjectGenerate = false;//a
            Instantiate(saveObject, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
            
        }
    }
}
