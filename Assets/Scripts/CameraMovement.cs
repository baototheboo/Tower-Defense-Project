using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    private Camera cam;
    private Vector3 dragOrigin;
    

    [SerializeField]
    private SpriteRenderer mapRenderer;
    private float mapMinX, mapMinY, mapMaxX, mapMaxY;

    // Update is called once per frame
    void Update()
    {
        PanCamera();
    }

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 different = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            print("origin " + dragOrigin + " new position " + cam.ScreenToWorldPoint(Input.mousePosition) + " = different " + different);
            cam.transform.position = ClampCamera(cam.transform.position + different);
            //cam.transform.position += different;

        }
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float cameraHeight = cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;

        float minX = mapMinX + cameraWidth;
        float maxX = mapMaxX - cameraWidth;

        float minY = mapMinY + cameraHeight;
        float maxY = mapMaxY - cameraHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
