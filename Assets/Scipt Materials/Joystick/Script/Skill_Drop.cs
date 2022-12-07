using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill_Drop : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image BgImage; //Lingkaran Besar
    private Image JoyImage; //Lingkaran Kecil

    public GameObject SkillRange;
    public GameObject TargetSkill; //Bisa maju mundur

    public Vector2 ArahSkill_1;
    public float offset = 2f;
    public float offset_skill = 5f;
    public float min_offset = 0.03f;
    public float magnitude;
    public float ZAxisskill_1;
    static int aktif;
    public Color MyColor;

    public Light lampu;

    public GameObject prefab;
    public GameObject probe;

    // Start is called before the first frame update
    void Start()
    {
        BgImage = GetComponent<Image>();
        JoyImage = transform.GetChild(0).GetComponent<Image>();
        ArahSkill_1 = Vector2.zero;
        SkillRange.GetComponent<Renderer>().enabled = false;
        TargetSkill.GetComponent<Renderer>().enabled = false;
        lampu.enabled = false;
        probe.GetComponent<Renderer>().enabled = false;
        probe.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrag(PointerEventData eventDataskill_1)
    {
        Vector2 posskill_1 = Vector2.zero;
        float bgimagesizeX = BgImage.rectTransform.sizeDelta.x;
        float bgimagesizeY = BgImage.rectTransform.sizeDelta.y;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (BgImage.rectTransform, eventDataskill_1.position, eventDataskill_1.pressEventCamera, out posskill_1))
        {
            posskill_1.x /= bgimagesizeX;
            posskill_1.y /= bgimagesizeY;

            ArahSkill_1 = new Vector2(posskill_1.x, posskill_1.y);
            ArahSkill_1 = ArahSkill_1.magnitude > 1 ? ArahSkill_1.normalized : ArahSkill_1;

            JoyImage.rectTransform.anchoredPosition
                = new Vector2(ArahSkill_1.x * (bgimagesizeX / offset),
                              ArahSkill_1.y * (bgimagesizeX / offset)); //Pake offset joystick (Joyimage)

            ZAxisskill_1 = Mathf.Atan2(ArahSkill_1.x, ArahSkill_1.y) * Mathf.Rad2Deg;
            SkillRange.transform.eulerAngles = new Vector3(0, ZAxisskill_1, 0);
            magnitude = Vector2.SqrMagnitude(ArahSkill_1);

            if (magnitude >= min_offset)
            {
                TargetSkill.GetComponent<Renderer>().enabled = true;
                TargetSkill.transform.localPosition = new Vector3(0, 0, offset_skill * magnitude);
                //Pakai offskill untuk jarak lingkaran target skill
                aktif = 1;
            }
            else
            {
                TargetSkill.GetComponent<Renderer>().enabled = true;
                aktif = 0;
            }
            lampu.transform.LookAt(TargetSkill.transform.position);
            probe.transform.position = TargetSkill.transform.position;
        }

    }
    public void OnPointerDown(PointerEventData eventDataskill_1)
    {
        //Menyalakan skill aimer
        SkillRange.GetComponent<Renderer>().enabled = true;
        OnDrag(eventDataskill_1);
        lampu.enabled = true;
        probe.GetComponent<BoxCollider>().enabled = true;
    }
    public void OnPointerUp(PointerEventData eventDataskill_1)
    {
        probe.GetComponent<BoxCollider>().enabled = false;
        lampu.enabled = false;
        SkillRange.GetComponent<Renderer>().enabled = false;
        TargetSkill.GetComponent<Renderer>().enabled = false;
        ArahSkill_1 = Vector2.zero;
        JoyImage.rectTransform.anchoredPosition = Vector2.zero;
        magnitude = 0;
        if (aktif == 1)
        {
            //SkillPertama();
            Vector3 pos = new Vector3();
            Quaternion rot = new Quaternion();
            pos = TargetSkill.transform.position;
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }

    void SkillPertama()
    {
        Vector3 posisiSkill;
        posisiSkill = TargetSkill.transform.position;
        //GameObject bola = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //bola.transform.position = posisiSkill;
        Color warnarand = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        //bola.GetComponent<Renderer>().material.color = warnarand;

    }
}
