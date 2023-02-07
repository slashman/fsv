using UnityEngine;

public class WeightedPrefab {
	[SerializeField]
	public GameObject prefab;

	[SerializeField]
	public int weight;

	public static GameObject selectFrom (WeightedPrefab[] list) {
		int sum = 0;
		foreach (WeightedPrefab prefab in list) {
			sum += prefab.weight;
		}
		int pivot = Random.Range(0, sum);
		int acum = 0;
		foreach (WeightedPrefab prefab in list) {
			acum += prefab.weight;
			if (pivot < acum) {
				return prefab.prefab;
			}
		}
		return list[0].prefab;
	}

	public static int selectIndexFrom (int[] weightList) {
		int sum = 0;
		foreach (int weight in weightList) {
			sum += weight;
		}
		int pivot = Random.Range(0, sum);
		int acum = 0;
		int index = 0;
		foreach (int weight in weightList) {
			acum += weight;
			if (pivot < acum) {
				return index;
			}
			index++;
		}
		return 0;
	}
}