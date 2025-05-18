using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class PizzaIcon : MonoBehaviour
{
    // Start is called before the first frame update
        void Start()
        {
            // Büyüt (1x -> 1.5x)
            transform.DOScale(0.0045f, 1f)
                .SetLoops(-1, LoopType.Yoyo)  // Sonsuz büyüt-küçült döngüsü
                .SetEase(Ease.InOutSine);     // Yumuşak geçiş
        }
    
}
