using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PassengersDatabase", menuName = "LD43/PassengersDatabase", order = 0)]
public class PassengersDatabase : ScriptableObject 
{
    public List<PassengerGraphicsBase> graphicsBaseList;
}