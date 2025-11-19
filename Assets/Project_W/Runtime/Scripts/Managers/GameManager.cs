using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private async void Awake()
    {
        await W.DataManager.Instance.Initialize();
    }
}
