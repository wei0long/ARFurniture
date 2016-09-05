using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Move : MonoBehaviour
{

    public Transform target;
    float distance = 30f;
    float xSpeed = 150f;
    float ySpeed = 150f;
    float yMinLimit = -180f;
    float yMaxLimit = 180f;
    float x = 0f;
    float y = 0f;
    Vector2 oldPosition1;
    Vector2 oldPosition2;
    Vector2 e;
    private bool flag_Roable = true;//自动旋转标志

    private System.DateTime oldTime;
    private System.DateTime nowTime;

    // Use this for initialization
    void Start()
    {
        transform.eulerAngles = new Vector3(0, -90, 0);//所挂模型的初始旋转角度
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;//冻结旋转
        }
        oldTime = System.DateTime.Now;

    }

    // Update is called once per frame
    void Update()
    {
        nowTime = System.DateTime.Now;
        System.TimeSpan ts1 = new System.TimeSpan(oldTime.Ticks);//获取表示当前TimeSpan结构的刻度数
        System.TimeSpan ts2 = new System.TimeSpan(nowTime.Ticks);

        System.TimeSpan ts = ts2.Subtract(ts1).Duration();
        if (ts.Seconds > 8 && !Input.anyKey)
        {

            flag_Roable = true;
            oldTime = System.DateTime.Now;

        }
        if (Input.anyKey)
        {

            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    x = Input.GetAxis("Mouse X") * xSpeed;
                    y = Input.GetAxis("Mouse Y") * ySpeed;
                    transform.Rotate(Vector3.up * -x * Time.deltaTime, Space.World);
                    transform.Rotate(Vector3.right * y * Time.deltaTime, Space.World);
                    e = Input.GetTouch(0).position;
                    // Vector3 body =transform.localScale;
                    // transform.Translate(new Vector3(-e.x/10,e.y/10,body.z),Space.World);
                }
            }









            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
                {
                    Vector2 tempPosition1 = Input.GetTouch(0).position;
                    Vector2 tempPosition2 = Input.GetTouch(1).position;
                    if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                    {
                        float oldScale = transform.localScale.x;
                        float newScale = oldScale * 1.025f;
                        transform.localScale = new Vector3(newScale, newScale, newScale);
                    }
                    else
                    {
                        float oldScale = transform.localScale.x;
                        float newScale = oldScale / 1.025f;
                        transform.localScale = new Vector3(newScale, newScale, newScale);
                    }
                    oldPosition1 = tempPosition1;
                    oldPosition2 = tempPosition2;
                }
            }
        }

    }

    bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {

        var leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        var leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        if (leng1 < leng2)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    void OnGUI()
    {

        GUIStyle bb = new GUIStyle();
        bb.normal.background = null;    //这是设置背景填充的
        bb.normal.textColor = new Color(1, 0, 0);   //设置字体颜色的
        bb.fontSize = 35;       //当然，这是字体颜色

        string s = string.Format("X={0}\ny=\n{1}",
            e.x, e.y);
        GUI.TextArea(new Rect(0, 0, Screen.width / 5, Screen.height / 5), s, bb);

    }

}



