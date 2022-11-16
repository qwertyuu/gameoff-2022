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
            var planes = GeometryUtility.CalculateFrustumPlanes(_cameraLens);
            var pictureInterests = Object.FindObjectsOfType<PictureInterest>();
            var elementsInPicture = GetPictureInterests(planes, pictureInterests);
            float mainSubjectScore = float.MaxValue;

            foreach (var elementInPicture in elementsInPicture)
            {
                Bounds bounds = elementInPicture.pictureInterestCollider.bounds;

                Vector3 boundsCenterToScreen = _cameraLens.WorldToScreenPoint(bounds.center);
                Vector2 boundsCenterToScreen2d = new Vector2(boundsCenterToScreen.x, boundsCenterToScreen.y);
                Vector3 cameraElementDistance = bounds.center - _cameraLens.gameObject.transform.position;
                Rect cameraRect = _cameraLens.pixelRect;
                Vector2 pixelCameraCenter = new Vector2(cameraRect.width / 2f, cameraRect.height / 2f);
                Vector2 distanceFromCenter = pixelCameraCenter - boundsCenterToScreen2d;
                // dCenter is a diff between the center of the bounding box of the PictureInterest, translated to screen point and the center of the image (w/2, h/2)
                var dCenter = distanceFromCenter.magnitude;
                // dCamera is the world distance between the camera and the PictureInterest
                var dCamera = cameraElementDistance.magnitude;
                Debug.LogFormat("DCentre: {0} DCamera: {1} Nom: {2}", dCenter, dCamera, elementInPicture.name);

                // Main subject is lowest on all axis (dCenter and dCamera)
                float score = (new Vector2(dCenter, dCamera)).sqrMagnitude;
                if (score < mainSubjectScore)
                {
                    mainSubjectScore = score;
                    // Hack here. SetMainSubject might be called many times, only the actual main subject will be set last.
                    picture.SetMainSubject(dCenter, dCamera, elementInPicture);
                }
            }
            if (picture.MainSubject)
            {
                Debug.LogFormat("MainSubject is {0}", picture.MainSubject.name);
            }

            //TODO: Create a Picture, give it the texture, find on-screen CameraSubjects, make calculations (screen space ratio, other special stuff, etc)

            return picture;
        }

        private PictureInterest[] GetPictureInterests(Plane[] planes, PictureInterest[] pictureInterests)
        {
            List<PictureInterest> elementsInPicture = new List<PictureInterest>();

            foreach (var pictureInterest in pictureInterests)
            {
                if (GeometryUtility.TestPlanesAABB(planes, pictureInterest.pictureInterestCollider.bounds))
                {
                    elementsInPicture.Add(pictureInterest);
                }
            }
            return elementsInPicture.ToArray();
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