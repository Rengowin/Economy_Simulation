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
        storage.Add(RessourceEnum.Wood, 10);
        storage.Add(RessourceEnum.Planks, 5);
        storage.Add(RessourceEnum.Stone, 5);
        // Check amounts
        Debug.Log($"Wood: {storage.GetAmount(RessourceEnum.Wood)}"); // Should print 10
        Debug.Log($"Planks: {storage.GetAmount(RessourceEnum.Planks)}"); // Should print 5
        Debug.Log($"Stone: {storage.GetAmount(RessourceEnum.Stone)}"); // Should print 5
        // Remove resources
        bool removedWood = storage.Remove(RessourceEnum.Wood, 3);
        bool removedStone = storage.Remove(RessourceEnum.Stone, 6); // Should fail
        Debug.Log($"Removed Wood: {removedWood}"); // Should print True
        Debug.Log($"Removed Stone: {removedStone}"); // Should print False
        // Check amounts again
        Debug.Log($"Wood after removal: {storage.GetAmount(RessourceEnum.Wood)}"); // Should print 7
        Debug.Log($"Stone after removal: {storage.GetAmount(RessourceEnum.Stone)}"); // Should still print 5
    }
}
