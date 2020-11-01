using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvCrystal : MonoBehaviour
{
    [SerializeField] Image crystalImage;
    [SerializeField] GameObject pickupStar;
    [SerializeField] AudioSource invCollect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameObject star = Instantiate(pickupStar);
            star.transform.position = transform.position;
            crystalImage.fillAmount += .3f;
            invCollect.Play();
            this.gameObject.SetActive(false);
        }
    }
}
