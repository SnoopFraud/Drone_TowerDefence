using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnalogFlight : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    //var
    public GameObject Objekgerak;
    private Image 
        Biggcircle,
        smallcircle;
    public Vector2 dir;
    public float currentrotate;
    public float 
        offset = 2f,
        MaxSpeed = 5f,
        rotationspeed = 1f,
        Zaxis;

    private void Start()
    {
        Biggcircle = GetComponent<Image>();
        smallcircle = transform.GetChild(0).GetComponent<Image>();
        dir = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = Vector2.zero;
        float ukuranX = Biggcircle.rectTransform.sizeDelta.x;
        float ukuranY = Biggcircle.rectTransform.sizeDelta.y;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (Biggcircle.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x /= ukuranX;
            pos.y /= ukuranY;

            dir = new Vector2(pos.x, pos.y);
            dir = dir.magnitude > 1 ? dir.normalized : dir;

            smallcircle.rectTransform.anchoredPosition
                = new Vector2(dir.x * (ukuranX / offset), dir.y * (ukuranY / offset));
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        dir = Vector2.zero;
        smallcircle.rectTransform.anchoredPosition = Vector2.zero;
    }

    private void Update()
    {
        Zaxis = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
    }

    private void FixedUpdate()
    {
        ForwardMovement();
        RotatePlayer();
        //BackwardMovement();
    }

    void ForwardMovement()
    {
        Objekgerak.transform.Translate(dir.x * MaxSpeed * Time.deltaTime, 0, dir.y * MaxSpeed * Time.deltaTime);
    }
    void RotatePlayer()
    {
        Objekgerak.transform.Rotate(0, Zaxis * Time.deltaTime * rotationspeed, 0);
    }
}
