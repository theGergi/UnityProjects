using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Texture2D texture;
    public GameObject OutlawGroup;
    private GameObject Outlaw;
    public int NumOutlaws = 10;
    public int[] x;
    void Start()
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);

        NumOutlaws = OutlawGroup.transform.childCount;
        int[] z = MoneyDivide();
        for(int i = 0; i< NumOutlaws;i++)
        {
           // Outlaw = OutlawGroup.transform.GetChild(i).gameObject;
            //Outlaw.GetComponent<GetShot>().bounty = z[i];

            OutlawGroup.transform.GetChild(i).gameObject.GetComponent<GetShot>().bounty = z[i];
            Debug.Log(z[i].ToString());
        }


    }

    private int[] MoneyDivide()
    {

        int sum = 1000;
        for (int i = 0; i < NumOutlaws/2-1; i++)
        {
            x[i] = Random.Range(1, (int)(sum * 0.5f));
            sum -= x[i];
        }
        x[NumOutlaws / 2 - 1] = sum;
        for (int i = NumOutlaws/2; i < NumOutlaws; i++)
            x[i] = Random.Range(1, 400);
        return x;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
