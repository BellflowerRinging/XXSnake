using UnityEngine;

public class BlockContorl : MonoBehaviour
{
    Vector3 tar;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (tar != transform.position)
        {
            transform.Translate(tar);
        }
    }

    public void MoveTo(Vector3 pos)
    {
        tar = pos;
    }

    public Collider OnCollider;

    void OnTriggerEnter(Collider other)
    {
        OnCollider = other;
    }
}