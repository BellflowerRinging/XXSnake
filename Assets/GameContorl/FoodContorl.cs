
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodContorl : MonoBehaviour
{
    public float TimeOneStar=2f;
    public int FoodMax=5;


    void Start()
    {

    }

    float Times = 0f;
    public List<Food> FoodList = null;

    public GameContorl GameContorl;


    void Update()
    {
        if (!GameContorl.Gameing)
        {
            return;
        }

        float k = -(TimeOneStar/(float)FoodMax);
        float TimeOne = k * FoodList.Count + TimeOneStar;

        if (TimeOne!=0 && (Times+=Time.deltaTime)>= TimeOne)
        {
            Times = 0;
            if (FoodList.Count< FoodMax)
            {
                FoodList.Add(new Food(new RandomColor().GetRandomColor(), 1, RandomPosition()));
            }
        }
    }

    struct ColorIndex
    {
        const int magenta = 0;
        const int blue = 1;
        const int white = 2;
        const int yellow = 3;
        const int green = 4;
    }

    public Floorborder border;

    public Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(border.left, border.right), 1.5f, Random.Range(border.buttom, border.top));
    }

}

[System.Serializable]
public class Floorborder
{
    public float left;
    public float top;
    public float right;
    public float buttom;
}
 