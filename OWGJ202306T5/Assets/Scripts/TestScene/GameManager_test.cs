using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_test : MonoBehaviour
{
    public int player_Life = 15;//プレイヤー残機

    public Vector2 save_tmp;//セーブオブジェクト位置保存用

    public bool Is_SaveObjectActive;//セーブオブジェクトは有効かどうか

    
    private void Player_Action_Reset()//リセット処理
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
        save_tmp = GameObject.FindWithTag("SaveObject").transform.position;//セーブオブジェクトの位置を取得；
        //Debug.Log("tmp.x;" + Save_tmp.x);
        //Debug.Log("tmp.y;" + Save_tmp.y);

    }
}
