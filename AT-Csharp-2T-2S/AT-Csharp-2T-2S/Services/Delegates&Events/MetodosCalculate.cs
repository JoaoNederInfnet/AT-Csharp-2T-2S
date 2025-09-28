namespace AT_Csharp_2T_2S.Services.Delegates_Events;

//Delegate
//1) Declarando o delegate
public delegate decimal CalculateDelegate(decimal preco);
//========================================================

//2)Definindo os métodos que serão usados com o delegate
public class MetodosCalculate
{
   public static decimal DescontoAplicado(decimal preco)
   {
      decimal precoDescontado = preco * (1 - 0.1m);

      return precoDescontado;
   }
}