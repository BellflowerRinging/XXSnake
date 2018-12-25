using UnityEngine;
using System.Collections.Generic;

public class Block
{

    public GameObject GameObject { get; }
    public Animation Animation { get; }
    protected BlockContorl BlockContorl;

    protected Transform Transform;
    protected Color Color;
    protected float Size;

    public Block(string Name, Color Color, float Size, Vector3 pos, GameObject Perent)
    {
        GameObject = GameObject.Instantiate(GameObject.Find("DefaultGameObject").transform.Find("Sphere").gameObject);
        GameObject.SetActive(true);

        Init(Name,Color,Size,pos,Perent);
    }

    public Block(GameObject go)
    {
        GameObject = go;
        Init(go.name, GameObject.GetComponent<MeshRenderer>().material.color, 1, go.transform.position, go.transform.parent.gameObject);
    }

    public void Init(string Name, Color Color, float Size, Vector3 pos, GameObject Perent)
    {
        GameObject.name = Name;
        GameObject.transform.SetParent(Perent.transform);

        BlockContorl = GameObject.GetComponent<BlockContorl>();
        //Animation = Gameobject.GetComponent<Animation>();
        Transform = GameObject.GetComponent<Transform>();

        Transform.position = pos;

        SetColor(Color);
        SetSize(Size);
    }

    public Color GetColor() { return Color; }

    public void SetColor(Color color)
    {
        Color = color;
        GameObject.GetComponent<MeshRenderer>().material.color = color;
    }

    public float GetSize() { return Size; }

    public void SetSize(float size)
    {
        Size = size;
        if (Transform.parent!=null)
        {
            Transform.localScale = Vector3.one * size;
        }
    }

    public void MoveTo(Vector3 pos)
    {
        BlockContorl.MoveTo(pos);
    }


    public BlockContorl GetBlockContorl()
    {
       return BlockContorl;
    }

    public override bool Equals(object obj)
    {
        var block = obj as Block;
        return block != null &&
               EqualityComparer<GameObject>.Default.Equals(GameObject, block.GameObject);
    }

    public override int GetHashCode()
    {
        return -177122556 + EqualityComparer<GameObject>.Default.GetHashCode(GameObject);
    }
}