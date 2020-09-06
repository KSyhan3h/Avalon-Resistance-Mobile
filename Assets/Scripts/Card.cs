using System;
using UnityEngine;

public class Card : MonoBehaviour
{
	public ScriptableObject cardInfo;	// Info about the card

	public CardFace frontFace;			// Front of the Card
	public CardFace backFace;			// Back of the Card

	[Serializable]
	public class CardFace
	{
		public GameObject model;
		public void ApplyImage (Sprite sprite)
		{
			var material = model.GetComponent<Renderer> ().material;
			material.SetTexture ("_MainTex", sprite.texture);
		}
	}
}