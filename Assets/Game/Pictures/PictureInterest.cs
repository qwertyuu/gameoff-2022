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
    private Collider pictureInterestCollider;

    public Collider PictureInterestCollider { get => pictureInterestCollider; }

    public PictureInterestState State = PictureInterestState.Default;
}
