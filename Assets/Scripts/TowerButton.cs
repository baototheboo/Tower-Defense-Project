using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField]
    private GameObject towerObject;
    [SerializeField]
    private Sprite dragSprite;
    [SerializeField]
    private int towerPrice;

    public Text goldText;



    public void Start()
    {
        goldText.text = towerPrice.ToString();
    }
    public int TowerPrice
    {
        get
        {
            return towerPrice;
        }
        set
        {
            towerPrice = value;
        }
    }

    public GameObject TowerObject
    {
        get
        {
            return towerObject;
        }
    }

    public Sprite DragSprite
    {
        get
        {
            return dragSprite;
        }
    }
}
