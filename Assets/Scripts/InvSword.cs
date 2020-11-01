using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvSword : MonoBehaviour
{
    [SerializeField] GameObject SwordButton;
    [SerializeField] GameObject pickupStar;
    [SerializeField] AudioSource invCollect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameObject star = Instantiate(pickupStar);
            star.transform.position = transform.position;
            SwordButton.SetActive(true);
            invCollect.Play();
            this.gameObject.SetActive(false);
        }
    }
}