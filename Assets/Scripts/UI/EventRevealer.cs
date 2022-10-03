using DG.Tweening;
using UnityEngine;

public class EventRevealer : MonoBehaviour
{
    public static bool IsRevealing;

    [SerializeField] private float _endPos = 533f;

    [SerializeField] private float _revealAnimationTime;
    [SerializeField] private float _revealWaitTime;

    [SerializeField] private EventCard _cardPrefab;

    private Vector3 _cardRotation = new Vector3(0, 90, 0);

    private void Start()
    {
        EventControl.Instance.OnTimedEvent.AddListener(EventFired);
    }

    private void EventFired()
    {
        if (IsRevealing) return;
        IsRevealing = true;
        var randomEvent = GameManager.Instance.GetRandomEventData();
        EventReveal(randomEvent);
    }

    public void EventReveal(TimedEventData eventData)
    {
        var card = Instantiate(_cardPrefab, transform.position - new Vector3(0, _endPos, 0), Quaternion.identity, transform);
        card.SetData(eventData);
        var cardTransform = (RectTransform) card.transform;
        AudioControl.Instance.PlaySound("EventDraw");
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cardTransform.DOAnchorPosY(0, _revealAnimationTime).SetEase(Ease.OutSine))
                .Append(cardTransform.DORotate(_cardRotation, _revealAnimationTime).SetEase(Ease.OutSine))
                .AppendCallback(() =>
                {
                    card.SetDataImage();
                })
                .Append(card.transform.DORotate(_cardRotation * 2, _revealAnimationTime).SetEase(Ease.OutSine))
                .SetDelay(_revealWaitTime)
                .AppendCallback(() =>
                {
                    if (!card.ApplyEventEffect())
                    {
                        GameManager.Instance.ResumeTimers();
                    }
                })
                .Append(cardTransform.DOAnchorPosY(-_endPos, _revealAnimationTime).SetEase(Ease.InSine))
                .OnComplete(() =>
                {
                    Destroy(card);
                    IsRevealing = false;
                })
                .Play();
    }
}
