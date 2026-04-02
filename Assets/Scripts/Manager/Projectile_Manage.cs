using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Image = UnityEngine.UI.Image;

public class Projectile_Manage : MonoBehaviour
{
    [SerializeField] private List<GameObject> list;
    [SerializeField] private Sprite exist;
    [SerializeField] private Sprite empty;
    [SerializeField] private int amount;

    // Start is called before the first frame update
    void Start()
    {
        list.Add(GameObject.Find("Proj1"));
        list.Add(GameObject.Find("Proj2"));
        list.Add(GameObject.Find("Proj3"));
        list.Add(GameObject.Find("Proj4"));

        amount = 0;
    }

    public void updateFrame(int newAmount)
    {
        if (newAmount > amount && newAmount <= 4)
        {
            list[newAmount - 1].GetComponent<Image>().sprite = exist;
        }
        else if (newAmount < amount && newAmount >= 0)
        {
            list[newAmount].GetComponent<Image>().sprite = empty;
        }

        if (0 <= newAmount && newAmount <= 4)
        {
            amount = newAmount;
        }
    }
}
