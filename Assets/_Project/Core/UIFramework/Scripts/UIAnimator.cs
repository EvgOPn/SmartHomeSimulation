using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Core.UIFramework
{
    public class UIAnimator
    {
        private const float ButtonScaleDuration = 0.14f;
        private const float ButtonScaleMultiplier = 1.03f;
        
        private const float WindowScaleDuration = 0.08f;
        private const float WindowScaleMultiplier = 1.15f;

        private readonly Dictionary<RectTransform, Vector3> _normalScales = new Dictionary<RectTransform, Vector3>();

        public void ButtonScale(Button button, TweenCallback OnComplete = null)
        {
            ButtonScale(button.transform as RectTransform, OnComplete);
        }
        
        public void ButtonScale(Transform transform, TweenCallback OnComplete = null)
        {
            ButtonScale(transform as RectTransform, OnComplete);
        }
        
        public void ButtonScale(RectTransform rectTransform, TweenCallback OnComplete = null)
        {
            if (!_normalScales.ContainsKey(rectTransform))
            {
                _normalScales[rectTransform] = rectTransform.localScale;
            }
            
            Vector3 normalScale = _normalScales[rectTransform];
            Vector3 endScale = _normalScales[rectTransform] * ButtonScaleMultiplier;
            rectTransform.DOScale(endScale, ButtonScaleDuration * 0.5f).OnComplete(() =>
            {
                rectTransform.DOScale(normalScale, ButtonScaleDuration * 0.5f).OnComplete(OnComplete);
            });
        }

        public void WindowAppearance(Transform transform, TweenCallback OnComplete = null)
        {
            WindowAppearance(transform as RectTransform, OnComplete);
        }
        
        public void WindowAppearance(GameObject gameObject, TweenCallback OnComplete = null)
        {
            WindowAppearance(gameObject.transform as RectTransform, OnComplete);
        }
        
        public void WindowAppearance(RectTransform rectTransform, TweenCallback OnComplete = null)
        {
            if (!_normalScales.ContainsKey(rectTransform))
            {
                _normalScales[rectTransform] = rectTransform.localScale;
            }
            
            rectTransform.localScale = Vector3.zero;
            Vector3 normalScale = _normalScales[rectTransform];
            Vector3 endScale = _normalScales[rectTransform] * WindowScaleMultiplier;
            rectTransform.DOScale(endScale, WindowScaleDuration).OnComplete(() =>
            {
                rectTransform.DOScale(normalScale, WindowScaleDuration * 0.4f).OnComplete(OnComplete);
            });
        }
        
        public void WindowClosing(Transform transform, TweenCallback OnComplete = null)
        {
            WindowClosing(transform as RectTransform, OnComplete);
        }
        
        public void WindowClosing(GameObject gameObject, TweenCallback OnComplete = null)
        {
            WindowClosing(gameObject.transform as RectTransform, OnComplete);
        }
        
        public void WindowClosing(RectTransform rectTransform, TweenCallback OnComplete = null)
        {
            if (!_normalScales.ContainsKey(rectTransform))
            {
                _normalScales[rectTransform] = rectTransform.localScale;
            }
            
            Vector3 endScale = Vector3.zero;
            rectTransform.DOScale(endScale, WindowScaleDuration * 0.5f).OnComplete(OnComplete);
        }
    }
}
