using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Game.Pictures
{
    public class Picture
    {
        [PublicAPI]
        public Texture Texture { get; }

        [PublicAPI]
        public PictureSubject MainSubject { get; private set; }

        [PublicAPI]
        public List<PictureSubject> Subjects { get; private set; }

        public Picture(Texture texture)
        {
            Texture = texture;
            Subjects = new List<PictureSubject>();
        }

        public void SetMainSubject(PictureSubject mainSubject)
        {
            MainSubject = mainSubject;
        }

        public void AddSubject(PictureSubject subject)
        {
            Subjects.Add(subject);
        }

        public float Score()
        {
            // TODO: Make a scoring algo from the MainSubject! Don't forget to do something about the PictureInterest's state!
            return 0;
        }
    }

    public class PictureSubject
    {
        /*
        DistanceFromCenterOfPicture is a diff between the center of the bounding box of the PictureInterest, translated to screen point and the center of the image (w/2, h/2)
        0 means the center of the PictureInterest is straight in the center of the image
        otherwise it is the distance offset with the center
        */
        [PublicAPI]
        public float DistanceFromCenterOfPicture { get; private set; }

        /*
        DistanceFromCamera is the world distance between the camera and the PictureInterest
        */
        [PublicAPI]
        public float DistanceFromCamera { get; private set; }

        /*
        HeadingAngle (0 to 180) between where the element is looking and where it should look for it to look straight at the camera
        0 means the PictureInterest is looking straight at the camera, 180 means looking straight away (turned around)
        */
        [PublicAPI]
        public float HeadingAngle { get; private set; }

        /*
        PictureInterest is the interest itself. note that it is not a copy in time of the picture, so the values contained in this class cannot be calculated again later
        */
        [PublicAPI]
        public PictureInterest PictureInterest { get; private set; }

        public PictureSubject(float distanceFromCenterOfPicture, float distanceFromCamera, float headingAngle, PictureInterest pictureInterest)
        {
            DistanceFromCenterOfPicture = distanceFromCenterOfPicture;
            DistanceFromCamera = distanceFromCamera;
            HeadingAngle = headingAngle;
            PictureInterest = pictureInterest;
        }

        public override string ToString()
        {
            return string.Format("DCentre: {0} DCamera: {1} HAngle: {2} Nom: {3}", DistanceFromCenterOfPicture, DistanceFromCamera, HeadingAngle, PictureInterest.name);
        }
    }
}