using System.Linq;
using UnityEngine;

public class TrapName : MonoBehaviour
{
    [SerializeField] private Transform _conteiner;
    [SerializeField] private Transform[] _transforms;

    private void Start()
    {
        GetTrapsInChildren();
        Sorting();
        Set();
    }

    private void Sorting() => 
        _transforms = _transforms.OrderBy(transform => transform.transform.position.x).ToArray();

    private void GetTrapsInChildren() => 
        _transforms = _conteiner.GetComponentsInChildren<Transform>()
            .Where(transform => transform.TryGetComponent<Trap>(out Trap trap) == true).ToArray();

    private void Set()
    {
        int index = 0;

        foreach (Transform transform in _transforms)
        {
            transform.name = $"Trap #{++index}";
        }
    }
}