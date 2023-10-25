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

    // ����
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

        // �}�E�X�{�^��
        if ( Input.GetMouseButtonDown(0) ) 
        {
            // �J�n�n�_���擾
            // �}�E�X�������ꂽ�ꏊ
            startPos = Input.mousePosition;
        }
        else if ( Input.GetMouseButtonUp(0) )
        {
            if ( 0 < touchCnt )
            { 
                // �I���n�_���擾
                // �}�E�X�𗣂����ꏊ
                Vector2 endPos = Input.mousePosition;

                // ���Z����l
                Vector2 addForce = endPos - startPos;

                rb.AddForce(addForce);

                touchCnt--;
            }
        }

        // ���C�����ď���������
        rb.velocity *= frictionForce;

        // �e�L�X�g�̍X�V
        // ���ݒn�̎擾
        float sx = transform.position.x;
        float sy = transform.position.y;

        // Ground�̈�ԉE�̏ꏊ
        float ex = objGround.transform.localScale.x / 2;
        // Ground�̈�ԉE����A���ݒn�����Z���c�苗�����擾
        float dx = ex - sx;

        txtInfo.GetComponent<Text>().text = "�c�� " + dx.ToString("N1");

        // �S�[������
        // ������
        if ( 0 < dx && dx < 1 )
        {
            // ��~�������
            if ( 0.01f > rb.velocity.x ) 
            {
                txtInfo.GetComponent<Text>().text = "Success!";

                // �������̓J���[�𔽓]������
                txtInfo.GetComponent<Text>().color = Color.black;                    // ����
                GetComponent<SpriteRenderer>().color = Color.black;                  // Player
                objGround.GetComponent<SpriteRenderer>().color = Color.black;        // �n��
                 
                Camera.main.GetComponent<Camera>().backgroundColor = Color.white;    // �w�i

                gameEnd = true;
            }
        }

        // ���s��
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
