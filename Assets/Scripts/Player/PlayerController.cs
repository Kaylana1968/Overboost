using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float JumpPower;

    private Vector3 _initialPosition;

    public Stat PlayerStat;
    public UnityEvent OnStartRound;
    public UnityEvent OnEndRound;
    public UnityEvent OnStartTurn;
    public UnityEvent OnEndTurn;

    void Awake()
    {
        OnStartRound = new();
        OnEndRound = new();
        OnStartTurn = new();
        OnEndTurn = new();
    }

    void Start()
    {
        _initialPosition = transform.position;
    }

    public void Wait()
    {
        print("ZZZ");
        OnEndTurn.Invoke();
    }

    public void Walk()
    {
        StartCoroutine(WalkBy(PlayerStat.Speed));
    }

    private IEnumerator WalkBy(int squares)
    {
        transform.DOComplete();
        for (int i = 0; i < squares; i++)
        {
            Vector3 targetPosition = new(_initialPosition.x + BoardManager.Instance.PlatformSpacing * PlayerStat.CurrentSquare, _initialPosition.y, _initialPosition.z);
            PlayerStat.CurrentSquare++;

            BoardManager.Instance.EnablePlatform(PlayerStat.CurrentSquare);
            transform.DOJump(targetPosition, JumpPower, 1, BoardManager.Instance.CreationDuration);

            yield return new WaitForSeconds(BoardManager.Instance.CreationDuration);
        }
        OnEndTurn.Invoke();
    }
}
