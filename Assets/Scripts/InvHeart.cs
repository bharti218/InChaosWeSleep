using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvHeart : MonoBehaviour
{
    [SerializeField] GameObject pickUpHeart;
    [SerializeField] AudioSource invCollect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameObject heart = Instantiate(pickUpHeart);
            heart.transform.position = transform.position;
            collision.gameObject.GetComponent<PlayerController>().SetFullHealth();
            invCollect.Play();
            this.gameObject.SetActive(false);
        }
    }
}
