using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Pictures
{
    public class Rollfilm : IDisposable
    {
        [Serializable]
        public struct Data
        {
            public int Width;
            public int Height;
            public int FilmCount;

            public Data(int width, int height, int filmCount)
            {
                Width = width;
                Height = height;
                FilmCount = filmCount;
            }
        }

        private readonly Camera _cameraLens;
        private readonly List<RenderTexture> _films;
        private readonly Data _data;
        
        private int _pictureToTake;

        public Rollfilm(Data data, Camera cameraLens)
        {
            _cameraLens = cameraLens;
            _data = data;
            _films = new List<RenderTexture>(_data.FilmCount);
            RenderTexture texture = new RenderTexture(_data.Width, _data.Height, 16);
            _films.Add(texture);
            for (int i = 1; i < _data.FilmCount; i++)
            {
                _films.Add(new RenderTexture(texture));
            }

            _cameraLens.enabled = false;
        }

        [CanBeNull, PublicAPI]
        public Texture TakePicture()
        {
            if (_pictureToTake >= _data.FilmCount)
            {
                _cameraLens.targetTexture = null;
                return null;
            }

            // render to texture
            _cameraLens.targetTexture = _films[_pictureToTake];
            _cameraLens.Render();
            _pictureToTake++;

            return _cameraLens.targetTexture;
        }

        public void Dispose()
        {
            _cameraLens.targetTexture = null;
            // You might think it's a good idea to dispose of the textures here
            // but they are used later on by other parts of the code when this rollfilm does not exist anymore,
            // so please don't delete them!
        }
    }
}