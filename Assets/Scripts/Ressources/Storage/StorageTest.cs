using UnityEngine;

public class StorageTest : MonoBehaviour
{
    /*
     Debug Function for inspector to test the storage system
     */

    [ContextMenu("Test Storage")]
    void TestStorage()
    {
        Storage storage = new Storage();
        // Add resources
        storage.Add(ResourceType.Wood, 10);
        storage.Add(ResourceType.Planks, 5);
        storage.Add(ResourceType.Stone, 5);
        // Check amounts
        Debug.Log($"Wood: {storage.GetAmount(ResourceType.Wood)}"); // Should print 10
        Debug.Log($"Planks: {storage.GetAmount(ResourceType.Planks)}"); // Should print 5
        Debug.Log($"Stone: {storage.GetAmount(ResourceType.Stone)}"); // Should print 5
        // Remove resources
        bool removedWood = storage.Remove(ResourceType.Wood, 3);
        bool removedStone = storage.Remove(ResourceType.Stone, 6); // Should fail
        Debug.Log($"Removed Wood: {removedWood}"); // Should print True
        Debug.Log($"Removed Stone: {removedStone}"); // Should print False
        // Check amounts again
        Debug.Log($"Wood after removal: {storage.GetAmount(ResourceType.Wood)}"); // Should print 7
        Debug.Log($"Stone after removal: {storage.GetAmount(ResourceType.Stone)}"); // Should still print 5
    }
}
