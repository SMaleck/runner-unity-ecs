using System;
using UGF.Util;
using UGF.Util.UniRx;
using UnityEngine;

namespace Source.Features.ScreenSize
{
    public class ScreenSizeController : AbstractDisposable
    {
        private readonly ScreenSizeModel _screenSizeModel;

        public ScreenSizeController(ScreenSizeModel screenSizeModel)
        {
            _screenSizeModel = screenSizeModel;
        }

        public Vector2 GetBottomLeftCorner(
            float offsetX = 0f,
            float offsetY = 0f)
        {
            return new Vector2(
                -_screenSizeModel.WidthExtendUnits + offsetX,
                -_screenSizeModel.HeightExtendUnits + offsetY);
        }

        public Vector2 GetBottomRightCorner(
            float offsetX = 0f,
            float offsetY = 0f)
        {
            return new Vector2(
                _screenSizeModel.WidthExtendUnits + offsetX,
                -_screenSizeModel.HeightExtendUnits + offsetY);
        }

        public bool IsOutOfScreenBounds(Vector3 position, Vector2 size)
        {
            var screenSides = EnumHelper<ScreenSide>.Iterator;
            foreach (var side in screenSides)
            {
                if (IsOutOf(side, position, size))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsOutOf(ScreenSide side, Vector3 position, Vector2 size)
        {
            var halfSize = GetHalfSizeFor(side, size);

            switch (side)
            {
                case ScreenSide.Top:
                case ScreenSide.Bottom:
                    return Math.Abs(position.y) > _screenSizeModel.HeightExtendUnits + halfSize;

                case ScreenSide.Left:
                case ScreenSide.Right:
                    return Math.Abs(position.x) > _screenSizeModel.WidthExtendUnits + halfSize;

                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
        }

        private float GetHalfSizeFor(ScreenSide spawnSide, Vector2 entitySize)
        {
            switch (spawnSide)
            {
                case ScreenSide.Top:
                case ScreenSide.Bottom:
                    return entitySize.y / 2;

                case ScreenSide.Left:
                case ScreenSide.Right:
                    return entitySize.x / 2;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
