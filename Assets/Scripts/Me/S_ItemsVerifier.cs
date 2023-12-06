using System.Collections.Generic;

public class S_ItemsVerifier
{
    public static int VerifieItem(string itemName)
    {
        for (int i = 0; i < PlayerInventory.Instance.items.Count; i++)
        {
            if (PlayerInventory.Instance.items[i].uniqueID == itemName)
            {
                return i;
            }
        }
        return -1;
    }

    public static List<int> VerifieItems(string itemName, int num)
    {
        List<int> temp = new List<int>();
        List<int> itemsIndex = new List<int>();

        itemsIndex.Add(-1);
        int count = 0;


        for (int i = 0; i < PlayerInventory.Instance.items.Count; i++)
        {
            if (PlayerInventory.Instance.items[i].uniqueID == itemName)
            {
                count++;
                temp.Add(i);

                if (count == num)
                {
                    itemsIndex.Clear();

                    itemsIndex = temp;
                }
            }
        }
        return itemsIndex;
    }
}
