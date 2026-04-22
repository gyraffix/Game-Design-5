using UnityEngine;

public class FollowDrawing : MonoBehaviour
{
    private Vector3 offset;
    public bool drawingDone;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        offset = transform.position;

    }

    public void SetCamera(Vector3 position)
    {
        transform.position = offset + position;
    }


    // Update is called once per frame
    void Update()
    {
        if (!drawingDone)
        transform.position = offset + Drawing.instance.currentPosition;
    }
}
