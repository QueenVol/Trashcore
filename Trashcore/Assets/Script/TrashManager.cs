using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    public static TrashManager Instance;

    [Header("Required Trash Names")]
    public List<string> requiredItems = new List<string>()
    {
        "Pumpkin",
        "Sprout",
        "Gingerbread",
        "Gingerman",
        "Candy",
        "Cane"
    };

    private HashSet<string> collectedItems = new HashSet<string>();

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Collect(string itemName)
    {
        if (!collectedItems.Contains(itemName))
        {
            collectedItems.Add(itemName);
        }
    }

    public bool IsAllCollected()
    {
        foreach (var item in requiredItems)
        {
            if (!collectedItems.Contains(item))
                return false;
        }
        return true;
    }
}
