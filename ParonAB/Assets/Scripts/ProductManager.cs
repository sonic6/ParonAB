using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class ProductManager : MonoBehaviour
{
    private string path = "//DESKTOP-1UKP42J/Arbetsprov/Produkter.txt";
    [SerializeField] GameObject ProductPrefab;
    [SerializeField] Transform ProductPanel;
    List<GameObject> products = new List<GameObject>(); //A list of current products that exist in the source file

    //Gets the file that the product information is saved in
    string[] GetSourceFile()
    {
        return File.ReadAllLines(path);
    }
    
    //Opens the produts Tab and displays all the available products
    public void OpenProductsMenu()
    {
        string[] lines = GetSourceFile();
        int i = 0;
        foreach (string line in lines)
        {
            string myLine = line;

            GameObject prodInstance = Instantiate(ProductPrefab, ProductPanel);
            products.Add(prodInstance);
            prodInstance.GetComponent<Product>().line = i;
            prodInstance.GetComponent<Product>().number.text = myLine.Substring(0, myLine.IndexOf(","));
            myLine = myLine.Remove(0, myLine.IndexOf(",") + 1);

            prodInstance.GetComponent<Product>().productName.text = myLine.Substring(0, myLine.IndexOf(","));
            myLine = myLine.Remove(0, myLine.IndexOf(",") + 1);


            prodInstance.GetComponent<Product>().price.text = myLine.Substring(0, myLine.IndexOf(";"));
            i++;
        }
        EditProduct(products[0].GetComponent<Product>(), "P001");
        StartCoroutine(UpdateLines());
    }

    //Refreshes the lines containing product information every second
    IEnumerator UpdateLines()
    {
        
        while(ProductPanel.gameObject.activeSelf)
        {
            string[] lines = GetSourceFile();
            int i = 0;
            foreach (string line in lines)
            {
                string myLine = line;

                products[i].GetComponent<Product>().number.text = myLine.Substring(0, myLine.IndexOf(","));
                myLine = myLine.Remove(0, myLine.IndexOf(",") + 1);

                products[i].GetComponent<Product>().productName.text = myLine.Substring(0, myLine.IndexOf(","));
                myLine = myLine.Remove(0, myLine.IndexOf(",") + 1);


                products[i].GetComponent<Product>().price.text = myLine.Substring(0, myLine.IndexOf(";"));
                i++;
                yield return new WaitForSeconds(1);
            }
        }
        yield return null;
        
    }

    void EditProduct(Product prod, string number = null, string name = null, string price = null)
    {
        string[] lines = GetSourceFile();
        for (int i = 0; i <= prod.line; i++)
        {
            if(i == prod.line)
            {
                if(number != null)
                    lines[i] = lines[i].Replace(prod.number.text, number);
                if(name != null)
                    lines[i] = lines[i].Replace(prod.productName.text, name);
                if(price != null)
                    lines[i] = lines[i].Replace(prod.price.text, price);

                print(lines[i]);
                File.WriteAllLines(path, lines);
                
            }
        }
    }

    public void DeleteInstancesOnPageClosed()
    {
        foreach(GameObject prod in products)
        {
            Destroy(prod);
        }
    }

    public void AddNewProduct(NewProduct prod)
    {
        string[] lines = GetSourceFile();
        List<string> newLines = new List<string>();
        newLines = lines.ToList();
        newLines.Add(prod.number + "," + prod.productName + "," + prod.price + ";");
        File.WriteAllLines(path, newLines);
    }
}
