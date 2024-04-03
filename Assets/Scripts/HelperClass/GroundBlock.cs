using UnityEngine;

public class GroundBlock : MonoBehaviour
{
    [SerializeField]
    private Transform _nextBlock;
    [SerializeField]
    internal float _halfLength = 100f;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private float _endOffset = 10f;

    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (transform.position.z + _halfLength < _player.transform.position.z - _endOffset)
        {
            transform.position = new Vector3(_nextBlock.position.x, _nextBlock.position.y, _nextBlock.position.z + _halfLength * 2);
        }
    }
}
