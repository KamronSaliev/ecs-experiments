using UnityEngine;

namespace ECSExperiments.Input
{
    public class InputSelectionView : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform _rectTransform;

        private Rect _rect;

        public void Show(Rect rect)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }

            var position = rect.center;
            _rectTransform.localPosition = position;
            _rect.center = position;

            var size = rect.size;
            _rectTransform.sizeDelta = size;
            _rect.size = size;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}