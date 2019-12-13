using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SentenceReader : MonoBehaviour {

	private TMP_Text textComponent;
	public PassengerAI passenger;
	public System.Action done;

	public float delayPerWord=0.3f;
	private void Awake() {
		textComponent = GetComponent<TMP_Text>();
	}
	public void FeedSentence(string text)
	{
		textComponent.firstVisibleCharacter = 0;
		textComponent.text = text;
		transform.GetChild(0).gameObject.SetActive(true);
		textComponent.maxVisibleLines = 2;
		textComponent.ForceMeshUpdate();
		textComponent.maxVisibleCharacters = textComponent.textInfo.characterCount;
 
		StartCoroutine(SubtitlesCoroutine());
			
	}

	private void Start() 
	{
		if(passenger != null)
		{
			FeedSentence(passenger.GetSentence());
		}
		transform.GetChild(0).gameObject.SetActive(false);
		textComponent.text = "";
	}

	IEnumerator SubtitlesCoroutine()
	{
		int totalLines = textComponent.textInfo.lineCount;
		if(totalLines > 0)
		{

			int currentLine = 0;
			int[] startLines = new int[totalLines];
			int[] wordsCount = new int[totalLines];
			for(int i = 0; i < totalLines; i++)
			{
				startLines[i] = textComponent.textInfo.lineInfo[i].firstVisibleCharacterIndex;
				wordsCount[i] = textComponent.textInfo.lineInfo[i].wordCount;
			}
			{
				int words = wordsCount[currentLine]+(currentLine+1<totalLines?wordsCount[currentLine+1]:0);
				float delay = delayPerWord*words;
				yield return new WaitForSeconds(delay>=1.5f?delay:1.5f);
			}
			while(currentLine < totalLines)
			{
				textComponent.maxVisibleCharacters = textComponent.textInfo.characterCount;

				if(currentLine + 2 < totalLines)
				{
					currentLine += 2;
					textComponent.firstVisibleCharacter = startLines[currentLine];	
					int words = wordsCount[currentLine]+(currentLine+1<totalLines?wordsCount[currentLine+1]:0);
					float delay = delayPerWord*words;
					yield return new WaitForSeconds(delay>=1.5f?delay:1.5f);
				}
				else
				{
					currentLine = totalLines;
				}
				textComponent.maxVisibleCharacters = 0;
				yield return new WaitForSeconds(0.15f);

			}	
		}


		textComponent.text = "";
		transform.GetChild(0).gameObject.SetActive(false);
		if(passenger != null)
		{
			yield return new WaitForSeconds(Random.Range(passenger.minSpeechDelay,passenger.maxSpeechDelay));
			FeedSentence(passenger.GetSentence());
		}

		if(done != null)
		{
			done.Invoke();
		}
		yield return null;
	}
}
