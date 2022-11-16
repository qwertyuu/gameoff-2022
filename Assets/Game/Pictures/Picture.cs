using JetBrains.Annotations;
using UnityEngine;

namespace Game.Pictures
{
    public class Picture
    {
        [PublicAPI]
        public Texture Texture { get; }

        [PublicAPI]
        public float MainSubjectDistanceFromCenterOfPicture { get; private set; }

        [PublicAPI]
        public float MainSubjectDistanceFromCamera { get; private set; }

        [PublicAPI]
        public PictureInterest MainSubject { get; private set; }

        public Picture(Texture texture)
        {
            Texture = texture;
        }

        public void SetMainSubject(float dCenter, float dCamera, PictureInterest mainSubject)
        {
            MainSubjectDistanceFromCenterOfPicture = dCenter;
            MainSubjectDistanceFromCamera = dCamera;
            MainSubject = mainSubject;
        }
    }
}