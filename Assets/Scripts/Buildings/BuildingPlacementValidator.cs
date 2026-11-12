using UnityEngine;

public static class BuildingPlacementValidator
{
    public static bool CanPlaceBuilding(BuildingData buildingData, Vector3 postion)
    {
        if (IsOccupied(buildingData, postion))
        {
            Debug.Log("Cannot place building, position is occupied!");
            return false;
        }

        return true;
    }

    static bool IsOccupied(BuildingData buildingData, Vector3 position)
    {
        Vector3 halfExtents = new Vector3(buildingData.FootPrint.x*0.5f, 0.5f, buildingData.FootPrint.y*0.5f);
        Vector3 center = position + Vector3.up * 0.5f;

        // häää? as far as i understand it so that only buildings are detected for this chat/ what should be detatdet
        int buildingLayer = LayerMask.GetMask("Building");

        return Physics.CheckBox(center, halfExtents*0.99f, Quaternion.identity, buildingLayer);
    }
}
