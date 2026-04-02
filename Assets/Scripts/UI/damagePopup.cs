using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class damagePopup : MonoBehaviour
{
    [Header("Stats:")]
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private float disappearTimer;
    [SerializeField] private Color textColor;

    /*public static damagePopup Create(Vector3 position, int damageAmount) {
        Transform damagePopupTransform = Instantiate(pfDamagePopup, position, Quaternion.identity);

        damagePopup damagePopup = damagePopupTransform.GetComponent<damagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }*/

    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount, Vector3 pos) {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = 0.12f;

        transform.position = pos;
    }

    private void Update() {
        float moveYspeed = 0.25f;
        transform.position += new Vector3(0, moveYspeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0)
        {
            float disappearSpeed = 0.9f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
