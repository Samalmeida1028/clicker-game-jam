using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    public Sprite defaultSprite1;
    public Sprite sprite2;
    public Image objectSprite;
    // Start is called before the first frame update
    void Start()
    {
        objectSprite = gameObject.GetComponentInParent<Image>();
        objectSprite.sprite = defaultSprite1;

    }

    public void setSprite1(){
        objectSprite.sprite = defaultSprite1;
    }
    public void setSprite2(){
        objectSprite.sprite = sprite2;
    }
}
