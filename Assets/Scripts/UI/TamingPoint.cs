using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TamingPoint : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TamingArea m_area;
    [SerializeField] private Sprite[] m_eyeFrames;
    public void OnPointerClick(PointerEventData eventData)
    {
        m_area.ShowNewEye(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStage(int i)
    {
        if (m_eyeFrames.Length <= i) return;
        GetComponent<Image>().sprite = m_eyeFrames[i];
    }
}
