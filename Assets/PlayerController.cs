using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject objGround;
    public GameObject txtInfo;

    Vector2 startPos;
    Rigidbody2D rb;

    // 原則
    float frictionForce = 0.999f;

    bool gameEnd;

    int touchCnt = 999;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( gameEnd )
        {
            return;
        }

        // マウスボタン
        if ( Input.GetMouseButtonDown(0) ) 
        {
            // 開始地点を取得
            // マウスが押された場所
            startPos = Input.mousePosition;
        }
        else if ( Input.GetMouseButtonUp(0) )
        {
            if ( 0 < touchCnt )
            { 
                // 終了地点を取得
                // マウスを離した場所
                Vector2 endPos = Input.mousePosition;

                // 加算する値
                Vector2 addForce = endPos - startPos;

                rb.AddForce(addForce);

                touchCnt--;
            }
        }

        // 摩擦をつけて少しずつ減速
        rb.velocity *= frictionForce;

        // テキストの更新
        // 現在地の取得
        float sx = transform.position.x;
        float sy = transform.position.y;

        // Groundの一番右の場所
        float ex = objGround.transform.localScale.x / 2;
        // Groundの一番右から、現在地を減算し残り距離を取得
        float dx = ex - sx;

        txtInfo.GetComponent<Text>().text = "残り " + dx.ToString("N1");

        // ゴール判定
        // 成功時
        if ( 0 < dx && dx < 1 )
        {
            // 停止した状態
            if ( 0.01f > rb.velocity.x ) 
            {
                txtInfo.GetComponent<Text>().text = "Success!";

                // 成功時はカラーを反転させる
                txtInfo.GetComponent<Text>().color = Color.black;                    // 文字
                GetComponent<SpriteRenderer>().color = Color.black;                  // Player
                objGround.GetComponent<SpriteRenderer>().color = Color.black;        // 地面
                 
                Camera.main.GetComponent<Camera>().backgroundColor = Color.white;    // 背景

                gameEnd = true;
            }
        }

        // 失敗時
        if ( sy < -10 )
        {
            txtInfo.GetComponent<Text>().text = "Fail:(";
            gameEnd = true;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
