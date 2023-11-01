using UnityEngine;

namespace ECSExperiments.Selection
{
    public class SelectionView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        private Rect _rect;

        public void Show(Rect rect)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            _rectTransform.localPosition = rect.center;
            _rect.center = rect.center;
            _rectTransform.sizeDelta = rect.size;
            _rect.size = rect.size;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}