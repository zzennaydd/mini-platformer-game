using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemCollector))]
public class ItemCollectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Temel Inspector'ı çizin (mevcut alanlar ve özellikler)
        DrawDefaultInspector();

        // Hedef referansı alın
        ItemCollector itemCollector = (ItemCollector)target;

        // Arayüzde bir boşluk bırakın
        GUILayout.Space(10);

        // Bir buton ekleyin
        if (GUILayout.Button("IncreaseGoldCoinCount"))
        {
            // Butona tıklanırsa CollectItem işlevini çağırın
            itemCollector.IncreaseGoldCoinCount();
        }
    }
}
