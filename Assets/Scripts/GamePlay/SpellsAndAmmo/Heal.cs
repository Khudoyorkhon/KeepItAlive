using UnityEngine;

public class Heal : MonoBehaviour
{
    #region Private Variable
    [SerializeField] private int _healAmount = 10;
    [SerializeField] private int _healDeviation = 2;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHeal>(out IHeal heal))
            heal?.Heal(Random.Range(_healAmount - _healDeviation, _healAmount + _healDeviation));
        gameObject.SetActive(false);
    }

}
