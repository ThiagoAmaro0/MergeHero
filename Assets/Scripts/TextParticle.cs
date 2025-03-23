
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextParticle : MonoBehaviour
{
    [SerializeField] private TMP_Text _textPrefab;
    [SerializeField] private float _upOffset;
    [SerializeField] private Transform _canvasTransform;

    public static TextParticle instance;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void NewText(string msg, Vector3 worldPosition, float duration, Color color)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
        if (screenPos.z < 0)
            return;
        RectTransform canvasRect = _canvasTransform as RectTransform;
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, Camera.main, out localPoint))
        {
            TMP_Text text = Instantiate(_textPrefab, _canvasTransform);
            text.rectTransform.localPosition = localPoint;

            text.text = msg;
            text.color = color;

            text.rectTransform.DOAnchorPosY(text.rectTransform.anchoredPosition.y + _upOffset, duration).onComplete += () =>
            {
                text.DOFade(0, duration);
                Destroy(text.gameObject, duration);
            };

        }
    }

}
