using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurretSummon : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    //Var
    private Image
        Biggcircle,
        smallcircle;
    public GameObject
        SkillRange,
        TargetRange,
        prefab;
    public Vector2 dir;
    public float currentrotate;
    public float
        offset = 4f,
        MaxSpeed = 5f,
        rotationspeed = 2f,
        Zaxis;
    private float magnitude;
    static int aktif;
    public float 
        offset_skill = 5f,
        min_offset = 0.03f;
    private void Start()
    {
        Biggcircle = GetComponent<Image>();
        smallcircle = transform.GetChild(0).GetComponent<Image>();

        SkillRange.GetComponent<Renderer>().enabled = false;
        TargetRange.GetComponent<Renderer>().enabled = false;

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
                = new Vector2(
                    dir.x * (ukuranX / offset), 
                    dir.y * (ukuranX / offset)
                    );

            Zaxis = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            magnitude = Vector2.SqrMagnitude(dir);
            if(magnitude >= min_offset)
            {
                TargetRange.GetComponent<Renderer>().enabled = true;
                TargetRange.transform.localPosition = new Vector3(0, 0, offset_skill * magnitude);
                aktif = 1;
            }
            else
            {
                TargetRange.GetComponent<Renderer>().enabled = true;
                aktif = 0;
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);

        //Enabled Object
        SkillRange.GetComponent<Renderer>().enabled = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        dir = Vector2.zero;
        smallcircle.rectTransform.anchoredPosition = Vector2.zero;
        magnitude = 0;

        //Disabled Object
        SkillRange.GetComponent<Renderer>().enabled = false;
        TargetRange.GetComponent<Renderer>().enabled = false;

        if (aktif == 1)
        {
            SummonTurret();
        }
    }

    void SummonTurret()
    {
        Vector3 pos = new Vector3();
        Quaternion rot = new Quaternion();
        pos = TargetRange.transform.position;
        Instantiate(prefab, pos, Quaternion.identity);
    }
}
