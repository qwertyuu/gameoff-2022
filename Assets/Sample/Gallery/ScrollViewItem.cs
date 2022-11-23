using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Pictures;


public class ScrollViewItem : MonoBehaviour
{

    [SerializeField]
    private RawImage rawImage;

    public void ChangeImage(Texture texture){
        rawImage.texture = texture;
    }
}
