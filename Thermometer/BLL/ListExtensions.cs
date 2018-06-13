using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Thermometer.BLL
{
  public static class ListExtensions
  {
    public static string GetValuesInOneString(this IList list)
    {
      var strb = new StringBuilder();

      foreach (var item in list)
      {
        strb.Append(item).Append(' ');
      }
      strb.Remove(strb.Length - 1, 1);
      return strb.ToString();
    }

    public static void PushToList<T>(this IList<T> list, T value )
    {
      for (var i = list.Count- 1; i > 0; i--)
      {
        list[i] = list[i - 1];
      }
      list[0] = value;
    }
  }
}