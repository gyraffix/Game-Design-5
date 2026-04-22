using UnityEngine;

public class Drawing : MonoBehaviour
{
    [SerializeField] private float drawingSpeed;
    public Vector3 currentPosition;
    [SerializeField] private Vector3 currentDirection;
    public static Drawing instance;
    private int currentVertex = 1;
    [SerializeField] private FollowDrawing canvas;
    [SerializeField] private FollowDrawing camera;
    [SerializeField] private KeyCode stopKey;
    private bool drawingDone = false;
    private bool cameraAdjusted;

    private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, new Vector3(0, 0, -0.1f));
        currentPosition = new Vector3(0, 0, -0.1f);
        currentDirection = new Vector3(0, 0, 0);
    }

    private void MoveDrawing()
    {
        currentPosition += currentDirection * drawingSpeed * Time.deltaTime;
        lineRenderer.SetPosition(currentVertex, currentPosition);
    }

    
    private void ChangeDirection()
    {
        if (Input.mousePositionDelta.magnitude > 0.3f)
        {
            currentDirection = Input.mousePositionDelta.normalized;
            currentVertex++;
            lineRenderer.positionCount++;
        }
           
    }

    private void Simplify()
    {
        lineRenderer.Simplify(0.01f);
        currentVertex = lineRenderer.positionCount - 1;
    }

    private void AdjustCamera()
    {
        Bounds bounds = lineRenderer.bounds;

        camera.drawingDone = true;
        camera.SetCamera(bounds.center);

        canvas.drawingDone = true;
        canvas.SetCamera(bounds.center);
        
        if (bounds.extents.magnitude > 5)
        {
            camera.gameObject.GetComponent<Camera>().orthographicSize = bounds.extents.magnitude;
            canvas.gameObject.transform.localScale = new Vector3(bounds.extents.magnitude * 50, bounds.extents.magnitude * 50, bounds.extents.magnitude);
            lineRenderer.startWidth = bounds.extents.magnitude / 50; 
            lineRenderer.endWidth = bounds.extents.magnitude / 50;
        }
          

        cameraAdjusted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (drawingDone)
        {
            if (!cameraAdjusted)
            {
                AdjustCamera();
            }

            return;
        }
        
        MoveDrawing();
        ChangeDirection();
        Simplify();

        if (Input.GetKeyDown(stopKey)) drawingDone = true;
    }
}
