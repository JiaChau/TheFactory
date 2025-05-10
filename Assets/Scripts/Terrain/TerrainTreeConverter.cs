using UnityEngine;
using UnityEditor;

public class TerrainTreeConverter : MonoBehaviour
{
    [ContextMenu("Convert Terrain Trees To GameObjects")]
    void ConvertTrees()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (!terrain) return;

        TerrainData data = terrain.terrainData;
        TreeInstance[] trees = data.treeInstances;

        for (int i = 0; i < trees.Length; i++)
        {
            TreeInstance tree = trees[i];
            TreePrototype prototype = data.treePrototypes[tree.prototypeIndex];
            GameObject prefab = prototype.prefab;

            Vector3 worldPos = Vector3.Scale(tree.position, data.size) + terrain.transform.position;
            GameObject newObj = Instantiate(prefab, worldPos, Quaternion.identity, this.transform);

            newObj.AddComponent<ResourceNode>(); // Optional, if not already part of prefab
        }

        data.treeInstances = new TreeInstance[0]; // Clear terrain trees
        Debug.Log("Converted trees to GameObjects.");
    }
}
