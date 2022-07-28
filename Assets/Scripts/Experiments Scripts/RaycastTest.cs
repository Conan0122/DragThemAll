// using IDragHandler interface
// or using raycast

using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastTest : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Camera mainCamera;
    float zAxis = 0;
    Vector3 clickOffset = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera.GetComponent<Physics2DRaycaster>() == null)
            mainCamera.gameObject.AddComponent<Physics2DRaycaster>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        zAxis = transform.position.z;
        clickOffset = transform.position - mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, zAxis)) + new Vector3(0, 3, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y, zAxis);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Use Offset To Prevent Sprite from Jumping to where the finger is
        Vector3 tempVec = mainCamera.ScreenToWorldPoint(eventData.position) + clickOffset;
        tempVec.z = zAxis; //Make sure that the z zxis never change


        transform.position = tempVec;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }

    //Add Event System to the Camera
    void addEventSystem()
    {
        GameObject eventSystem = new GameObject("EventSystem");
        eventSystem.AddComponent<EventSystem>();
        eventSystem.AddComponent<StandaloneInputModule>();
    }
}