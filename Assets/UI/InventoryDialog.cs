using UnityEngine;
using TMPro;

public class InventoryDialog: MonoBehaviour {
	public GameObject panel;
	public GameObject rowPrefab;
	public Transform rowsContainer;
	public TMP_Text capacityText;
	public TMP_Text moneyText;
	public AudioClip openSFX;
	public AudioClip closeSFX;

	public void Show () {
		foreach (Transform row in rowsContainer) {
			GameObject.Destroy(row.gameObject);
 		}
		foreach (InventoryItem item in Expedition.i.inventory) {
			TableRow newRow = Instantiate(rowPrefab, rowsContainer).GetComponent<TableRow>();
			newRow.textfield.text = item.quantity == 1 ? item.GetName() : item.quantity + "x " + item.GetName();
		}
		capacityText.text = "Carrying " + Expedition.i.GetBurden() + "/" + Expedition.i.GetCarryCapacity();
		moneyText.text = "Money " + Expedition.i.money + " pesos";
		if (panel.activeInHierarchy == false)
		{	GetComponent<AudioSource>().PlayOneShot(openSFX);
			panel.SetActive(true);
		}
	}

	public void Hide () {
		World.i.ResumeTime();
		panel.SetActive(false);
		GetComponent<AudioSource>().PlayOneShot(closeSFX);
	}
}