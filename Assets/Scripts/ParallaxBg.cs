using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBg : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    private Transform camTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;


    private void Start()
    {
        camTransform = Camera.main.transform;
        lastCameraPosition = camTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = camTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y, 0);
         lastCameraPosition = camTransform.position;

        if(Mathf.Abs(camTransform.position.x - transform.position.x )>= textureUnitSizeX)
        {
            Debug.Log("resetting");
            float offsetPositionX = (camTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(camTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
}
