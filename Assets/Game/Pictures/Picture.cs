using JetBrains.Annotations;
using UnityEngine;

namespace Game.Pictures
{
    public class Picture
    {
        [PublicAPI]
        public Texture Texture { get; }

        public Picture(Texture texture)
        {
            Texture = texture;
        }
    }
}