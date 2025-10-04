private void AdicionarPontos(int valor)
{
    pontos += valor;
    Debug.Log("AdicionarPontos chamado! Valor: " + valor + " | Total: " + pontos);

    if (textoPontuacao != null)
        textoPontuacao.text = "Pontos: " + pontos;
}
