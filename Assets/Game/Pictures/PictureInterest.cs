using UnityEngine;

public enum PictureInterestState
{
    Default,
    Enraged,
    Special
}

public class PictureInterest : MonoBehaviour
{
    [SerializeField]
    private BoxCollider pictureInterestCollider;

    public BoxCollider PictureInterestCollider { get => pictureInterestCollider; }

    [SerializeField]
    private float fullScore;

    public float FullScore { get => fullScore; }

    public PictureInterestState State = PictureInterestState.Default;
}
