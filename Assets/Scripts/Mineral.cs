using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral
{

    // This is the super class.
    // Not big fan of oop but since C# is that bullshit I must to do it like in this way.

    // The name of the mineral
    public string sName { get; set; }
    // The ID. Gonna be usefull for the inventory system
    public int iId { get; set; }
    // How much mineral can you extract from the rock
    public int iQuantity { get; set; }

    /// <summary>
    /// Mines the mineral.
    /// </summary>
    /// <returns>The </returns>
    int mine()
    {

        return iQuantity * 1;
    }
}
