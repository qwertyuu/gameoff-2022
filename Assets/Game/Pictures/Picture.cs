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
            if (MainSubject == null || MainSubject.Heading == Heading.Away || MainSubject.Centering == Centering.WayOff || MainSubject.SizeCategory == Sizing.TooBig || MainSubject.SizeCategory == Sizing.TooSmall)
            {
                return 0;
            }
            // Idea for score:
            // every subject has a base score value
            // having the subject centered (in a zone around the absolute center of the camera) and facing at a reasonable distance gives max amount of points
            // so, TODO: Determine the zones in a picture (centered, off center, out of focus)
            // Also determine what a "reasonable distance" means. experiment by also adding pictureinterest width and height in order to approximate object size in picture (width, height and distance from camera)
            var scoreMultiplier = 1f;
            var score = MainSubject.PictureInterest.FullScore;

            if (MainSubject.Heading == Heading.Side)
            {
                scoreMultiplier /= 2f;
            }


            switch (MainSubject.SizeCategory)
            {
                case Sizing.Big:
                    scoreMultiplier /= 1.1f;
                    break;
                case Sizing.Small:
                    scoreMultiplier /= 1.5f;
                    break;
            }

            if (MainSubject.SubjectState == PictureInterestState.Enraged)
            {
                scoreMultiplier *= 2f;
            }
            return score * scoreMultiplier;

        }
    }

    public enum Heading
    {
        Facing, // Looking at the camera
        Side, // Seeing the side of the head
        Away // Turned away from the camera
    }

    public enum Sizing
    {
        TooSmall,
        Small,
        Reasonable,
        Big,
        TooBig
    }

    public enum Centering
    {
        WayOff, // Subject is really not centered
        Off, // Subject is not centered in image
        Centered // Subject is right in the center
    }

    public class PictureSubject
    {
        /*
        DistanceFromCenterOfPicture is a diff between the center of the bounding box of the PictureInterest, translated to screen point and the center of the image (w/2, h/2)
        0 means the center of the PictureInterest is straight in the center of the image
        otherwise it is the distance offset with the center
        */
        [PublicAPI]
        public float DistanceFromCenterOfPicture { get; }

        /*
        DistanceFromCamera is the world distance between the camera and the PictureInterest
        */
        [PublicAPI]
        public float DistanceFromCamera { get; }

        /*
        HeadingAngle (0 to 180) between where the element is looking and where it should look for it to look straight at the camera
        0 means the PictureInterest is looking straight at the camera, 180 means looking straight away (turned around)
        */
        [PublicAPI]
        public float HeadingAngle { get; }

        /*
        BoundsFrontArea is the area (width*height) of the front of the PictureInterest bounds. 
        This is used to approximate the size of the object in the photo in combination with the distance to the camera
        */
        [PublicAPI]
        public float BoundsFrontArea { get; }

        /*
        Heading tells us where the subject is looking relative to the picture.
        */
        [PublicAPI]
        public Heading Heading
        {
            get
            {
                var absAngle = Mathf.Abs(HeadingAngle);
                if (absAngle <= 15)
                {
                    return Heading.Facing;
                }
                if (absAngle <= 95)
                {
                    return Heading.Side;
                }
                return Heading.Away;
            }
        }

        /*
        SizeCategory tells us if the subject is far and hard to see, at a reasonable size or too big.
        */
        [PublicAPI]
        public Sizing SizeCategory
        {
            get
            {
                var sizeOnCanvas = BoundsFrontArea * (1f / DistanceFromCamera);
                if (sizeOnCanvas < 1)
                {
                    return Sizing.TooSmall;
                }
                if (sizeOnCanvas < 2)
                {
                    return Sizing.Small;
                }
                if (sizeOnCanvas < 4)
                {
                    return Sizing.Reasonable;
                }
                if (sizeOnCanvas <= 5.8)
                {
                    return Sizing.Big;
                }
                return Sizing.TooBig;
            }
        }

        /*
        Centering tells us if the subject is well centered in the image
        */
        [PublicAPI]
        public Centering Centering
        {
            get
            {
                if (DistanceFromCenterOfPicture < 25)
                {
                    return Centering.Centered;
                }
                if (DistanceFromCenterOfPicture >= 80)
                {
                    return Centering.WayOff;
                }
                return Centering.Off;
            }
        }

        /*
        HeadingAngle (0 to 180) between where the element is looking and where it should look for it to look straight at the camera
        0 means the PictureInterest is looking straight at the camera, 180 means looking straight away (turned around)
        */
        [PublicAPI]
        public PictureInterestState SubjectState { get; private set; }

        /*
        PictureInterest is the interest itself. note that it is not a copy in time of the picture, so the values contained in this class cannot be calculated again later
        */
        [PublicAPI]
        public PictureInterest PictureInterest { get; private set; }

        public PictureSubject(float distanceFromCenterOfPicture, float distanceFromCamera, float headingAngle, float boundsFrontArea, PictureInterest pictureInterest)
        {
            DistanceFromCenterOfPicture = distanceFromCenterOfPicture;
            DistanceFromCamera = distanceFromCamera;
            HeadingAngle = headingAngle;
            BoundsFrontArea = boundsFrontArea;
            PictureInterest = pictureInterest;
            SubjectState = pictureInterest.State;
        }

        public override string ToString()
        {
            return string.Format("Heading: {0}\tEtat: {1}\tGrosseur: {2}\tCentré: {3}\tNom: {4}", Heading, SubjectState, SizeCategory, Centering, PictureInterest.name);
        }
    }
}