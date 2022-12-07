using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joybutton : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image Biggcircle;
    private Image smallcircle;
    public Vector2 dir;

    public GameObject Objekgerak;
    public GameObject Objekputar; // Yg diputer, boleh objek yg sama

    public float offset = 2f;
    public float MaxSpeed = 5f;
    public float Zaxis;
    
    // Start is called before the first frame update
    void Start()
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

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle
            (Biggcircle.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x /= ukuranX;
            pos.y /= ukuranY;

            dir = new Vector2(pos.x, pos.y);
            dir = dir.magnitude > 1 ? dir.normalized : dir;

            smallcircle.rectTransform.anchoredPosition 
                = new Vector2(dir.x * (ukuranX / offset), dir.y * (ukuranY / offset));
            //Putaran Objek
            Zaxis = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            Objekputar.transform.eulerAngles = new Vector3(0, Zaxis, 0);
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


    // Update is called once per frame
    void Update()
    {
        Objekgerak.transform.Translate(dir.x * MaxSpeed * Time.deltaTime, 0, dir.y * MaxSpeed * Time.deltaTime);
    }
}
