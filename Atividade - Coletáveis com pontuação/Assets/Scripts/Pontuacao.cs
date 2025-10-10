using UnityEngine;
using TMPro;

public class Pontuacao : MonoBehaviour
{
    public static Pontuacao instance;   
    public int pontos = 0;              
    public TMP_Text textoPontuacao;    

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
