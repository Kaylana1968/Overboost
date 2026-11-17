using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float JumpPower;
    [SerializeField]
    private float JumpDuration;

    private Vector3 _initialPosition;

    public Stat PlayerStat;


    void Start()
    {
        _initialPosition = transform.position;
    }

    public void Wait()
    {
        StartCoroutine(WalkBy(0));
    }

    public void Walk()
    {
        StartCoroutine(WalkBy(PlayerStat.Speed));
    }

    private IEnumerator WalkBy(int squares)
    {
        transform.DOComplete();

        float duration = JumpDuration / (1f + Mathf.Floor(squares / 10));

        for (int i = 0; i < squares; i++)
        {
            PlayerStat.CurrentSquare++;

            Vector3 targetPosition = new(_initialPosition.x + BoardManager.Instance.PlatformSpacing * PlayerStat.CurrentSquare, _initialPosition.y, _initialPosition.z);
            BoardManager.Instance.EnablePlatform(PlayerStat.CurrentSquare, duration);
            transform.DOJump(targetPosition, JumpPower, 1, duration);

            yield return new WaitForSeconds(duration);
        }

        GameManager.Instance.OnMovementEnded();
    }
}
