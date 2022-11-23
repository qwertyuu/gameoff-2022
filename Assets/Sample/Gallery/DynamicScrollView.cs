using UnityEngine;
using UnityEngine.UI;
using Game.Pictures;
using System.Collections.Generic;

public class DynamicScrollView : MonoBehaviour
{
    [SerializeField]
    private Transform scrollViewContent;

    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private List<Picture> _pictures;
    
    [SerializeField]
    private PhotoCamera photoCamera;



    // Start is called before the first frame update
    void Start()
    {
        photoCamera = GameObject.FindGameObjectsWithTag("PhotoCamera")[0].GetComponent<PhotoCamera>();

        _pictures = photoCamera.Photos;
        // for (int i = 0; i<200; i++){
        //     var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);

        //     // set the pixel values
        //     texture.SetPixel(0, 0, Color.black);
        //     texture.SetPixel(1, 0, Color.black);
        //     texture.SetPixel(0, 1, Color.black);
        //     texture.SetPixel(1, 1, Color.black);
        
        //     // Apply all SetPixel calls
        //     texture.Apply();

        //     _pictures.Add(new Picture(texture));
        // }



        foreach(Picture picture in _pictures){
            GameObject newPicture = Instantiate(prefab, scrollViewContent);

            if (newPicture.TryGetComponent<ScrollViewItem>(out ScrollViewItem item)){
                item.ChangeImage(picture.Texture);
            }
        }
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
