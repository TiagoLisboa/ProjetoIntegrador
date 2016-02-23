﻿	using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class mainController : MonoBehaviour {

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
		if (mainModel.tempo > mainModel.maxTempo) {
			mainModel.tempo = 0;
			mainModel.tempoInt = 0;
		} else if (mainModel.tempo < mainModel.maxTempo) {
			mainModel.tempo += Time.deltaTime;
		}
		if ((int)mainModel.tempo > mainModel.tempoInt) {
			mainModel.tempoInt = (int)mainModel.tempo;
			atualizarRecursos ();
		}
	}

	public void atualizarRecursos(){
		double aux;
		if (mainModel.tempoInt % 3 == 0) {
			if(mainModel.residencia > -1)
				mainModel.residencia -= 1;
			if(mainModel.saude > -1)
				mainModel.saude -= 1;
			if(mainModel.escola > -1)
				mainModel.escola -= 1;
			atualizarPrestigio ();
		} else if (mainModel.tempoInt % 5 == 0) {
			if(mainModel.industria > -1)
				mainModel.industria -= 1;
			if(mainModel.seguranca > -1)
				mainModel.seguranca -= 1;
			atualizarPrestigio ();
		} else if (mainModel.tempoInt % 7 == 0) {
			if(mainModel.alimento > -1)
				mainModel.alimento -= 1;
			atualizarPrestigio ();
		} else if (mainModel.tempoInt >= 30) {
			gerarQuestoes ();
			gerarMissao ();
			mainModel.dias++;
			Debug.Log (mainModel.dias);
			aux = mainModel.energia * 1.1;
			mainModel.energia = (int)aux;
			if (mainModel.energia > 100) {
				mainModel.energia = 100;
			}
		}
	}

	public void atualizarPrestigio () {
		if (mainModel.residencia < 0) {
			mainModel.prestigio += mainModel.residencia;
		}
		if (mainModel.saude < 0) {
			mainModel.prestigio += mainModel.saude;
		}
		if (mainModel.escola < 0) {
			mainModel.prestigio += mainModel.escola;
		}
		if (mainModel.industria < 0) {
			mainModel.prestigio += mainModel.industria;
		}
		if (mainModel.seguranca < 0) {
			mainModel.prestigio += mainModel.seguranca;
		}
		if (mainModel.alimento < 0) {
			mainModel.prestigio += mainModel.alimento;
		}
	}

	public void gameover () {
		if (mainModel.prestigio > 0) {

		} else {
			Debug.Log ("perdeeeeuuuu!");
			Time.timeScale = 0;
		}

		if (mainModel.energia <= 0
		    && mainModel.alimento <= 0
		    && mainModel.escola <= 0
		    && mainModel.industria <= 0
		    && mainModel.residencia <= 0
		    && mainModel.saude <= 0
		    && mainModel.seguranca <= 0) {

			Debug.Log ("perdeeeeuuuu!");
			Time.timeScale = 0;
		}
	}

	public void gerarMissao(){
		mainModel.missao = (Random.Range(1, mainModel.quests.Length));
		if (mainModel.missao != mainModel.auxMissao) {
			mainModel.auxMissao = mainModel.missao;
			Debug.Log (mainModel.missao);
		} else {
			gerarMissao ();
		}
	}

	public void gerarQuestoes () {
		int rando = Random.Range (0, 100);
		Debug.Log ("random = " + rando);
		if (rando < 60) {
			mainModel.temQuel = true;
			mainModel.questG = (Random.Range (0, mainModel.questoes.GetLength (0)));
			if (mainModel.questG != mainModel.auxQuestG) {
				mainModel.auxQuestG = mainModel.questG;
			} else {
				gerarQuestoes ();
			}
		}
	}

	public void verificarResposta (string resposta) {
		if (resposta == mainModel.questoes [mainModel.auxQuestG, 5]) {
			mainModel.energia += int.Parse( mainModel.questoes [mainModel.auxQuestG, 6]);
			if (mainModel.energia > 100)
				mainModel.energia = 100;
			mainModel.temQuel = false;
		} else {
			Debug.Log ("errou");
			mainModel.temQuel = false;
		}
	}

	public void verificarPausa () {
		if (mainModel.pause == true) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void adicionarRec (int recu){
		if (mainModel.energia > 0) {
			switch (recu) {
			case 1:
				if (mainModel.residencia < 10)
					mainModel.residencia++;
				mainModel.energia--;
				break;
			case 2:
				if (mainModel.saude < 10)
					mainModel.saude++;
				mainModel.energia--;
				break;
			case 3:
				if (mainModel.escola < 10)
					mainModel.escola++;
				mainModel.energia--;
				break;
			case 4:
				if (mainModel.seguranca < 10)
					mainModel.seguranca++;
				mainModel.energia--;
				break;
			case 5:
				if (mainModel.industria < 10)
					mainModel.industria++;
				mainModel.energia--;
				break;
			case 6:
				if (mainModel.alimento < 10)
					mainModel.alimento++;
				mainModel.energia--;
				break;
			}
		}
	}
}