using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingEye : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] float time;
    float timer = 0;
    int id = 0;
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time) 
        {
            id++;
            if (id >= sprites.Length) id = 0;
            image.sprite = sprites[id];
            timer = 0;
        }
    }
}
