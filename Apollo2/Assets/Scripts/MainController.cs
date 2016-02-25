﻿	using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class MainController : MonoBehaviour {

	public int rec1;

	// Use this for initialization
	void Start () {
		gerarMissao ();
	}
	
	// Update is called once per frame
	void Update () {
		contarTempo ();
		gameover ();
		verificarPausa ();
	}

	/**********************************************************************/
	/*******************************funções********************************/
	/**********************************************************************/


	public void contarTempo(){
		if (MainModel.tempo > MainModel.maxTempo) {
			MainModel.tempo = 0;
			MainModel.tempoInt = 0;
		} else if (MainModel.tempo < MainModel.maxTempo) {
			MainModel.tempo += Time.deltaTime;
		}
		if ((int)MainModel.tempo > MainModel.tempoInt) {
			MainModel.tempoInt = (int)MainModel.tempo;
			atualizarRecursos ();
		}
	}

	public void atualizarRecursos(){
		double aux;
		if (MainModel.tempoInt % 5 == 0) {
			if(MainModel.residencia > -1)
				MainModel.residencia -= 1;
			if(MainModel.saude > -1)
				MainModel.saude -= 1;
			if(MainModel.escola > -1)
				MainModel.escola -= 1;
			atualizarPrestigio ();
		} else if (MainModel.tempoInt % 7 == 0) {
			if(MainModel.industria > -1)
				MainModel.industria -= 1;
			if(MainModel.seguranca > -1)
				MainModel.seguranca -= 1;
			atualizarPrestigio ();
		} else if (MainModel.tempoInt % 11 == 0) {
			if(MainModel.alimento > -1)
				MainModel.alimento -= 1;
			atualizarPrestigio ();
		} else if (MainModel.tempoInt >= 30) {
			gerarQuestoes ();
			gerarMissao ();
			MainModel.dias++;
			Debug.Log (MainModel.dias);
			aux = MainModel.energia * 1.1;
			MainModel.energia = (int)aux;
			if (MainModel.energia > 100) {
				MainModel.energia = 100;
			}
		}
	}

	public void atualizarPrestigio () {
		if (MainModel.residencia < 0) {
			MainModel.prestigio += MainModel.residencia;
		}
		if (MainModel.saude < 0) {
			MainModel.prestigio += MainModel.saude;
		}
		if (MainModel.escola < 0) {
			MainModel.prestigio += MainModel.escola;
		}
		if (MainModel.industria < 0) {
			MainModel.prestigio += MainModel.industria;
		}
		if (MainModel.seguranca < 0) {
			MainModel.prestigio += MainModel.seguranca;
		}
		if (MainModel.alimento < 0) {
			MainModel.prestigio += MainModel.alimento;
		}
	}

	public void gameover () {
		if (MainModel.prestigio > 0) {

		} else {
			Debug.Log ("perdeeeeuuuu!");
			Time.timeScale = 0;
		}

		if (MainModel.energia <= 0
		    && MainModel.alimento <= 0
		    && MainModel.escola <= 0
		    && MainModel.industria <= 0
		    && MainModel.residencia <= 0
		    && MainModel.saude <= 0
		    && MainModel.seguranca <= 0) {

			Debug.Log ("perdeeeeuuuu!");
			Time.timeScale = 0;
		}
	}

	public void gerarMissao(){
		MainModel.missao = (Random.Range(1, MainModel.quests.Length));
		if (MainModel.missao != MainModel.auxMissao) {
			MainModel.auxMissao = MainModel.missao;
			Debug.Log (MainModel.missao);
		} else {
			gerarMissao ();
		}
	}

	public void gerarQuestoes () {
		int rando = Random.Range (0, 100);
		Debug.Log ("random = " + rando);
		if (rando < 60) {
			MainModel.temQuel = true;
			MainModel.questG = (Random.Range (0, MainModel.questoes.GetLength (0)));
			if (MainModel.questG != MainModel.auxQuestG) {
				MainModel.auxQuestG = MainModel.questG;
			} else {
				gerarQuestoes ();
			}
		}
	}

	public void verificarResposta (string resposta) {
		if (resposta == MainModel.questoes [MainModel.auxQuestG, 5]) {
			MainModel.energia += int.Parse( MainModel.questoes [MainModel.auxQuestG, 6]);
			Debug.Log ("acertou");
			if (MainModel.energia > 100)
				MainModel.energia = 100;
			MainModel.temQuel = false;
		} else {
			Debug.Log ("errou");
			MainModel.temQuel = false;
		}
	}

	public void verificarPausa () {
		if (MainModel.pause == true) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void adicionarRec (int recu){
		if (MainModel.energia > 0) {
			switch (recu) {
			case 1:
				if (MainModel.residencia < 10)
					MainModel.residencia++;
				MainModel.energia--;
				break;
			case 2:
				if (MainModel.saude < 10)
					MainModel.saude++;
				MainModel.energia--;
				break;
			case 3:
				if (MainModel.escola < 10)
					MainModel.escola++;
				MainModel.energia--;
				break;
			case 4:
				if (MainModel.seguranca < 10)
					MainModel.seguranca++;
				MainModel.energia--;
				break;
			case 5:
				if (MainModel.industria < 10)
					MainModel.industria++;
				MainModel.energia--;
				break;
			case 6:
				if (MainModel.alimento < 10)
					MainModel.alimento++;
				MainModel.energia--;
				break;
			}
		}
	}
}