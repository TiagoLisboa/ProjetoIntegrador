﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CityController : MonoBehaviour {
	public int 	residencia, //bem louco
			   	saude,
			   	escola,
			   	industria,
			   	seguranca,
			   	alimento,
			   	energia,
			   	prestigio,
				maxRecurso,
				maxTempo,
				dias = 0,
				tempoInt,
				quest = 0,
				questG = 0;

	public int 	auxQuest,
				auxQuestG;

	public bool rel = false,
				quel = false,
				pause = false;

	public float tempo;

	public GameObject relatorioPanel;
	public GameObject quesPanel;

	private Animator anim1;
	private Animator anime;

	private string[] quests = {	"",
								"Mussum ipsum cacilds, vidis litro abertis. Consetis adipiscings elitis.",
								"Pra lá , depois divoltis porris, paradis. Paisis, filhis, espiritis santis.",
								"Mé faiz elementum girarzis, nisi eros vermeio, in elementis mé pra quem é amistosis quis leo.",
								"Manduma pindureta quium dia nois paga.",
								"Sapien in monti palavris qui num significa nadis i pareci latim."};

	private string[,] questoes = {
		{ "AAAAAAAAAAAAA", "a", "a", "a", "a", "a" },
		{ "BBBBBBBBBBBBB", "b", "b", "b", "b", "b" },
		{ "CCCCCCCCCCCCC", "c", "c", "c", "c", "c" },
		{ "DDDDDDDDDDDDD", "d", "d", "d", "d", "d" },
	}; 

	private Text 	varTextDia,
					varTextResidencia,
					varTextSaude,
					varTextEscola,
					varTextIndustria,
					varTextSeguranca,
					varTextAlimento,
					varTextEnergia,
					varTextPrestigio,
					varTextRelatorio;

	private Text 	questao,
					resA,
					resB,
					resC,
					resD,
					textChamada;

	// Use this for initialization
	public void updateRelatorio(){
		quest = (Random.Range(1, quests.Length-1));
		if (quest != auxQuest) {
			auxQuest = quest;
			varTextRelatorio.text = "Missões Diárias:\n" + quests [quest] + "\n" +
			"-----------------------------------------------------------" + "\n";
		} else {
			updateRelatorio ();
		}

	}

	public void selectQuestoes() {
		questG = (Random.Range(0, questoes.GetLength(0)));
		if (questG != auxQuestG) {
			auxQuestG = questG;
			questao.text = questoes [questG, 0];
			textChamada.text = questoes [questG, 0];
			resA.text = questoes [questG, 1];
			resB.text = questoes [questG, 2];
			resC.text = questoes [questG, 3];
			resD.text = questoes [questG, 4];
		} else {
			selectQuestoes ();
		}
	}

	public void checarResp(string x) {
		if (x == questoes [questG, 5]) {
			Debug.Log ("Acertou, miseravi");
		} else {
			Debug.Log ("Errou, retards");
		}
	}

	public void addEscola () {
		if (escola < maxRecurso) {
			escola += 1;
			energia -= 1;
		}

	}

	public void addSaude () {
		if (saude < maxRecurso) {
			saude += 1;
			energia -= 1;
		}

	}

	public void addAlimento () {
		if (alimento < maxRecurso) {
			alimento += 1;
			energia -= 1;
		}

	}

	public void addPolicia () {
		if (seguranca < maxRecurso) {
			seguranca += 1;
			energia -= 1;
		}

	}

	public void addIndustria () {
		if (industria < maxRecurso) {
			industria += 1;
			energia -= 1;
		}

	}

	public void addResidencia () {
		if (residencia < maxRecurso) {
			residencia += 1;
			energia -= 1;
		}

	}

	public void updateTempo(){
		if (tempo >= maxTempo) {
			tempo = 0;
			tempoInt = 0;
		} else if (tempo < maxTempo) {
			tempo += Time.deltaTime;
		}
		if ((int)tempo > tempoInt) {
			tempoInt = (int)tempo;
			updateRecursos ();
		}
		varTextDia.text = dias + " / " + tempoInt;
	}


	public void updateRecursos(){
		double aux;
		if (tempoInt % 3 == 0) {
			residencia -= 1;
			saude -= 1;
			escola -= 1;
			Debug.Log ("3 " + tempoInt);
			updatePrestigo ();
		} else if (tempoInt % 5 == 0) {
			industria -= 1;
			seguranca -= 1;
			Debug.Log ("5 " + tempoInt);
			updatePrestigo ();
		} else if (tempoInt % 7 == 0) {
			alimento -= 1;
			Debug.Log ("7 " + tempoInt);
			updatePrestigo ();
		} else if (tempoInt == 29) {
			dias++;
			Debug.Log (dias);
			aux = energia * 1.1;
			energia = (int)aux;
			if (energia > 100) {
				energia = 100;
			}
		} else if (tempoInt == 1) {
			selectQuestoes ();
			updateRelatorio ();
		}

	}

	public void updatePrestigo () {
		if (residencia < 0) {
			prestigio += residencia;
		}
		if (saude < 0) {
			prestigio += saude;
		}
		if (escola < 0) {
			prestigio += escola;
		}
		if (industria < 0) {
			prestigio += industria;
		}
		if (seguranca < 0) {
			prestigio += seguranca;
		}
		if (alimento < 0) {
			prestigio += alimento;
		}
	}

	void updateGUI(){
		varTextEscola.text = "Escola\n" + "(" + escola + ")";
		varTextSaude.text = "Saude\n" + "(" + saude + ")";
		varTextAlimento.text = "Alimento\n" + "(" + alimento + ")";
		varTextSeguranca.text = "Segurança\n" + "(" + seguranca + ")";
		varTextIndustria.text = "Industria\n" + "(" + industria + ")";
		varTextResidencia.text = "Residencia\n" + "(" + residencia + ")";
		varTextEnergia.text = ""+energia;
		varTextPrestigio.text = ""+prestigio;
	}

	public void slideRelatorio () {
		if (rel == false) {
			anim1.enabled = true;
			rel = true;
			pause = true;
			anim1.Play ("relatorioSlide");
		} else if (rel == true) {
			anim1.enabled = true;
			rel = false;
			pause = false;
			anim1.Play ("relatorioSlideBack");
		}
	}

	public void slideQuestionario () {
		if (quel == false) {
			anime.enabled = true;
			quel = true;
			anime.Play ("questionarioSlide");
		} else if (quel == true) {
			anime.enabled = true;
			quel = false;
			anime.Play ("questionarioSlideBack");
		}
	}


	void Start () { //start significa iniciar
		varTextDia = GameObject.Find("textDia").GetComponent<Text>();
		varTextResidencia = GameObject.Find("textResidencia").GetComponent<Text>();
		varTextSaude = GameObject.Find("textSaude").GetComponent<Text>();
		varTextEscola = GameObject.Find("textEscola").GetComponent<Text>();
		varTextIndustria = GameObject.Find("textIndustria").GetComponent<Text>();
		varTextSeguranca = GameObject.Find("textPolicia").GetComponent<Text>();
		varTextAlimento = GameObject.Find("textAlimento").GetComponent<Text>();
		varTextEnergia = GameObject.Find("textEnergia").GetComponent<Text>();
		varTextPrestigio = GameObject.Find("textPrestigio").GetComponent<Text>();
		varTextRelatorio = GameObject.Find("textRelatorio").GetComponent<Text>();

		textChamada = GameObject.Find("textChamada").GetComponent<Text>();
		questao = GameObject.Find("questao").GetComponent<Text>();
		resA = GameObject.Find("resA").GetComponent<Text>();
		resB = GameObject.Find("resB").GetComponent<Text>();
		resC = GameObject.Find("resC").GetComponent<Text>();
		resD = GameObject.Find("resD").GetComponent<Text>();

		anim1 = relatorioPanel.GetComponent<Animator> ();
		anime = quesPanel.GetComponent<Animator> ();
	}

	
	// Update is called once per frame
	void Update () { //update significa atualizar
		updateTempo();
		updateGUI();

		if (pause == false) {
			Time.timeScale = 1;
		} else {
			Time.timeScale = 0;
		}
	}



}


/*

1- Pensou, não é XGH.

XGH não pensa, faz a primeira coisa que vem à mente. Não existe segunda opção, a única opção é a mais rápida.

2- Existem 3 formas de se resolver um problema, a correta, a errada e a XGH, que é igual à errada, só que mais rápida.

XGH é mais rápido que qualquer metodologia de desenvolvimento de software que você conhece (Vide Axioma 14).

3- Quanto mais XGH você faz, mais precisará fazer.

Para cada problema resolvido usando XGH, mais uns 7 são criados. Mas todos eles serão resolvidos da forma XGH. XGH tende ao infinito.

4- XGH é totalmente reativo.

Os erros só existem quando aparecem.

5- XGH vale tudo, só não vale dar o toba.

Resolveu o problema? Compilou? Commit e era isso.

6- Commit sempre antes de update.

Se der merda, a sua parte estará sempre correta.. e seus colegas que se fodam.

7- XGH não tem prazo.

Os prazos passados pelo seu cliente são meros detalhes. Você SEMPRE conseguirá implementar TUDO no tempo necessário (nem que isso implique em acessar o BD por um script malaco).

8- Esteja preparado para pular fora quando o barco começar a afundar… ou coloque a culpa em alguém ou algo.

Pra quem usa XGH, um dia o barco afunda. Quanto mais o tempo passa, mais o sistema vira um monstro. O dia que a casa cair, é melhor seu curriculum estar cadastrado na APInfo, ou ter algo pra colocar a culpa.

9- Seja autêntico, XGH não respeita padrões.

Escreva o código como você bem entender, se resolver o problema, commit e era isso.

10- Não existe refactoring, apenas rework.

Se der merda, refaça um XGH rápido que solucione o problema. O dia que o rework implicar em reescrever a aplicação toda, pule fora, o barco irá afundar (Vide Axioma 8).

11- XGH é totalmente anárquico.

A figura de um gerente de projeto é totalmente descartável. Não tem dono, cada um faz o que quiser na hora que os problemas e requisitos vão surgindo (Vide Axioma 4).

12- Se iluda sempre com promessas de melhorias.

Colocar TODO no código como uma promessa de melhoria ajuda o desenvolvedor XGH a não sentir remorso ou culpa pela cagada que fez. É claro que o refactoring nunca será feito (Vide Axioma 10).

13- XGH é absoluto, não se prende à coisas relativas.

Prazo e custo são absolutos, qualidade é totalmente relativa. Jamais pense na qualidade e sim no menor tempo que a solução será implementada, aliás… não pense, faça!

14- XGH é atemporal.

Scrum, XP… tudo isso é modinha. O XGH não se prende às modinhas do momento, isso é coisa de viado. XGH sempre foi e sempre será usado por aqueles que desprezam a qualidade.

15- XGH nem sempre é POG.

Muitas POG’s exigem um raciocínio muito elevado, XGH não raciocina (Vide Axioma 1).

16- Não tente remar contra a maré.

Caso seus colegas de trabalho usam XGH para programar e você é um coxinha que gosta de fazer as coisas certinhas, esqueça! Pra cada Design Pattern que você usa corretamente, seus colegas gerarão 10 vezes mais código podre usando XGH.

17- O XGH não é perigoso até surgir um pouco de ordem.

Este axioma é muito complexo, mas sugere que o projeto utilizando XGH está em meio ao caos. Não tente por ordem no XGH (Vide Axioma 16), é inútil e você pode jogar um tempo precioso no lixo. Isto fará com que o projeto afunde mais rápido ainda (Vide Axioma 8). Não tente gerenciar o XGH, ele é auto suficiente (Vide Axioma 11), assim como o caos.

18- O XGH é seu brother, mas é vingativo.

Enquanto você quiser, o XGH sempre estará do seu lado. Mas cuidado, não o abandone. Se começar um sistema utilizando XGH e abandoná-lo para utilizar uma metodologia da moda, você estará fudido. O XGH não permite refactoring (vide axioma 10), e seu novo sistema cheio de frescurites entrará em colapso. E nessa hora, somente o XGH poderá salvá-lo.

19- Se tiver funcionando, não rela a mão.

Nunca altere, e muito menos questione um código funcionando. Isso é perda de tempo, mesmo porque refactoring não existe (Vide Axioma 10). Tempo é a engrenagem que move o XGH e qualidade é um detalhe desprezível.

20- Teste é para os fracos.

Se você meteu a mão num sistema XGH, é melhor saber o que está fazendo. E se você sabe o que está fazendo, vai testar pra que? Testes são desperdício de tempo, se o código compilar, é o suficiente.

21- Acostume-se ao sentimento de fracasso iminente.

O fracasso e o sucesso andam sempre de mãos dadas, e no XGH não é diferente. As pessoas costumam achar que as chances do projeto fracassar utilizando XGH são sempre maiores do que ele ser bem sucedido. Mas sucesso e fracasso são uma questão de ponto de vista. O projeto foi por água abaixo mas você aprendeu algo? Então pra você foi um sucesso!

22- O problema só é seu quando seu nome está no Doc da classe.

Nunca ponha a mão numa classe cujo autor não é você. Caso um membro da equipe morra ou fique doente por muito tempo, o barco irá afundar! Nesse caso, utilize o Axioma 8.

*/