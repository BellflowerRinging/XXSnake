using UnityEngine;

public class Body : Block
{
    public Body(Color Color, float Size, Vector3 pos) : base("body", Color, 1f, pos, GameObject.Find("Snake"))
    {

    }

    public Body(GameObject go) : base(go)
    {
        go.name = "Body";
        Vector3 old = go.transform.position;
        go.transform.SetParent(GameObject.Find("Snake").transform);

    }

    /*private static Body BodyFrom(Block block)
    {

    }*/

    public static Body BodyFrom(Food food)
    {
        return new Body(food.GameObject);
    }


}