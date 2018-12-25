using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeContorl : MonoBehaviour
{
    public Head Head;
    public GameObject HeadGameObject;

    public List<Body> BodyList = null;
    public List<Body> BlackBodyList = null;

    public GameObject Look;

    float Speed;
    public float SpeedMin = 5f;
    public float SpeedMax = 20f;
    public int BodyMax = 20;

    public float RSpeed = 250f;

    public float FarK = 1.5f;

    public float RSpeedMax = 250f;
    public float RSpeedK = 5.5f;

    public float LookK = 1.8f;
    public float Look_Z = -9.79f;
    public float Look_YMax = 40f;
    public float Look_YMin = 27.51f;

    public BallContorl BallContorl;
    public TextContorl TextContorl;
    public GameContorl GameContorl;

    //bool Gameing = false;


    void Start()
    {

        //Gameing = true;
    }


    public Vector3 ForwordVector = Vector3.zero;


    void Update()
    {
        //RSpeed = RSpeedMax - BodyList.Count * RSpeedK;

        if (!GameContorl.Gameing)
        {
            return;
        }

        if (BallContorl.VectorForword != Vector3.zero)
        {
            ForwordVector.x = BallContorl.VectorForword.x;
            ForwordVector.z = BallContorl.VectorForword.y;
            ForwordVector.Normalize();
        }

        if (!Vector3Like(transform.forward, ForwordVector, 0.001f))
        {
            transform.forward += (ForwordVector - transform.forward) * Time.deltaTime * RSpeed;
        }
        else
        {
            transform.forward = ForwordVector;
        }

        float k = -(SpeedMax / (float)BodyMax);
        Speed = k * BodyList.Count + SpeedMax + SpeedMin;
        Speed = Mathf.Clamp(Speed,SpeedMin,SpeedMax);

        if (BodyList.Count >= BodyMax+1)
        {
            Debug.Log("GoodGame");
            Speed = 0;
        }


        HeadGameObject.transform.Translate(transform.forward * Time.deltaTime * Speed);


        Follow(BodyList[0].GameObject, HeadGameObject, FarK);

        for (int i = 1; i < BodyList.Count; i++)
        {
            Follow(BodyList[i].GameObject, BodyList[i - 1].GameObject, FarK);
        }
    }

    public void Follow(GameObject A, GameObject B, float far)
    {
        Vector3 v = B.transform.position - A.transform.position;

        float speed = Mathf.Clamp((1 / v.magnitude) * 100f, 1 / (Time.deltaTime * 10f), 1 / Time.deltaTime);

        v.Normalize();

        if (v == Vector3.zero) return;

        Vector3 tar = B.transform.position - v * far - A.transform.position;

        A.transform.Translate(tar * Time.deltaTime * speed);
    }




    public FoodContorl FoodContorl;

    private void LateUpdate()
    {

        if (!GameContorl.Gameing)
        {
            return;
        }

        LookContorl();

        OnTrigger(Head.GetBlockContorl());

        ThreeIsOK();

        TextContorl.Speed = Speed;

        TextContorl.Body = BodyList.Count;

        TextContorl.BodyMax = BodyMax;

        if (BodyList.Count>=BodyMax+1 || BodyList.Count==0)
        {
            GameContorl.GoodGame();
        }

        if (HeadGameObject.transform.position.x>50|| HeadGameObject.transform.position.x < -50 || HeadGameObject.transform.position.z > 50 || HeadGameObject.transform.position.z < -50)
        {
            GameContorl.GoodGame();
        }
    }

    void  LookContorl()
    {
        Vector3 vector;

        if (BodyList.Count > 2)
        {
            vector = (HeadGameObject.transform.position + BodyList[BodyList.Count - 2].GameObject.transform.position) / 2;
        }
        else
        {
            vector = HeadGameObject.transform.position;
        }

        vector.y = Mathf.Clamp(BodyList.Count * LookK, Look_YMin, Look_YMax);
        vector.z += Look_Z;
        if (Look.transform.position != vector)
        {
            if (!Vector3Like(Look.transform.position, vector, 0.01f))
            {
                Vector3 vector3 = vector - Look.transform.position;
                Look.transform.position += vector3 * Time.deltaTime * Speed;
            }
            else
            {
                Look.transform.position = vector;
            }

        }
        else 
        {
            Look.transform.Translate(Vector3.zero);
        }
    }

    public void LookContorlInit()
    {
        Vector3 vector= BodyList[0].GameObject.transform.position;
        vector.y = Mathf.Clamp(BodyList.Count * LookK, Look_YMin, Look_YMax);
        Look.transform.position = vector;
    }

    void OnTrigger(BlockContorl blockContorl)
    {
        Collider collider = blockContorl.OnCollider;
        blockContorl.OnCollider = null;

        if (collider == null)
        {
            return;
        }

        Block block = new Block(collider.gameObject);

        if (block.GetColor()==Color.black)
        {
            GameContorl.GoodGame();
        }


        foreach (var item in FoodContorl.FoodList)
        {
            if (item.GameObject == collider.gameObject)
            {
                Body body = Body.BodyFrom(item);
                if (!Head.BodyList.Contains(body))
                {
                    Head.AddBody(body);
                    FoodContorl.FoodList.Remove(item);
                }
                return;
            }
        }

    }

    void ThreeIsOK()
    {
        if (BodyList.Count > 2
            && BodyList[BodyList.Count - 1].GetColor() == BodyList[BodyList.Count - 2].GetColor()
            && BodyList[BodyList.Count - 1].GetColor() == BodyList[BodyList.Count - 3].GetColor()
            && Vector3Like(BodyList[BodyList.Count - 1].GameObject.transform.position, BodyList[BodyList.Count - 2].GameObject.transform.position, 3f))
        {
            for (int i = 0; i < 3; i++)
            {
                BodyList[BodyList.Count - 1].SetColor(Color.black);
                BlackBodyList.Add(BodyList[BodyList.Count - 1]);
                BodyList.RemoveAt(BodyList.Count - 1);
            }
        }
    }





    bool Vector3Like(Vector3 A, Vector3 B, float k)
    {
        if (Math.Abs(A.x - B.x) < k)
        {
            if (Math.Abs(A.y - B.y) < k)
            {
                if (Math.Abs(A.z - B.z) < k)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
