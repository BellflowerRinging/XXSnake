using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallContorl : MonoBehaviour
{

    public TextContorl TextContorl;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject Ball;

    public Vector3 VectorForword;

    public float BallVectorPosK;

    public void OnMouseDrag()
    {
        Vector3 vector = Input.mousePosition;
        Vector3 vectorF = Input.mousePosition - transform.position;

        if (Input.touchCount>1)
        {
            vector = Input.GetTouch(0).position;
            vectorF = vector - transform.position;
        }

        //BallClampPos(vector,20f);
        if (Mathf.Abs( vectorF.x) > BallVectorPosK || Mathf.Abs(vectorF.y) > BallVectorPosK)
        {
            Ball.transform.position = transform.position + (vector - transform.position).normalized * BallVectorPosK;
        }
        else
        {
            Ball.transform.position = transform.position + vectorF;
        }

        VectorForword = vector - transform.position;
    }

    void BallClampPos(Vector3 vector, float k)
    {
        vector.x = Mathf.Clamp(vector.x, transform.position.x - k, transform.position.x + k);
        vector.y = Mathf.Clamp(vector.y, transform.position.y - k, transform.position.y + k);
        Ball.transform.position = vector;
    }



    public void OnMouseUpAsButton()
    {
        Ball.transform.position = transform.position;
        VectorForword = Vector3.zero;
    }

    public SnakeContorl SnakeContorl;

    public void ChangeTwoBall()
    {
        if (SnakeContorl.Head.BodyList.Count>1)
        {
            Color color = SnakeContorl.Head.BodyList[0].GetColor();
            SnakeContorl.Head.BodyList[0].SetColor(SnakeContorl.Head.BodyList[SnakeContorl.Head.BodyList.Count - 1].GetColor());
            SnakeContorl.Head.BodyList[SnakeContorl.Head.BodyList.Count - 1].SetColor(color);
        }
    }
}
