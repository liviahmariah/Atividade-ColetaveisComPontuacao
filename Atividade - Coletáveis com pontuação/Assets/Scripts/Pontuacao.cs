using UnityEngine;
using TMPro;

public class Pontuacao : MonoBehaviour
{
    public static Pontuacao instance;   // Singleton
    public int pontos = 0;              // Pontos atuais
    public TMP_Text textoPontuacao;     // Arraste o TMP_Text do Canvas aqui

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void AdicionarPontos(int valor)
    {
        pontos += valor;

        if (textoPontuacao != null)
            textoPontuacao.text = "Pontos: " + pontos;
    }
}
