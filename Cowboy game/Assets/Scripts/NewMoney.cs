using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMoney : MonoBehaviour
{
    public Texture2D texture;
    public GameObject OutlawGroup;
    private GameObject Outlaw;
    //private Vector2 hotSpot;
    // Start is called before the first frame update
    void Start()
    {
        //hotSpot = new Vector2(texture.width / 2, texture.height / 2);
        //Cursor.SetCursor(texture,hotSpot , CursorMode.Auto);

        Dictionary<string, int> outlaws = new Dictionary<string, int>()
        {
            { "Elias",250 },{ "Clint",220 },{ "Gil",100 },{ "Huck",390 },{ "Cody",90 },{ "Amos",570 },{ "Billy",660 },{ "Carson",70 },{ "Jed",340 }
        };
        for (int i = 0; i < 9; i++)
        {
            Outlaw = OutlawGroup.transform.GetChild(i).gameObject;
            Outlaw.GetComponent<GetShot>().bounty = outlaws[Outlaw.name];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
