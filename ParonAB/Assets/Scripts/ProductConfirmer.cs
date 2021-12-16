using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductConfirmer : MonoBehaviour
{
    public Text number;
    public Text productName;
    public Text price;

    public ProductManager manager;

    //Creates an instance of the Class NewProducts and gives its variables the values insterted through the UI
    public void CreateNewProduct()
    {
        NewProduct prod = new NewProduct(number.text.ToUpper(), productName.text, int.Parse(price.text));
        manager.AddNewProduct(prod);
    }
}
