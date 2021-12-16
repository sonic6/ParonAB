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
        NewProduct prod = new NewProduct();
        prod.number = number.text;
        prod.productName = productName.text;
        prod.price = int.Parse(price.text);

        manager.AddNewProduct(prod);
    }
}
