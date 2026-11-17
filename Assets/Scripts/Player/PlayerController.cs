using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float JumpPower;
    [SerializeField]
    private float JumpDuration;

    public Stat PlayerStat;

    private Vector3 _initialPosition;


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

            print(PlayerStat.CurrentSquare);

            if (PlayerStat.CurrentSquare == GameManager.Instance.FinishSquare)
            {
                GameManager.Instance.OnFinishReached();
                yield break;
            }
        }

        TurnManager.Instance.OnMovementEnded();
    }
}
