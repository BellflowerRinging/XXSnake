using UnityEngine;
using UnityEngine.UI;

public class TextContorl : MonoBehaviour
{
    public float Score = 0f;
    public float Speed = 0f;
    public int Body = 0;
    public int BodyMax = 0;


    private void Update()
    {
        GetComponent<Text>().text = "Speed:"+Speed+ " Body:" + Body+"/"+ BodyMax;
    }
}

