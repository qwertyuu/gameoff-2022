using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Pictures
{
    public class PhotoCamera : MonoBehaviour
    {
        [SerializeField] private Rollfilm.Data _rollfilmData;
        [SerializeField] private Camera _cameraLens;
        [SerializeField] private RawImage _debugImage;

        private Rollfilm _rollfilm;
        private List<Picture> _pictures;

        private void Awake()
        {
            _rollfilm = new Rollfilm(_rollfilmData, _cameraLens);
            _pictures = new List<Picture>(_rollfilmData.FilmCount);
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Texture photo = _rollfilm.TakePicture();
                _debugImage.texture = photo;
                if (photo != null)
                {
                    Picture picture = CreatePicture(photo);
                    _pictures.Add(picture);
                }
            }
        }

        public Picture CreatePicture(Texture photo)
        {
            Picture picture = new Picture(photo);

            //TODO: Create a Picture, give it the texture, find on-screen CameraSubjects, make calculations (screen space ratio, other special stuff, etc)

            return picture;
        }

        public void Reinitialize()
        {
            _rollfilm.Dispose();
            _rollfilm = new Rollfilm(_rollfilmData, _cameraLens);
            _pictures.Clear();
        }

        private void OnDestroy()
        {
            _rollfilm.Dispose();
        }
    }
}