using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseButtons : MonoBehaviour
{
    public bool close;
    public bool levelUp;

    private void OnMouseDown()
    {
        if (!close && !levelUp)
            return;
        if (levelUp)
        {
            GameManager.instance.GetBase().LevelUp();
            transform.parent.gameObject.SetActive(false);
            return;
        }
        if (close)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
    }
}
