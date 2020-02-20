using System;
using Source.Features.Camera;
using UGF.Util.UniRx;
using Unity.Mathematics;
using UnityEngine;

namespace Source.Features.ScreenSize
{
    public class ScreenSizeModel : AbstractDisposable
    {
        private readonly GameCamera _sceneCamera;

        public float WidthUnits => _sceneCamera.WidthUnits;
        public float HeightUnits => _sceneCamera.HeightUnits;

        public float WidthExtendUnits => WidthUnits / 2;
        public float HeightExtendUnits => HeightUnits / 2;

        private Vector2 CameraPosition => _sceneCamera.transform.position;

        public ScreenSizeModel(GameCamera sceneCamera)
        {
            _sceneCamera = sceneCamera;
        }

        public float2 GetCurrentEdge(ScreenSide screenSide)
        {
            switch(screenSide)
            {
                case ScreenSide.Top:
                    return new float2(
                        0,
                        CameraPosition.y + HeightExtendUnits);

                case ScreenSide.Bottom:
                    return new float2(
                        0,
                        CameraPosition.y - HeightExtendUnits);

                case ScreenSide.Left:
                    return new float2(
                        CameraPosition.x - WidthExtendUnits,
                        0);

                case ScreenSide.Right:
                    return new float2(
                        CameraPosition.x + WidthExtendUnits,
                        0);

                default:
                    throw new ArgumentOutOfRangeException(nameof(screenSide), screenSide, null);
            }
        }

        public float2 GetCurrentRightBottomCorner()
        {
            return new float2(
                CameraPosition.x + WidthExtendUnits,
                CameraPosition.y -HeightExtendUnits);
        }

        public float2 GetCurrentLeftBottomCorner()
        {
            return new float2(
                CameraPosition.x - WidthExtendUnits,
                CameraPosition.y - HeightExtendUnits);
        }
    }
}