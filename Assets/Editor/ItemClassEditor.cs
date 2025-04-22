//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(ItemClass))]
//public class ItemClassEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        ItemClass itemClass = (ItemClass)target;

//        // Draw the default database field
//        itemClass.itemDatabase = (ItemDatabase)EditorGUILayout.ObjectField("Item Database", itemClass.itemDatabase, typeof(ItemDatabase), false);

//        if (itemClass.itemDatabase != null)
//        {
//            // Dropdown for item types
//            itemClass.selectedType = (ItemData.ItemType)EditorGUILayout.EnumPopup("Item Type", itemClass.selectedType);

//            // Get the actual ItemData object
//            ItemData data = itemClass.itemDatabase.GetItemByType(itemClass.selectedType);
//            itemClass.itemData = data;

//            if (data != null)
//            {
//                EditorGUILayout.Space(10);
//                EditorGUILayout.LabelField("Preview", EditorStyles.boldLabel);

//                // Mesh preview
//                if (data.mesh != null)
//                {
//                    Rect previewRect = GUILayoutUtility.GetRect(100, 100);
//                    EditorGUI.DrawPreviewTexture(previewRect, AssetPreview.GetAssetPreview(data.mesh));
//                }

//                //// Material preview
//                //if (data.material != null)
//                //{
//                //    Rect matRect = GUILayoutUtility.GetRect(60, 60);
//                //    EditorGUI.DrawPreviewTexture(matRect, AssetPreview.GetAssetPreview(data.material));
//                //}

//                // Apply visuals immediately
//                if (GUILayout.Button("Apply to Mesh"))
//                {
//                    itemClass.ApplyItemVisuals();
//                }
//            }
//        }
//        else
//        {
//            EditorGUILayout.HelpBox("Assign an Item Database to begin.", MessageType.Info);
//        }

//        if (GUI.changed)
//        {
//            EditorUtility.SetDirty(itemClass);
//            itemClass.ApplyItemVisuals();
//        }
//    }
//}
