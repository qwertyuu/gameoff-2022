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
    private GameObject popup;

    [SerializeField]
    private Text text;

    [SerializeField]
    private PictureList _pictureList;

    private float score = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Picture picture in _pictureList.pictures){
            GameObject newPicture = Instantiate(prefab, scrollViewContent);

            score += picture.Score();

            if (newPicture.TryGetComponent<ScrollViewItem>(out ScrollViewItem item)){
                item.ChangeImage(picture.Texture);
            }
        }
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CalculateScore(){
        text.text = "Congratulations ! \n You did " + score.ToString() + " points";
        popup.SetActive(true);
    }
}
