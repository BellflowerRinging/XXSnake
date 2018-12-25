using UnityEngine;
using System.Collections.Generic;

public class Head : Block
{
    public List<Body> BodyList { get; }


    public Head(string Name, Color Color, float Size, Vector3 pos, GameObject Perent) : base("Head", Color, 1.5f, pos, GameObject.Find("Snake"))
    {
        BodyList = new List<Body>();
    }

    public Head(int bodyCount) : base("Head", Color.white, 1, Vector3.up*1.5f+Vector3.back*45f, GameObject.Find("Snake"))
    {


        BodyList = new List<Body>();
        Color lastColor=Color.black;

        for (int i = 0; i < bodyCount; i++)
        {
            RandomColor randomColor = new RandomColor();
            if (i>0)
            {
                randomColor.SetColorPercent(lastColor,-20f);
            }
            lastColor = randomColor.GetRandomColor();
            AddBody(new Body(lastColor, 1, Transform.position-Transform.forward*1.5f*(i+1)));
        }
    }

    public void AddBody(Body body)
    {
        BodyList.Add(body);
    }



}