using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    public GameObject MoneyDisplay;
    public GameObject OutlawGroup;
    public bool noTime = false;
    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0) && !noTime)
        {
            this.transform.localScale =new Vector3(1.1f, 1.1f, 1);
            for(int i = 0;i<9;i++)
                OutlawGroup.transform.GetChild(i).GetComponent<GetShot>().Setup();
            MoneyDisplay.GetComponent<MoneyCounter>().Setup();
            FindObjectOfType<AudioManager>().Play("Reload");
        }
        else
            this.transform.localScale = new Vector3(1, 1, 1);
    }
}
