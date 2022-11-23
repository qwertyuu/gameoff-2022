using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class cellsSizing : MonoBehaviour{
    [SerializeField]
	private GridLayoutGroup gridLayoutGroup;

    private Boolean notSet = true;

    // Update is called once per frame
    void Update()
    {
        float t = gridLayoutGroup.GetComponent<RectTransform>().rect.width;

        if (notSet && t != 0){
            notSet = false;
            float widthMinusSpacing = (t - (gridLayoutGroup.spacing.x * (gridLayoutGroup.constraintCount + 1)));
            float size = widthMinusSpacing / gridLayoutGroup.constraintCount;
            gridLayoutGroup.cellSize = new Vector2 (size, size);

        }
    }
}
    
    