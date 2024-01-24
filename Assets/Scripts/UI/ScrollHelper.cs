using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities
{
    public static class ScrollHelper
    {
        private static bool _isScrolling = false;
        public static IEnumerator ScrollToTarget(ScrollRect scrollRect, RectTransform targetRT, float speed = 2f, float precision = 0.1f)
        {
            if (_isScrolling) yield break;
            if (targetRT == null) yield return null;
            _isScrolling = true;
            RectTransform viewPort = scrollRect.viewport;

            var viewPortPosition = viewPort.position;
            var viewPortRect = viewPort.rect;

            float botLimit = viewPortPosition.y - precision * viewPortRect.height;
            float topLimit = viewPortPosition.y + precision * viewPortRect.height;
            float elapsedTime = 0f;
            float scrollIncrement;

            while ((targetRT.position.y < botLimit && scrollRect.verticalNormalizedPosition > 0.001f) || (targetRT.position.y > topLimit && scrollRect.verticalNormalizedPosition < 0.999f) || elapsedTime > 2000)
            {
                if (targetRT == null) yield return null;
                scrollIncrement = 0.5f * (targetRT.position.y - viewPortRect.position.y) / viewPortRect.height;

                if (scrollIncrement > 0f)
                {
                    scrollIncrement = Mathf.Min(scrollIncrement, 3000f / viewPortRect.height);
                }
                else
                {
                    scrollIncrement = Mathf.Max(scrollIncrement, -3000f / viewPortRect.height);
                }

                elapsedTime += Time.deltaTime;
                scrollRect.verticalNormalizedPosition += scrollIncrement * Time.deltaTime * speed;
                yield return null;
            }
            _isScrolling = false;
        }
    }
}
