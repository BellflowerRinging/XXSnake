using UnityEngine;

public class Food : Block
{
    public Food(Color Color, float Size, Vector3 pos) : base("Food", Color, Size, pos, GameObject.Find("Food"))
    {

    }

}