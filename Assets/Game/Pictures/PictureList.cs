using UnityEngine;
using System.Collections.Generic;

namespace Game.Pictures
{
    [CreateAssetMenu(fileName = "PictureList", menuName = "PictureList")]
    public class PictureList : ScriptableObject
    {
        public List<Picture> pictures;

        public void Clear(int limit) 
        {
            pictures = new List<Picture>(limit);
        }
    }
}
