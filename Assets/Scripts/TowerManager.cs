using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : MonoBehaviour
{
    public TowerButton towerBtnPressed { get; set; }
    private SpriteRenderer spriteRenderer;
    private Collider2D buildTile;
    public List<GameObject> TowerList = new List<GameObject>();
    public List<Collider2D> BuildList = new List<Collider2D>();
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            //Get the mouse position on the screen and send a raycast into the game world from that position.
            Vector2 worldPointPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPointPosition, Vector2.zero);
            //If something was hit, the RaycastHit2D.collider will not be null.

            if (hit)
            {
                if (hit.collider.tag == "BuildPlace")
                {

                    buildTile = hit.collider;
                    buildTile.tag = "BuildPlaceFull";
                    RegisterBuildPlace(buildTile);
                    PlaceTower(hit);
                }
            }

        }

        // when sprite is enabled move the sprite where our mouse position is
        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
    }

    public void RegisterBuildPlace(Collider2D other)
    {
        // Register build list to avoid put more towers in that place
        BuildList.Add(other);
    }

    public void RenameTagsBuildPlace()
    {
        // loop through building list and tag them as BuildSite
        foreach (Collider2D buildTag in BuildList)
        {
            buildTag.tag = "BuildPlace";
        }
        BuildList.Clear();
    }

    public void RegisterTower(GameObject tower)
    {
        //add tower to list
        TowerList.Add(tower);
    }

    public void DestroyAllTowers()
    {
        //loop through all the towers in list and destroy them
        foreach (GameObject tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }

    public void DestroyTower(GameObject tower)
    {
        //add tower to list
        Destroy(tower);
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            // instantiate game object tower
            GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            //register that tower in our tower list
            RegisterTower(newTower);
            //call the buy tower function
            BuyTower(towerBtnPressed.TowerPrice);
            //disable the sprite of tower
            DisableDragSprite();
        }
    }

    public void BuyTower(int price)
    {
        //reduce from our current gold after buying tower
        GameManager.instance.ReduceGold(price);
        AudioManager.instance.PlaySFX(8);
    }

    public void SelectedTower(TowerButton towerBtn)
    {
        //check that tower price is less than our current gold
        if (towerBtn.TowerPrice <= GameManager.instance.currentGold)
        {
            //when we clicked on tower UI button active the sprite of that tower
            towerBtnPressed = towerBtn;
            EnableDragSprite(towerBtn.DragSprite);

        }
    }

    private void FollowMouse()
    {
        //get the position of mouse
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnableDragSprite(Sprite sprite)
    {
        //enable the sprite of tower
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void DisableDragSprite()
    {
        //disable the sprite of tower
        spriteRenderer.enabled = false;
        towerBtnPressed = null;
    }
}
