using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
public class Item
{
    public enum Quality
    {
        Trash,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    int id;
	string name;
	//gfx
	string description;
	int maxCount;
	int cost;

	[JsonConstructor]
	public Item(int id, string name, string description, int maxCount, int cost)
	{
		this.id = id;
		this.name = name;
		this.description = description;
		this.maxCount = maxCount;
		this.cost = cost;
	}
}
