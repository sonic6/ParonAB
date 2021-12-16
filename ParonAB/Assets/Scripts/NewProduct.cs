using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProduct
{
    public string number;
    public string productName;
    public int price;

    public NewProduct(string myNumber, string myName, int myPrice)
    {
        number = myNumber;
        productName = myName;
        price = myPrice;
    }
}
