﻿using UnityEngine;

namespace UGF.Views.Closable.AnimationStrategies
{
    // ToDo Strategies should ideally be components, that get setup by a ClosableView
    public static class AnimationStrategyFactory
    {
        public static IIClosableViewAnimationStrategy CreateDefaultAnimationStrategy(
            GameObject target)
        {
            return new DefaultAnimationStrategy(
                target);
        }

        public static IIClosableViewAnimationStrategy CreateFadeAnimationStrategy(
            Transform target,
            CanvasGroup canvasGroup)
        {
            return new FadeAnimationStrategy(
                target,
                canvasGroup);
        }

        public static IIClosableViewAnimationStrategy CreatePopOutAnimationStrategy(Transform target)
        {
            return new PopOutAnimationStrategy(target);
        }
    }
}
