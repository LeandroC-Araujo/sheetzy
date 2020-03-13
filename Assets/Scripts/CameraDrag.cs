using UnityEngine;
 
public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    private float minFov = 15;
    private float maxFov = 90;
    private float sensitivity = 50;
    public Transform upperLeft;
    public Transform downRight;
    
    void Update()
    {
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
 
        if (!Input.GetMouseButton(0)) return;
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * -dragSpeed, pos.y * -dragSpeed, 0);

        //if (pos.x > upperLeft.position.x && pos.x < downRight.position.x && 
        //    pos.y < upperLeft.position.y && pos.y > downRight.position.y) {
            transform.Translate(move, Space.World);
        //}
    }
 
}