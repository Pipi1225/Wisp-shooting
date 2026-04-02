using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform pfDamagePopup;
    
    private void Start()
    {
        Transform damagePopupTransform = Instantiate(pfDamagePopup, Vector3.zero, Quaternion.identity);
        damagePopup damagePopup = damagePopupTransform.GetComponent<damagePopup>();

       // damagePopup.Setup(300);
    }
}
