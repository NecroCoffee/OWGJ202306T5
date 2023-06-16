using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_test : MonoBehaviour
{
    [Header("オブジェクト用変数")]
    [SerializeField] private GameObject life_TextObject;//lifetextオブジェクト保存用
    private TextMeshProUGUI life_Text;
    [SerializeField] private GameObject player_Prefab;
    [SerializeField] private GameObject save_Prefab;

    [Header("管理用変数")]
    public int player_Life = 15;//プレイヤー残機

    [SerializeField]private Vector2 save_tmp;//セーブオブジェクト座標保存用

    public bool Is_saveObjectActive=false;//セーブオブジェクトは有効かどうか

    private Vector2 startPointPos;//スタート時プレイヤー生成位置











    
    public Vector2 currentCheckPointPos;//チェックポイント座標保存用

    public bool Is_canSaveObjectGenerate;//セーブオブジェクト生成可否判定

    private void Player_Generate_Start()//開始時プレイヤー生成
    {
        Instantiate(player_Prefab, startPointPos, Quaternion.identity);
        
    }

    private void Player_Action_Reset()//リセット処理
    {

        GameObject.FindWithTag("Player").transform.position = new Vector3(save_tmp.x, save_tmp.y+0.25f);
        Is_canSaveObjectGenerate = true;
        Destroy(save_Prefab);
        
    }

    private void Find_Pos_CurrentSaveObject()
    {
        if (save_Prefab == null)
        {
            save_Prefab = GameObject.FindWithTag("SaveObject");
        }
        else if (save_Prefab != null)
        {
            save_tmp = GameObject.FindWithTag("SaveObject").transform.position;//セーブオブジェクトの位置を取得；
        }
    }

    private void Return_To_Point()//Pointタグオブジェクトに戻る
    {
        if (Input.GetKeyDown("r"))
        {
            if (Is_saveObjectActive == true)
            {
                GameObject.FindWithTag("Player").transform.position = new Vector2(currentCheckPointPos.x, currentCheckPointPos.y);
                Debug.Log("Return to checkpoint!");
            }
            else if(Is_saveObjectActive == false)
            {
                GameObject.FindWithTag("Player").transform.position = new Vector2(startPointPos.x,startPointPos.y);
                Debug.Log("CheckPoint isnt active. Return to startpoint!");
            }
        }
    }

    private void Awake()
    {
        startPointPos = GameObject.FindWithTag("StartPoint").transform.position;//スタート位置取得
        life_Text = life_TextObject.GetComponent<TextMeshProUGUI>();
        life_Text.text = player_Life.ToString();
    }

    private void Start()
    {
        Player_Generate_Start();
    }
    



    private void Update()
    {

        Find_Pos_CurrentSaveObject();
        //Debug.Log("curSavePos.x" + save_tmp.x);
        //Debug.Log("curSavePos.y" + save_tmp.y);
        
        
        if (Input.GetMouseButtonDown(1))
        {
            Player_Action_Reset();
        }

    }
}
