using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameUIRotatingCircle : MonoBehaviour
{
    // Start is called before the first frame update
    public float angle = 30;
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0.0f, 0.0f, angle), 1.0f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative();
    }

    
}
